using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using MasterMobile.Infrastructure.Services;
using MasterMobile.MVVM;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using System;

namespace MasterMobile.Pages.UserPagesView
{
    public class LogInPageViewModel : BaseViewModel
    {
        public UserService UserService => DependencyService.Get<UserService>();

        public RelyCommand LogInCommand { get; set; }
        public RelyCommand FingerLogInCommand { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { OnSetNewValue(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { OnSetNewValue(ref _password, value); }
        }


        public LogInPageViewModel()
        {
            LogInCommand = new RelyCommand(async (param) => await LogInAsync());
            FingerLogInCommand = new RelyCommand(async (paam) =>
            {
                await OnFingerLogInCommand();
            });
        }

        private async Task OnFingerLogInCommand()
        {
            var aviability = await CrossFingerprint.Current.IsAvailableAsync();

            if (!aviability)
            {
                MessagingCenter.Send<string>("Error", "Biomatric is not available");
                return;
            }

            var authResult = await CrossFingerprint.Current.AuthenticateAsync(
                new AuthenticationRequestConfiguration("Auth", " I wouild like use your biometric please"));

            if (authResult.Authenticated)
            {
                CheckAutoLogin();
            }
        }

        private async Task LogInAsync()
        {
            var cred = await UserService.Authorize(Username, Password);

            if (cred != null)
            {
                await SecureStorage.SetAsync("user", Username);
                await SecureStorage.SetAsync("pass", Password);
                await SecureStorage.SetAsync("token", cred.Token);
                await Shell.Current.GoToAsync("//PaymentPage");
            }
            else
            {
                MessagingCenter.Send<string>("Error", "Wrong Login/Password");
                SecureStorage.RemoveAll();
            }
        }

        public async void OnAppearing()
        {
            var user = await SecureStorage.GetAsync("user");
            var pass = await SecureStorage.GetAsync("pass");
            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                await Task.Delay((int)TimeSpan.FromSeconds(2).TotalMilliseconds);
                await OnFingerLogInCommand();
            }
        }

        //private async void StartMyAuth()
        //{
        //    if (Device.RuntimePlatform == Device.iOS)
        //    {
        //        AuthType = DependencyService.Get<IBiometricAuthenticateService>().GetAuthenticationType();
        //        if (!AuthType.Equals("None"))
        //        {
        //            Desc = "Please use " + AuthType + " to unlock";
        //            if (AuthType.Equals("TouchId") || AuthType.Equals("FaceId"))
        //            {
        //                await GetAuthResults();
        //            }
        //        }
        //    }
        //    if (Device.RuntimePlatform == Device.Android)
        //    {
        //        if (DependencyService.Get<IBiometricPieAuthenticate>().CheckSDKGreater29())
        //        {
        //            bool res = DependencyService.Get<IBiometricAuthenticateService>().FingerprintEnabled();
        //            if (res)
        //            {
        //                //subscribe for biometricpromt response from Android
        //                MessagingCenter.Subscribe<object>("BiometricPrompt", "Success", (sender) =>
        //                {
        //                    MessagingCenter.Unsubscribe<object>("BiometricPrompt", "Success");
        //                    BackgroundColor = Color.Green;
        //                    Desc = "TouchID authentication success";
        //                });
        //                MessagingCenter.Subscribe<object>("BiometricPrompt", "Fail", (sender) =>
        //                {
        //                    MessagingCenter.Unsubscribe<object>("BiometricPrompt", "Fail");
        //                    BackgroundColor = Color.Red;
        //                    Desc = "TouchId authentication fail";
        //                });

        //                //call biomtricprompt dependency service
        //                DependencyService.Get<IBiometricPieAuthenticate>().RegisterOrAuthenticate();
        //            }
        //            else
        //            {
        //                //biometric enrolled in device
        //                BackgroundColor = Color.Gray;
        //                Desc = "No biomtrics enrolled in device";
        //            }
        //        }
        //        else
        //        {
        //            //lower device than pie sdk
        //            //this also support biometric prompt by android yet we lack by XamarinForms
        //            bool res = DependencyService.Get<IBiometricAuthenticateService>().FingerprintEnabled();
        //            if (res)
        //            {
        //                //check for user have gives access to finger print for this app
        //                //app permision stored locally
        //                FingerprintDroid = true;
        //                await DependencyService.Get<IBiometricAuthenticateService>().AuthenticateUserIDWithTouchID();

        //                MessagingCenter.Subscribe<string>("Auth", "Success", (sender) =>
        //                {
        //                    BackgroundColor = Color.Green;
        //                    Desc = "TouchID authentication success";
        //                });
        //                MessagingCenter.Subscribe<string>("Auth", "Fail", (sender) =>
        //                {
        //                    BackgroundColor = Color.Red;
        //                    Desc = "TouchId authentication fail";
        //                });

        //            }
        //            else
        //            {
        //                BackgroundColor = Color.Red;
        //                Desc = "Biometric not supported on this device or no fingerprint enrolled";
        //                FingerprintDroid = false;
        //            }
        //        }
        //    }
        //}

        //private async Task GetAuthResults()
        //{
        //    //todo according to Auth type change the authenticationmethod in interface if face id or touch id
        //    //string AuthType = DependencyService.Get<IFingerprintAuthService>().GetAuthenticationType();
        //    var result = await DependencyService.Get<IBiometricAuthenticateService>().AuthenticateUserIDWithTouchID();
        //    if (result)
        //    {
        //        if (AuthType.Equals("TouchId"))
        //        {
        //            BackgroundColor = Color.Green;
        //            Desc = "TouchID authentication success";
        //        }
        //        else if (AuthType.Equals("FaceId"))
        //        {
        //            BackgroundColor = Color.Green;
        //            Desc = "FaceID authentication success";
        //        }
        //    }
        //    else
        //    {
        //        BackgroundColor = Color.Red;
        //        Desc = "Authentication failed";
        //    }
        //}

        private async void CheckAutoLogin()
        {
            var user = await SecureStorage.GetAsync("user");
            var pass = await SecureStorage.GetAsync("pass");
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessagingCenter.Send<string>("Error", "Needed authorize");
            }
            else
            {
                Username = user;
                Password = pass;
                await LogInAsync();
            }
        }
    }
}
