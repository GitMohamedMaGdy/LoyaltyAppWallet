using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loyalty.Domain.Models
{
    public class Registration
    {
        public Registration(string passTypeIdentifier, string deviceLibraryIdentifier, string serialNumber, string pushToken)
        {
            PassTypeIdentifier = passTypeIdentifier;
            DeviceLibraryIdentifier = deviceLibraryIdentifier;
            SerialNumber = serialNumber;
            PushToken = pushToken;
        }
        //public int Id { get; set; }
        public string DeviceLibraryIdentifier { get; set; }
        public string PassTypeIdentifier { get; set; }
        public string SerialNumber { get; set; }
        public string PushToken { get; set; }
        public virtual Pass pass { get; set; }
        public virtual Device Device { get; set; }



    }
}
