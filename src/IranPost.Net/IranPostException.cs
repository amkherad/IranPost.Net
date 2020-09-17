using System;
using System.Runtime.Serialization;

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
        
        public IranPostException(
            string message,
            Exception innerException
        )
            : base(message, innerException)
        {
        }
        
        protected IranPostException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        {
        }
    }
}