using System;

namespace IranPost.Net.PersianDateTime
{
    internal class InvalidPersianDateException : Exception
    {
        public InvalidPersianDateException()
            : base("Invalid persian datetime format.")
        {
        }

        public InvalidPersianDateException(
            string message
        )
            : base(message)
        {
        }
    }
}