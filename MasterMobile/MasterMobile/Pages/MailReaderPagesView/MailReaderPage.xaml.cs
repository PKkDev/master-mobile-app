using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.MailReaderPagesView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MailReaderPage : ContentPage
    {
        MailReaderViewModel _viewModel;

        public MailReaderPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MailReaderViewModel();
        }
    }
}