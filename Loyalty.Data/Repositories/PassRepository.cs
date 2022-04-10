using Loyalty.DataAccess.Shared;
using Loyalty.Domain;
using Loyalty.Domain.Models;
using Loyalty.DataAccess.Shared;
using Loyalty.Domain.Shared.IRepositories;

namespace Loyalty.DataAccess.Repositories
{
    public class PassRepository : Repository<Pass>, IPassRepository
    {
        public PassRepository(LoyaltyContext dbContext) : base(dbContext)
        {
        }

    }
}
