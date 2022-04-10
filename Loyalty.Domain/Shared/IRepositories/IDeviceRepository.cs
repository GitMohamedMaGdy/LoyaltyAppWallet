using Loyalty.Domain;
using Loyalty.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Shared.IRepositories
{
    public interface IDeviceRepository : IRepository<Device>
    {
        List<string> GetDevicesPushTokenList(string passTypeIdentifier, string serialNumber);

    }
}

