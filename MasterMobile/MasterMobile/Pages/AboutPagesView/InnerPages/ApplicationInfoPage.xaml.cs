using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.AboutPagesView.InnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApplicationInfoPage : ContentPage
    {
        ApplicationInfoViewModel _viewModel;

        public ApplicationInfoPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ApplicationInfoViewModel();
        }
    }
}