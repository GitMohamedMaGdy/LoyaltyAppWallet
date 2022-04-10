using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loyalty.Domain.Models
{
    public class Pass
    {
        public Pass(string passTypeIdentifier, string serialNumber, string value, DateTime lastUpdateAt, string authenticationToken)
        {
            PassTypeIdentifier = passTypeIdentifier;
            SerialNumber = serialNumber;
            Value = value;
            LastUpdateAt = lastUpdateAt;
            AuthenticationToken = authenticationToken;
            Registrations = new HashSet<Registration>();
        }
        public string PassTypeIdentifier { get; set; }
        public string SerialNumber { get; set; }
        public string Value { get; set; }
        public DateTime LastUpdateAt { get; set; }
        public string AuthenticationToken { get; set; }
        public ICollection<Registration> Registrations { get; set; }

    }
}
