using System;
using Xamarin.Forms;

namespace MasterMobile.Domain.Model.Payments
{
    public class PaymentDetailModel
    {
        public int Cost { get; set; }

        public DateTime Date { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public string Description { get; set; }

        public ImageSource ImageSource { get; set; }

        public PaymentDetailModel() { }
    }
}
