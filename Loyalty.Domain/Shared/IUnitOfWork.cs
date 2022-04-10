using Loyalty.Domain;
using Loyalty.Domain.Shared.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Loyalty360Core.Domain.Shared
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task DisposeTransaction();
        public IDeviceRepository devices { get; }
        public IRegistrationRepository Registerations { get; }
        public IPassRepository Passes { get; }
        public ILogRepository Logs { get; }
        //TRepository Repository<TEntity, TRepository>() where TEntity : class
        //    where TRepository : IRepository<TEntity>;
    }
}
