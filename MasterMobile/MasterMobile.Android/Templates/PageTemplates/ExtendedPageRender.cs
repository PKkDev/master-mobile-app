using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MasterMobile.Droid.Templates.PageTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ExtendedPageRender))]
namespace MasterMobile.Droid.Templates.PageTemplates
{
    public class ExtendedPageRender : PageRenderer
    {

        public ExtendedPageRender(Context context)
            : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
        }

        public void Element_OnDoing(string parameter, Action<string> action)
        {
            // ...
            action("pass values");
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
        }

    }
}