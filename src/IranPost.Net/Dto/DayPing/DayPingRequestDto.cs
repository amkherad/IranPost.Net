using System;

namespace IranPost.Net.Dto.DayPing
{
    public class DayPingRequestDto
    {
        public DateTimeOffset ChangeDate { get; set; }
        public string LastId { get; set; }
        public int Number { get; set; }
    }
}