using Loyalty.DataAccess.Shared;
using Loyalty.Domain.Models;
using Loyalty.Domain.Shared.IRepositories;

namespace Loyalty.DataAccess.Repositories
{
    public class LogRepository : Repository<APILog>, ILogRepository
    {
        public LogRepository(LoyaltyContext dbContext) : base(dbContext)
        {
        }

    }
}
