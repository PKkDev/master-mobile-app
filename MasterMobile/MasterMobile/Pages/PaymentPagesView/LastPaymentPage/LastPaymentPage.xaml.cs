using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.PaymentPagesView.LastPaymentPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LastPaymentPage : ContentPage
    {
        LastPaymentViewModel _viewModel;

        public LastPaymentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LastPaymentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}