using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Database;
using Android.Content;
using Xamarin.Essentials;
using Plugin.Fingerprint;

namespace MasterMobile.Droid
{
    [Activity(Label = "SmsReader", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity FormsContext { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            FormsContext = this;

            //var status = Permissions.CheckStatusAsync<Permissions.Sms>();
            //var statusR = status.Result;

            //var status2 = Permissions.RequestAsync<Permissions.Sms>();
            //var status2R = status.Result;

            //string INBOX = "content://sms/inbox";
            //string[] reqCols = new string[] { "_id", "thread_id", "address", "person", "date", "body", "type" };
            //Android.Net.Uri uri = Android.Net.Uri.Parse(INBOX);

            //var cursor21 = _activity.ContentResolver.Query(uri, reqCols, null, null, null);

            //var cursor2 = ContentResolver.Query(uri, reqCols, null, null, null);

            //var loader = new CursorLoader(this, uri, reqCols, null, null, null);
            //ICursor cursor = (ICursor)loader.LoadInBackground();// ContentResolver.Query(uri, reqCols, null, null);

            //if (cursor21.MoveToFirst())
            //{
            //    do
            //    {
            //        var msg = cursor21.GetString(cursor.GetColumnIndex(reqCols[5]));
            //    }
            //    while (cursor.MoveToNext());
            //}
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
            }
            else
                base.OnBackPressed();
            // Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        public override void OnRequestPermissionsResult(
            int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}