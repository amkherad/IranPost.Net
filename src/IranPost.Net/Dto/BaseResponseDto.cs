using IranPost.Net.Enums;

namespace IranPost.Net.Dto
{
    public class BaseResponseDto<TResult>
    {
        public PostErrors Error { get; set; }

        public string ErrorDescription => IranPostHelpers.GetPostErrorMessage(Error);
        
        public bool Success { get; set; }
        
        public TResult Result { get; set; }
        
        
        public static implicit operator TResult(
            BaseResponseDto<TResult> input
        )
        {
            return input.Result;
        }

        public static implicit operator BaseResponseDto<TResult>(
            TResult input
        )
        {
            return new BaseResponseDto<TResult>
            {
                Success = true,
                Error = PostErrors.NoError,
                Result = input
            };
        }
    }
}