using Xamarin.Forms;
using MasterMobile.Pages.PaymentPagesView.NewPaymentPage;
using MasterMobile.Domain.Model;
using MasterMobile.Pages.PaymentPagesView.PaymentAnayzePage;
using Xamarin.Essentials;

namespace MasterMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(NewPaymentPage), typeof(NewPaymentPage));
            Routing.RegisterRoute(nameof(PaymentAnayzePage), typeof(PaymentAnayzePage));

            MessagingCenter.Subscribe<ErrorMessage>(this, "Error", (sender) =>
            {
                DisplayAlert("Error", sender.Message, "ok");
            });
        }

        private async void Logout_MenuItem_Clicked(object sender, System.EventArgs e)
        {
            SecureStorage.RemoveAll();
            await Current.GoToAsync("//LoginPage");
        }
    }
}
