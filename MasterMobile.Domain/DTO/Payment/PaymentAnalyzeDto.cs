using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MasterMobile.Domain.DTO.Payment
{
    public class PaymentAnalyzeDto
    {
        [JsonProperty("startPeriod")]
        public DateTime StartPeriod { get; set; }
        [JsonProperty("endPeriod")]
        public DateTime EndPeriod { get; set; }

        [JsonProperty("monthPay")]
        public int MonthPay { get; set; }
        [JsonProperty("dynamic")]
        public double Dynamic { get; set; }

        [JsonProperty("categoryPays")]
        public List<CategoryPayDto> CategoryPays { get; set; }

        public PaymentAnalyzeDto(DateTime startPeriod, DateTime endPeriod)
        {
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            MonthPay = 0;
            Dynamic = 0.0;
            CategoryPays = new List<CategoryPayDto>();
        }
    }

    public class CategoryPayDto
    {
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("pay")]
        public int Pay { get; set; }

        public CategoryPayDto(string categoryName, int pay)
        {
            CategoryName = categoryName;
            Pay = pay;
        }
    }
}
