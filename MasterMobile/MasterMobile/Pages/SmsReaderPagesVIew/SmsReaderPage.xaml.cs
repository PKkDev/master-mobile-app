using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.SmsReaderPagesVIew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmsReaderPage : ContentPage
    {
        SmsReaderViewModel _viewModel;

        public SmsReaderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new SmsReaderViewModel();
        }
    }
}