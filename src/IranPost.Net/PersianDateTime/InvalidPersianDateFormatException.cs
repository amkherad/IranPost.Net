using System;

namespace IranPost.Net.PersianDateTime
{
    internal class InvalidPersianDateFormatException : Exception
    {
        public InvalidPersianDateFormatException(
            string message
        )
            : base(message)
        {
        }

        public InvalidPersianDateFormatException()
            : base()
        {
        }
    }
}