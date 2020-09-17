using IranPost.Net.Dto.GetPrice;

namespace IranPost.Net.Dto.EditOrder
{
    public class EditOrderResponseDto : GetPriceResponseDto
    {
        public string InvoiceNumber { get; set; }
    }
}