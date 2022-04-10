using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Dtos
{
    public class ApplePassData
    {

        public class RegisterationModel
        {
            public string PushToken { get; set; }
        }
        public class SerialNumbersData
        {
            public SerialNumbersData()
            {
                SerialNumbers = new List<string>();
            }
            public string LastUpdated { get; set; }
            public List<string> SerialNumbers { get; set; }
        }
        public class LogPayload
        {
            public string[] logs { get; set; }
        }

    }
}
