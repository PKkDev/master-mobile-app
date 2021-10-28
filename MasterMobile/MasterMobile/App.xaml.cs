using MasterMobile.Infrastructure.Services;
using Xamarin.Forms;

namespace MasterMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<PaymentDataService>();
            DependencyService.Register<PaymentCategoriesDataService>();
            DependencyService.Register<PaymentAnalyzDataService>();
            DependencyService.Register<UserService>();

            DependencyService.Register<MailReaderService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        { }

        protected override void OnSleep()
        { }

        protected override void OnResume()
        { }
    }
}
