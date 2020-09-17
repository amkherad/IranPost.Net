using System;

namespace IranPost.Net.Dto.Billing
{
    public class BillingResponseDto
    {
        public string SettlementCode { get; set; }
        
        public DateTimeOffset DateTime { get; set; }
        
        public decimal Price { get; set; }
    }
}