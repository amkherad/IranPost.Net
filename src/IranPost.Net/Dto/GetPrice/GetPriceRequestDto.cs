using IranPost.Net.Enums;

namespace IranPost.Net.Dto.GetPrice
{
    public class GetPriceRequestDto
    {
        public int Weight { get; set; }
        
        public int Price { get; set; }
        
        public string Shcode { get; set; }
        
        public int StateId { get; set; }
        
        public int CityId { get; set; }
        
        public TipTypes Tip { get; set; }
        
        public PaymentTypes PaymentType { get; set; }
    }
}