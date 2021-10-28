using MasterMobile.Pages.AboutPagesView.InnerPages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.AboutPagesView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : TabbedPage
    {
        public AboutPage()
        {
            InitializeComponent();

            Children.Add(new AuthorInfoPage());
            Children.Add(new EthernetInfoPage());
            Children.Add(new ApplicationInfoPage());
        }
    }
}