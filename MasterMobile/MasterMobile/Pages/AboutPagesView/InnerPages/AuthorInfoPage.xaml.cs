using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.AboutPagesView.InnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorInfoPage : ContentPage
    {
        AuthorInfoViewModel _viewModel;

        public AuthorInfoPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AuthorInfoViewModel();
        }
    }
}