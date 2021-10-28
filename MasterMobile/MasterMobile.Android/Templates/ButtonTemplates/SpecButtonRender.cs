using Android.Content;
using MasterMobile.Droid.Templates.ButtonTemplates;
using MasterMobile.Templates.ButtonTemplates;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SpecButton), typeof(SpecButtonRender))]
namespace MasterMobile.Droid.Templates.ButtonTemplates
{
    public class SpecButtonRender : Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer
    {
        public SpecButtonRender(Context context)
            : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.SetHeight(10);
                Control.SetWidth(20);
                Control.SetTextColor(Color.Red.ToAndroid());
                Control.SetPadding(30, 30, 30, 30);
                Control.SetShadowLayer(5, 3, 3, Color.Black.ToAndroid());
                Control.SetAllCaps(true);
            }
        }
    }
}