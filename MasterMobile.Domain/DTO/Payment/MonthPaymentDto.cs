using Newtonsoft.Json;
using System;

namespace MasterMobile.Domain.DTO.Payment
{
    public class MonthPaymentDto
    {
        [JsonProperty("coast")]
        public int Coast { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
    }
}
