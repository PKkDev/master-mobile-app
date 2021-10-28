using MasterMobile.MVVM;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MasterMobile.Pages.SmsReaderPagesVIew
{
    public class SmsReaderViewModel : BaseViewModel
    {
        public RelyCommand CheckSmsPremissions { get; set; }
        public RelyCommand StartGetSmsPremissions { get; set; }

        public PermissionStatus SmsPremission { get; set; } = PermissionStatus.Unknown;

        public SmsReaderViewModel()
        {
            Title = "Sms Reader";

            Task.Run(async () => await CheckPremissonsForSms());
            Task.WaitAll();

            CheckSmsPremissions = new RelyCommand(async o => await CheckPremissonsForSms());
            StartGetSmsPremissions = new RelyCommand(async o => await Permissions.RequestAsync<Permissions.Sms>());
        }

        private async Task CheckPremissonsForSms()
        {
            SmsPremission = await Permissions.CheckStatusAsync<Permissions.Sms>();
            OnPropertyChanged("SmsPremission");
        }
    }
}
