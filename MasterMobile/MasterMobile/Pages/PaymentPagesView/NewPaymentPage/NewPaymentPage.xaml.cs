using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.PaymentPagesView.NewPaymentPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPaymentPage : ContentPage
    {
        NewPaymentViewModel _viewModel;

        public NewPaymentPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new NewPaymentViewModel();
        }
    }
}