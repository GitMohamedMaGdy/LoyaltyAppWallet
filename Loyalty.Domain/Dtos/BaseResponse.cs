using System;
using System.Collections.Generic;
using System.Text;
using static Loyalty.Domain.Dtos.ApplePassData;

namespace Loyalty.Domain.Dtos
{
    public class BaseResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
    
    public class FileResponse : BaseResponse
    {
        public byte[] FileStream { get; set; }
    }
    public class SerialNumbersResponse : BaseResponse
    {
        public SerialNumbersResponse()
        {
            serialNumbers = new SerialNumbersData();
        }
        public SerialNumbersData serialNumbers   { get; set; }
    }
}
