using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Dtos
{
    public class VoucherDetails
    {
        public string VoucherCode { get; set; }
        public string Status { get; set; }
        public string StoreName { get; set; }
        public string ClientName { get; set; }
        public string Category { get; set; }
        public int DiscountValue { get; set; }
        //public DateTime ExpiryDate { get; set; }
    }
}
