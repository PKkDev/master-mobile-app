using Newtonsoft.Json;
using System;

namespace MasterMobile.Domain.DTO.Payment
{
    public class LastPaymentDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("day")]
        public DateTime Day { get; set; }

        [JsonProperty("imageSource")]
        public byte[] ImageSource { get; set; }

        public LastPaymentDto() { }
    }
}
