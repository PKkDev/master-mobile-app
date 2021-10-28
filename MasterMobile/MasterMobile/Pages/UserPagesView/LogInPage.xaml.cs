using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.UserPagesView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        LogInPageViewModel _viewModel;

        public LogInPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new LogInPageViewModel();
        }

        protected override void OnAppearing()
        {
            _viewModel.OnAppearing();
            base.OnAppearing();
        }

    }
}