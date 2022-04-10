using Loyalty.DataAccess.Shared;
using Loyalty.Domain.Models;
using Loyalty.Domain.Shared.IRepositories;

namespace Loyalty.DataAccess.Repositories
{
    public class RegistrationRepository : Repository<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(LoyaltyContext dbContext) : base(dbContext)
        {
        }

    }
}
