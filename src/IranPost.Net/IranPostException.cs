using System;
using System.Runtime.Serialization;

namespace IranPost.Net
{
    public class IranPostException : Exception
    {
        public enum ExceptionType
        {
            ValidationPostalCode,
            ValidationWeight,
            ValidationProductPrice,
            ValidationInvoiceNumber,
            ValidationEmpty,
        }
        
        public ExceptionType Type { get; set; }
        
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