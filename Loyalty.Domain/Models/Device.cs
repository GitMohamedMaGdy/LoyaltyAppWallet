using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loyalty.Domain.Models
{
   public class Device
    {
        public Device(string deviceLibraryIdentifier, string pushToken)
        {
            DeviceLibraryIdentifier = deviceLibraryIdentifier;
            PushToken = pushToken;
            Registrations = new HashSet<Registration>();
        }
        public string DeviceLibraryIdentifier { get; set; }
        public string PushToken { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}
 