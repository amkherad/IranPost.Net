using IranPost.Net.Enums;

namespace IranPost.Net.Dto.EditOrder
{
    public class EditOrderRequestDto
    {
        public string Id { get; set; }
        
        public string[] ProductNames { get; set; }
        
        public int Weight { get; set; }
        
        public PaymentTypes PaymentType { get; set; }
        
        public int Price { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        
        public string Email { get; set; }
        
        public string Tel { get; set; }
        
        public string PostalCode { get; set; }
    }
}