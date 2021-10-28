using MasterMobile.MVVM;
using Xamarin.Essentials;

namespace MasterMobile.Pages.AboutPagesView.InnerPages
{
    public class ApplicationInfoViewModel : BaseViewModel
    {
        public RelyCommand OnShowSettingsUI { get; set; }

        public ApplicationInfoViewModel()
        {
            Title = "Application";

            OnShowSettingsUI = new RelyCommand(o => AppInfo.ShowSettingsUI());
        }

        public string AppName { get; set; } = AppInfo.Name;
        public string PackageName { get; set; } = AppInfo.PackageName;
        public string Version { get; set; } = AppInfo.VersionString;
        public string Build { get; set; } = AppInfo.BuildString;

    }
}
