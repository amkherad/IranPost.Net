using IranPost.Net.Enums;

namespace IranPost.Net.Dto.NewOrder2
{
    public class NewOrder2RequestDto
    {
        public TipTypes OrderTip { get; set; }
        
        public DetDto Det { get; set; }
    }
}