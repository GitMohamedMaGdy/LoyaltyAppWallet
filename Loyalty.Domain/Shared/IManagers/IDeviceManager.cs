using Loyalty.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Loyalty.Services.Shared.IManagers
{
    public interface IDeviceManager 
    {
        Task<BaseResponse> HandleRegisterDeviceAsync(HttpRequest request,string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken);
        Task<BaseResponse> HandleUnRegisterDevice(HttpRequest request,string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber);
        bool IsAuthorized(HttpRequest request,string passTypeIdentifier, string serialNumber);
        Task<BaseResponse> GetSerialNumberToUpdatePasses(string deviceLibraryIdentifier, string passTypeIdentifier, string passesUpdatedSince);
    }
}
