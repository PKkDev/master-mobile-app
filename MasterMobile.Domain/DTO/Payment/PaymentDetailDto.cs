using Newtonsoft.Json;
using System;

namespace MasterMobile.Domain.DTO.Payment
{
    public class PaymentDetailDto
    {
        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("imageSource")]
        public byte[] ImageSource { get; set; }

        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty("subCategoryName")]
        public string SubCategoryName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public PaymentDetailDto() { }
    }
}
