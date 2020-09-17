namespace IranPost.Net
{
    public struct RemoteServiceEndpoints
    {
        public string GetPriceRelativeUrl { get; set; }
        
        public string GetLastStatusUrl { get; set; }
        
        public string NewOrder2Url { get; set; }
        
        public string EditOrderUrl { get; set; }
        
        public string ChangeStatusUrl { get; set; }
        
        public string PingUrl { get; set; }
        
        public string DayPingUrl { get; set; }
        
        public string BillingUrl { get; set; }
        
        public string Billing2Url { get; set; }
        
        public string RejectExpUrl { get; set; }
        
        public string RejectIdUrl { get; set; }
    }
}