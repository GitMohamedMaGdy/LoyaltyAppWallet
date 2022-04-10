using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Dtos
{
    public class HttpClientResponseVM
    {
        public int StatusCode { get; set; }
        public JObject ResponseJson { get; set; }
    }
}
