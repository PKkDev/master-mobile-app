using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MasterMobile.Domain.Model.Payments
{
    public class GroupedLastPyments : List<LastPayment>
    {
        public string Name { get; private set; }

        public GroupedLastPyments(string name, List<LastPayment> payments)
            : base(payments)
        {
            Name = name;
        }
    }

    public class LastPayment
    {
        public int Id { get; set; }

        public int Cost { get; set; }

        public string Time { get; set; }

        public DateTime Day { get; set; }

        public ImageSource ImageSource { get; set; }

        public LastPayment() { }
    }
}
