using Loyalty.Services.Shared.IManagers;
using Loyalty.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Threading.Tasks;
using Loyalty360Core.Domain.Shared;
using Loyalty.DataAccess.Shared;
using System.Net;
using Loyalty.Domain.Dtos;
using Loyalty.DataAccess.Shared.IManagers;

namespace Loyalty.DataAccess.Managers
{
    public class DeviceManager : IDeviceManager
    {
        private readonly Helper _helper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPassManager _passManager;
        public DeviceManager(Helper helper, IUnitOfWork unitOfWork, IPassManager passManager)
        {
            _helper = helper;
            _unitOfWork = unitOfWork;
            _passManager = passManager;
        }
        public async Task<BaseResponse> HandleRegisterDeviceAsync(HttpRequest request, string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken)
        {
            var authenticationToken = _helper.IsAuthorized(request, passTypeIdentifier, serialNumber);
            if (authenticationToken == null) return new BaseResponse { Code = 5, Message = "Not Authorized" };

            var DeviceRegisterationFound = await GetDeviceRegisteration(deviceLibraryIdentifier, serialNumber);
            var PassExists = await _passManager.GetPass(passTypeIdentifier, serialNumber);
            if (DeviceRegisterationFound != null)
            {
                return new BaseResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Device Already Registered"
                };
            }
            else
            {
                try
                {
                    await _unitOfWork.BeginTransaction();
                    if (PassExists == null) await _unitOfWork.Passes.Add(new Pass(passTypeIdentifier, serialNumber, null, DateTime.Now, authenticationToken));
                    await _unitOfWork.devices.Add(new Device(deviceLibraryIdentifier, pushToken));
                    await _unitOfWork.Registerations.Add(new Registration(passTypeIdentifier, deviceLibraryIdentifier, serialNumber, pushToken));
                    await _unitOfWork.CommitTransaction();
                    await _unitOfWork.Complete();
                    return new BaseResponse()
                    {
                        Code = (int)HttpStatusCode.Created,
                        Message = "Device Registered"
                    };
                }
                catch (Exception ex)
                {
                    await _unitOfWork.RollbackTransaction();
                    throw ex;
                }
            }
        }
        public async Task<BaseResponse> HandleUnRegisterDevice(HttpRequest request, string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber)
        {
            var isAuthorized = IsAuthorized(request, passTypeIdentifier, serialNumber);
            if (!isAuthorized) return new BaseResponse { Code = (int)HttpStatusCode.Unauthorized, Message = "Not Authorized" };

            var DeviceRegisterationFound = await GetDeviceRegisteration(deviceLibraryIdentifier, serialNumber);
            if (DeviceRegisterationFound == null)
                return new BaseResponse()
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = "Device Not Found"
                };
            else
            {
                await _unitOfWork.Registerations.DeleteWithComplete(DeviceRegisterationFound);
                var HasAnyRegisteration = DeviceHasRegisteration(deviceLibraryIdentifier);
                if (!HasAnyRegisteration)
                    await _unitOfWork.devices.DeleteWithComplete(new Device(deviceLibraryIdentifier, DeviceRegisterationFound.SerialNumber));
                return new BaseResponse()
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Device Deleted"
                };
            }

        }
        private bool DeviceHasRegisteration(string deviceLibraryIdentifier)
        {
            return _unitOfWork.Registerations.GetMany(c => c.DeviceLibraryIdentifier == deviceLibraryIdentifier).Any();

        }
        public async Task<BaseResponse> GetSerialNumberToUpdatePasses(string deviceLibraryIdentifier, string passTypeIdentifier, string passesUpdatedSince)
        {
            var response = new SerialNumbersResponse();
            var DeviceRegisteredForAnyPass = DeviceHasRegisteration(deviceLibraryIdentifier);
            if (!DeviceRegisteredForAnyPass)
                return new BaseResponse
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = "Device is not Registered"
                };
            else
            {
                var RegisteredPassesForDevice = _unitOfWork.Registerations.GetMany(x => x.DeviceLibraryIdentifier == deviceLibraryIdentifier &&
                                                          x.PassTypeIdentifier == passTypeIdentifier).Select(x => x.SerialNumber).ToList();

                var AllpassesForDevice = (await _unitOfWork.Passes.GetManyAsync(x => RegisteredPassesForDevice.Contains(x.SerialNumber))).ToList();

                if (passesUpdatedSince == null || passesUpdatedSince == "")
                {
                    response.serialNumbers.SerialNumbers = AllpassesForDevice.Select(c => c.SerialNumber).ToList(); ;
                    response.serialNumbers.LastUpdated = null;
                }
                else
                {
                    var updatedSince = DateTime.ParseExact(passesUpdatedSince, "yyyy/MM/dd HH:mm:ss", null);
                    response.serialNumbers.SerialNumbers = AllpassesForDevice.Where(x => x.LastUpdateAt >= updatedSince).Select(c => c.SerialNumber).ToList();
                    response.serialNumbers.LastUpdated = string.Format("{0:yyyy/MM/dd HH:mm:ss}", updatedSince);
                }
                response.Code = (int)HttpStatusCode.OK;
                response.Message = "passes that could be updated for this device";
                return response;
            }
        }

        public bool IsAuthorized(HttpRequest request, string passTypeIdentifier, string serialNumber)
        {
            request.Headers.TryGetValue("Authorization", out StringValues headerValues);
            if (headerValues.Count == 0)
                return false;
            else
            {
                var authorizationHeaderValue = headerValues[0];
                var tokenValue = authorizationHeaderValue.Replace("ApplePass", "").Trim();
                var pass = _unitOfWork.Passes.Get(p => p.PassTypeIdentifier == passTypeIdentifier && p.SerialNumber == serialNumber);
                return tokenValue == pass.AuthenticationToken;
            }
        }

        private async Task<Registration> GetDeviceRegisteration(string deviceLibraryIdentifier, string serialNumber)
        {
            return await _unitOfWork.Registerations.GetAsync((r => r.DeviceLibraryIdentifier == deviceLibraryIdentifier &&
                                      r.SerialNumber == serialNumber));
        }

    }
}
