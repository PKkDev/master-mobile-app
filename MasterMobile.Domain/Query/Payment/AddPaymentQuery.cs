using Newtonsoft.Json;
using System;

namespace MasterMobile.Domain.Query.Payment
{
    public class AddPaymentQuery
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("subCategoryId")]
        public int SubCategoryId { get; set; }

        [JsonProperty("coast")]
        public int Coast { get; set; }

        [JsonProperty("datePay")]
        public DateTime DatePay { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public AddPaymentQuery() { }
    }
}
