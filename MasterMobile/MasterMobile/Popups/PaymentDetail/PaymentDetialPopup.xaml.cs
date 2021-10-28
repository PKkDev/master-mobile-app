using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Popups.PaymentDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentDetialPopup : PopupPage
    {
        PaymentDetailPopupViewModel _viewModel;

        public PaymentDetialPopup(int paymentId)
        {
            InitializeComponent();

            BindingContext = _viewModel = new PaymentDetailPopupViewModel(paymentId);
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
            base.OnAppearing();
        }
    }
}