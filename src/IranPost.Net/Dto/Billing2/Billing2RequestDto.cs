using IranPost.Net.Enums;

namespace IranPost.Net.Dto.Billing2
{
    public class Billing2RequestDto
    {
        public string Id { get; set; }
        
        public TipTypes Tip { get; set; }
        
        public int Page { get; set; }
    }
}