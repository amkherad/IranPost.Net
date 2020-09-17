using IranPost.Net.Enums;

namespace IranPost.Net.Dto.ChangeStatus
{
    public class ChangeStatusRequestDto
    {
        public string Id { get; set; }
        
        public OrderStates Status { get; set; }
    }
}