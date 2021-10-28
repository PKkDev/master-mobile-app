using Android.App;
using Android.Content;
using Android.OS;

namespace MasterMobile.Droid
{
    [Activity(Label = "MasterApp", Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class PreLoadActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed() { }
    }
}