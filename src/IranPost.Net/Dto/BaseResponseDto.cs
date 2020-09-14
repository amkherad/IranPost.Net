using IranPost.Net.Enums;

namespace IranPost.Net.Dto
{
    public class BaseResponseDto<TResult>
    {
        public PostErrors Error { get; set; }
        
        public bool Success { get; set; }
        
        public TResult Result { get; set; }
    }
}