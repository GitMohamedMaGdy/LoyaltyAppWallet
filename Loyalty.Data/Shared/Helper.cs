using Loyalty.Domain.Dtos;
using Loyalty.Domain.Shared.IRepositories;
using Loyalty360Core.Domain.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Loyalty.DataAccess.Shared
{
    public class Helper
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _Configuration;
        private readonly ILogger _logger;

        public Helper(IConfiguration Configuration, IUnitOfWork unitOfWork, ILogger<Helper> logger)
        {
            _unitOfWork = unitOfWork;
            _Configuration = Configuration;
            _logger = logger;
        }
        public int counter = 0;

        public string IsAuthorized(HttpRequest request, string passTypeIdentifier, string serialNumber)
        {
            request.Headers.TryGetValue("Authorization", out StringValues headerValues);
            if (headerValues.Count != 0)
            { 
                var authorizationHeaderValue = headerValues[0];
                var authenticationToken = authorizationHeaderValue.Replace("ApplePass", "").Trim();
                //var pass = _unitOfWork.Passes.Get(p => p.PassTypeIdentifier == passTypeIdentifier && p.SerialNumber == serialNumber);
                if (authenticationToken == _Configuration.GetValue<string>("PkPassFile:AuthenticationToken"))
                return authenticationToken;
            }
            return null;

        }

        private StringBuilder FormatUrl(string url, Dictionary<string, string> urlParams)
        {
            var apiUrl = urlParams.Count > 0 ? new StringBuilder(url + "?") : new StringBuilder(url);
            for (var i = 0; i < urlParams.Count; i++)
            {
                apiUrl.Append(urlParams.Keys.ElementAt(i)).Append("=").Append(urlParams[urlParams.Keys.ElementAt(i)]);
                if (i < urlParams.Count - 1)
                    apiUrl.Append("&");
            }
            return apiUrl;
        }

        public async Task<HttpClientResponseVM> CreateRequest(string baseurl, HttpMethod method, string relativeUrl, string jsonObj = null)
        {
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                using (var client = new HttpClient(httpClientHandler))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    client.BaseAddress = new Uri(baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Accept-Language", "en");
                    HttpRequestMessage request = new HttpRequestMessage(method, relativeUrl);
                    if (jsonObj != null)
                        request.Content = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                    var Res = new HttpResponseMessage();
                    try
                    {
                        Res = client.SendAsync(request).Result;
                        if (Res.StatusCode == HttpStatusCode.Unauthorized && counter == 0)
                        {
                            counter++;
                            return await CreateRequest(baseurl, method, relativeUrl, jsonObj);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Exception Occured in request GetAPI to Couponz {ex}");
                    }
                    var obj = JObject.Parse(Res.Content.ReadAsStringAsync().Result);
                    return new HttpClientResponseVM() { StatusCode = (int)Res.StatusCode, ResponseJson = obj };
                }
            }
        }
    }
}
