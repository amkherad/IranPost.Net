using IranPost.Net.Enums;

namespace IranPost.Net.Dto.NewOrder2
{
    public class NewOrder2RequestDto
    {
        public OrderTip OrderTip { get; set; }
        
        public DetDto Det { get; set; }
    }
}