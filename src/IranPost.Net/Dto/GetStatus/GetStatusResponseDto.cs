using System;
using IranPost.Net.Enums;

namespace IranPost.Net.Dto.GetStatus
{
    public class GetStatusResponseDto
    {
        public OrderStates LatestStatus { get; set; }
        
        public DateTimeOffset LatestStatusDateTime { get; set; }
        
        public string Id { get; set; }
    }
}