using Loyalty.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Loyalty.DataAccess.Shared.IManagers

{
    public interface IPassManager
    {
        byte[] GeneratePass(VoucherDetails voucherDetails);
        Task<HttpClientResponseVM> GetVoucherDetails(string couponNumber);
        BaseResponse HandleGetLatestPass(HttpRequest request,string passTypeIdentifier,string serialNumber);
        Task<Domain.Models.Pass> GetPass(string passTypeIdentifier, string serialNumber);

    }
}
