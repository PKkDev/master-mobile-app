using System;
using System.Collections.Generic;

namespace MasterMobile.Domain.Model.Payments
{
    public class PaymentAnalyzeRes
    {
        public DateTime StartPeriod { get; set; }
        public DateTime EndPeriod { get; set; }
        public string PeriodStr { get; set; }

        public TotalPay TotalPay { get; set; }

        public List<CategoryPay> CategoryPays { get; set; }

        public PaymentAnalyzeRes(DateTime startPeriod, DateTime endPeriod)
        {
            PeriodStr = startPeriod.ToString("MMMM");
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            TotalPay = new TotalPay(0, 0.0);
            CategoryPays = new List<CategoryPay>();
        }
    }

    public class TotalPay
    {
        public int Value { get; set; }
        public double Dynamic { get; set; }
        public bool IsBad { get; set; }

        public TotalPay(int monthPay, double dynamic)
        {
            Value = monthPay;
            Dynamic = dynamic;
            IsBad = dynamic > 0;
        }
    }

    public class CategoryPay
    {
        public string CategoryName { get; set; }
        public int Pay { get; set; }

        public CategoryPay(string categoryName, int pay)
        {
            CategoryName = categoryName;
            Pay = pay;
        }
    }
}
