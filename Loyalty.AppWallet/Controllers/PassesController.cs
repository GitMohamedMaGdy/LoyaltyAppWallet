using Loyalty.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Loyalty.DataAccess.Shared.IManagers;
using static Loyalty.Domain.Dtos.ApplePassData;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
using System.Net.Http.Headers;
using System.Net;

namespace Loyalty.AppWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassesController : Controller
    {
        private readonly IPassManager _passManager;
        public PassesController(IPassManager passManager)
        {
            _passManager = passManager;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        //[HttpPost]
        //[Route("GeneratePass")]
        //public IActionResult GeneratePass(string couponNumber)
        //{
        //    var response = await _passManager.GetVoucherDetails(couponNumber);
        //    var voucherDetails = response.ResponseJson.Value<VoucherDetails>();
        //    var generatedPassFile = _passManager.GeneratePass(voucherDetails);
        //    return File(generatedPassFile, "application/vnd.apple.pkpass");
        //}

        [HttpPost]
        [Route("GeneratePass")]
        public IActionResult GeneratePassAsync(VoucherDetails voucherDetails)
        {
            var generatedPassFile = _passManager.GeneratePass(voucherDetails);
            if (generatedPassFile == null)
                return BadRequest("Error Generating File");
            else
                return File(generatedPassFile, "application/vnd.apple.pkpass");
        }

        [HttpGet]
        [Route("/{version}/passes/{passTypeIdentifier}/{serialNumber}")]
        public  IActionResult Get (string passTypeIdentifier, string serialNumber)
        {
            var response = _passManager.HandleGetLatestPass(Request, passTypeIdentifier, serialNumber);
            if (response.Code != (int)HttpStatusCode.OK)
                return Unauthorized(response);

            var successResponse = response as FileResponse;
            Response.Headers.Add("Last-Modified", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            return File(successResponse.FileStream, "application/vnd.apple.pkpass");

           
        }
    }

}
