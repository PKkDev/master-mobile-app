using System.Collections.Generic;
using Xamarin.Forms;

namespace MasterMobile.Domain.Model.Payments
{
    public class PaymentCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ImageSource ImageSource { get; set; }

        public List<PaymentCategory> SubCategory { get; set; }

        public PaymentCategory() { }
    }
}
