using Newtonsoft.Json;
using System.Collections.Generic;

namespace MasterMobile.Domain.DTO.Payment
{
    public class PaymentCategoryDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageSource")]
        public byte[] ImageSource { get; set; }

        [JsonProperty("subCategory")]
        public List<PaymentSubCategoryDto> SubCategory { get; set; }

        public PaymentCategoryDto() { }
    }

    public class PaymentSubCategoryDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("imageSource")]
        public byte[] ImageSource { get; set; }

        public PaymentSubCategoryDto() { }
    }
}
