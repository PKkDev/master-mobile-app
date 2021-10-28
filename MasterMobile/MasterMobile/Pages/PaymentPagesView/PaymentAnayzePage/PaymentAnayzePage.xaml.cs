using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.PaymentPagesView.PaymentAnayzePage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentAnayzePage : ContentPage
    {
        PaymentAnayzePageViewModel _viewModel;

        public PaymentAnayzePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new PaymentAnayzePageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}