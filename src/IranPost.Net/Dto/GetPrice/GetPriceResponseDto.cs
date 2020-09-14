namespace IranPost.Net.Dto.GetPrice
{
    public class GetPriceResponseDto
    {
        public decimal Price { get; set; }
        
        public decimal PriceWithoutCod { get; set; }
        
        public decimal Tax { get; set; }
    }
}