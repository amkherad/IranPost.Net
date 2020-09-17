using System;

namespace IranPost.Net.Dto.Billing
{
    public class BillingRequestDto
    {
        public DateTimeOffset Min { get; set; }
        
        public DateTimeOffset Max { get; set; }
    }
}