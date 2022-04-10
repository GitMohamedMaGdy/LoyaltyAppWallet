using Loyalty.Domain.Dtos;
using Loyalty.Domain.Models;
using Loyalty360Core.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Loyalty.AppWallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("/{version}/log")]
        public async System.Threading.Tasks.Task<IActionResult> PostAsync([FromBody] ApplePassData.LogPayload payload)
        {
            var log = new APILog();
            foreach (var logItem in payload.logs)
            {
                log.Log = logItem;
                log.datetime = DateTime.Now;
                await _unitOfWork.Logs.Add(log);
            }
            await _unitOfWork.Complete();
            return Ok();
        }
    }

}
