using Loyalty.Domain.Dtos;
using Loyalty.Services.Shared.IManagers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using static Loyalty.Domain.Dtos.ApplePassData;

namespace Loyalty.AppWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : Controller
    {
        private readonly IDeviceManager _deviceManager;
        public DevicesController(IDeviceManager deviceManager)
        {
            _deviceManager = deviceManager;
        }
        [HttpPost]
        [Route("/{version}/devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public async Task<ActionResult> RegisterDeviceAsync([FromBody] RegisterationModel model,
                  string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber)
        {
            var result = await _deviceManager.HandleRegisterDeviceAsync(Request,
                deviceLibraryIdentifier, passTypeIdentifier, serialNumber, model.PushToken);
            if (result.Code == (int)HttpStatusCode.Created)
                return Created("",result);
            else
                return BadRequest(result);
        }
        [HttpDelete]
        [Route("/{version}/devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}/{serialNumber}")]
        public async Task<ActionResult> UnRegisterDeviceAsync(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber)
        {
            var result = await _deviceManager.HandleUnRegisterDevice(Request, deviceLibraryIdentifier, passTypeIdentifier, serialNumber);
            if (result.Code == (int)HttpStatusCode.OK)
                return Ok(result);
            else
                return NotFound(result);
        }
        [HttpGet]
        [Route("/{version}/devices/{deviceLibraryIdentifier}/registrations/{passTypeIdentifier}")]
        public async Task<IActionResult> GetSerialNumbers([FromRoute] string deviceLibraryIdentifier, [FromRoute] string passTypeIdentifier, [FromQuery] string passesUpdatedSince)
        {
            var response = await _deviceManager.GetSerialNumberToUpdatePasses
                (deviceLibraryIdentifier, passTypeIdentifier, passesUpdatedSince);

            if (response.Code != (int)HttpStatusCode.OK)
                return NotFound(response);

            var SuccessResponse = response as SerialNumbersResponse;
            return Ok(JsonConvert.SerializeObject(SuccessResponse.serialNumbers));
        }
    }

}
