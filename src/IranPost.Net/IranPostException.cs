using System;

namespace IranPost.Net
{
    public class IranPostException : Exception
    {
        public IranPostException() : base("An error occurred in IranPost API call.")
        {
        }
        
        public IranPostException(
            string message
        ) : base(message)
        {
        }
    }
}