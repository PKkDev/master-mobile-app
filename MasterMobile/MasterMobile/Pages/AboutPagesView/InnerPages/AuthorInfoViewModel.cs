using MasterMobile.MVVM;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MasterMobile.Pages.AboutPagesView.InnerPages
{
    public class AuthorInfoViewModel : BaseViewModel
    {
        public ICommand OpenWebCommand { get; }
        public RelyCommand DoTest { get; }

        public AuthorInfoViewModel()
        {
            Title = "Author";

            var OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            var DoTest = new RelyCommand((param) => { });

        }
    }
}
