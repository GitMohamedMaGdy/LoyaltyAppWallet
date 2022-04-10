using Loyalty.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Passbook.Generator;
using Passbook.Generator.Fields;
using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Loyalty.DataAccess.Shared;
using Loyalty.DataAccess.Shared.IManagers;
using Loyalty360Core.Domain.Shared;
using Loyalty.Domain.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Primitives;

namespace Loyalty.DataAccess.Managers
{
    public class PassManager : IPassManager
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly Helper _helper;
        private readonly IUnitOfWork _unitOfWork;


        public PassManager(IConfiguration configuration, ILogger<PassManager> logger, Helper helper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _logger = logger;
            _helper = helper;
            _unitOfWork = unitOfWork;
        }

        public byte[] GeneratePass(VoucherDetails voucherDetails)
        {
            try
            {
                var request = new PassGeneratorRequest();
                SetConfigurationToPassFile(request, voucherDetails);
                SetImagesToPassFile(request, voucherDetails);
                SetFieldsToPassFile(request, voucherDetails);
                request.AuthenticationToken = _configuration.GetValue<string>("PkPassFile:AuthenticationToken");
                request.WebServiceUrl = _configuration.GetValue<string>("PkPassFile:WebServiceURL");
                request.AddBarcode(BarcodeType.PKBarcodeFormatQR, voucherDetails.VoucherCode, "UTF-8");
                var rawPass = new PassGenerator().Generate(request);
                return rawPass;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Exception Occured at Generate PkPass File {ex}");
                return null;
            }

        }
        private void SetImagesToPassFile(PassGeneratorRequest request, VoucherDetails voucherDetails)
        {
            request.Images.Add(PassbookImage.Strip, GetFileContents(@"Passes/Templates/Coupon/strip.png"));
            request.Images.Add(PassbookImage.Strip2X, GetFileContents(@"Passes/Templates/Coupon/strip@2x.png"));
            request.Images.Add(PassbookImage.Logo, GetFileContents(@"Passes/Templates/Coupon/logo.png"));
            request.Images.Add(PassbookImage.Logo2X, GetFileContents(@"Passes/Templates/Coupon/logo@2x.png"));
            request.Images.Add(PassbookImage.Icon, GetFileContents(@"Passes/Templates/Coupon/icon.png"));
            request.Images.Add(PassbookImage.Icon2X, GetFileContents(@"Passes/Templates/Coupon/icon@2x.png"));
            _logger.LogInformation("Images Added Successfully");
        }
        private void SetFieldsToPassFile(PassGeneratorRequest request, VoucherDetails voucherDetails)
        {
            request.AddHeaderField(new StandardField("statusKey", "", voucherDetails.Status));
            request.AddSecondaryField(new StandardField("storeNameKey", "", voucherDetails.StoreName));
            request.AddSecondaryField(new StandardField("clientNameKey", "", voucherDetails.ClientName));
            request.AddSecondaryField(new StandardField("discountValueKey", "", voucherDetails.DiscountValue.ToString()));
            request.AddSecondaryField(new StandardField("codeKey", "Code", voucherDetails.VoucherCode));
            _logger.LogInformation("Fields Added Successfully");
        }
        private void SetConfigurationToPassFile(PassGeneratorRequest request, VoucherDetails voucherDetails)
        {
            byte[] certData = GetFileContents(@"wwwroot/certificates/DsqPassCert.p12");
            byte[] certData__ = GetFileContents(@"wwwroot/certificates/wwdc.pem");
            request.Style = PassStyle.Coupon;
            request.PassTypeIdentifier = _configuration.GetValue<string>("PkPassFile:PassTypeIdentifier");
            request.TeamIdentifier = _configuration.GetValue<string>("PkPassFile:TeamIdentifier");
            request.PassbookCertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certData, _configuration.GetValue<string>("PkPassFile:CertificatePassword"));
            request.AppleWWDRCACertificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certData__);
            request.SerialNumber = _configuration.GetValue<string>("PkPassFile:SerialNumber");
            request.BackgroundColor = _configuration.GetValue<string>("PkPassFile:BackgroundColor");
            request.ForegroundColor = _configuration.GetValue<string>("PkPassFile:ForegroundColor");
            request.LabelColor = _configuration.GetValue<string>("PkPassFile:LabelColor");
            request.Description = _configuration.GetValue<string>("PkPassFile:Description");
            request.OrganizationName = _configuration.GetValue<string>("PkPassFile:OrganizationName");
            request.LogoText = voucherDetails.StoreName;
            _logger.LogInformation("Configurations Added Successfully");
        }
        private byte[] GetFileContents(string path)
        {
            return File.ReadAllBytes(path);
        }

        public async Task<HttpClientResponseVM> GetVoucherDetails(string couponNumber)
        {
            var relativeUrl = string.Format("api/getVoucherDetails?couponNumber={0}", couponNumber);
            return await _helper.CreateRequest("ClientURL", HttpMethod.Get, relativeUrl, null);
        }
        public BaseResponse HandleGetLatestPass(HttpRequest request, string passTypeIdentifier, string serialNumber)
        {
            byte[] passBytes = null;
            var token = _helper.IsAuthorized(request, passTypeIdentifier, serialNumber);
            if (token == null) return new BaseResponse { Code = (int)HttpStatusCode.Unauthorized, Message = "Not Authorized" };

            request.Headers.TryGetValue("if-modified-since", out StringValues headerValues);
            var passesUpdatedSince = headerValues[0];
            var pass = _unitOfWork.Passes.Get(p => p.PassTypeIdentifier == passTypeIdentifier && p.SerialNumber == serialNumber);
            if (passesUpdatedSince != null)
            {
                var updatedSince = DateTime.ParseExact(passesUpdatedSince, "MM/dd/yyyy HH:mm:ss", null);
                pass = _unitOfWork.Passes.Get(p => p.PassTypeIdentifier == passTypeIdentifier 
                                             && p.SerialNumber == serialNumber && p.LastUpdateAt > updatedSince);
            }
            return new FileResponse
            {
                Code = (int)HttpStatusCode.OK,
                Message = "succeeded",
                FileStream = passBytes
            };
        }

        public async Task<Domain.Models.Pass> GetPass(string passTypeIdentifier, string serialNumber)
        {
            return await _unitOfWork.Passes.GetAsync((r => r.PassTypeIdentifier == passTypeIdentifier &&
                                      r.SerialNumber == serialNumber));
        }
    }
}
