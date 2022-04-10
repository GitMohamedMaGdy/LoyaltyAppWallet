using Loyalty.Domain.Models;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Loyalty.DataAccess.Shared;
using Loyalty.Domain.Shared.IRepositories;

namespace Loyalty.DataAccess.Repositories
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {

        private readonly IRegistrationRepository _registrationRepository;
        public DeviceRepository(LoyaltyContext dbContext) : base(dbContext)
        {
        }
        public DeviceRepository(IRegistrationRepository registrationRepository,LoyaltyContext dbContext) : base(dbContext)
        {
            _registrationRepository = registrationRepository;
        }

        public List<string> GetDevicesPushTokenList(string passTypeIdentifier, string serialNumber)
        {
            var pushTokens = new List<string>();
            var registrations = _registrationRepository.GetAll(d => d.PassTypeIdentifier == passTypeIdentifier && d.SerialNumber == serialNumber).ToList();
            foreach (var device in registrations)
            {
                pushTokens.Add(device.PushToken);
            }
            return pushTokens;
        }

    }
}
