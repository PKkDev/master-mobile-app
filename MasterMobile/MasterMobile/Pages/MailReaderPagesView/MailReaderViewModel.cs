using MasterMobile.Domain.Model.MailReader;
using MasterMobile.Infrastructure.Services;
using MasterMobile.MVVM;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MasterMobile.Pages.MailReaderPagesView
{
    public class MailReaderViewModel : BaseViewModel
    {
        public MailReaderService MailReaderService => DependencyService.Get<MailReaderService>();

        public ObservableCollection<AnalyzeMessage> Logs { get; set; }

        private bool isStarted;
        public bool IsStarted
        {
            get { return isStarted; }
            set { OnSetNewValue(ref isStarted, value); }
        }

        private string mail;
        public string Mail
        {
            get { return mail; }
            set { OnSetNewValue(ref mail, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { OnSetNewValue(ref password, value); }
        }

        public RelyCommand StartCheckCommand { get; set; }

        public MailReaderViewModel()
        {
            Title = "Check Mail";

            Logs = new ObservableCollection<AnalyzeMessage>();
            Mail = "";
            Password = "";
            StartCheckCommand = new RelyCommand(async (p) => await OnStartCheckCommand());

            MessagingCenter.Subscribe<AnalyzeMessage>(this, "ResMailCheck", (sender) =>
            {
                Logs.Add(sender);
            });
        }

        public async Task OnStartCheckCommand()
        {
            Logs.Clear();
            await MailReaderService.StartCheck(Mail, Password);
        }
    }
}
