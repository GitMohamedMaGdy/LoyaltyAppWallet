using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Models
{
    public class APILog
    {

        public int PKID { get; set; }
        public string Log { get; set; }
        public DateTime datetime { get; set; }
    }
}
