using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MasterMobile.Pages.AboutPagesView.InnerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EthernetInfoPage : ContentPage
    {
        EthernetInfoVieModel _viewModel;

        public EthernetInfoPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EthernetInfoVieModel();
        }
    }
}