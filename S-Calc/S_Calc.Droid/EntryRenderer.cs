using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Util;
using S_Calc;
using S_Calc.Droid;
using Android.Views.InputMethods;
using Android.Content.PM;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace S_Calc.Droid
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Click += (sender, evt) => {
                    new Handler().Post(delegate
                    {
                        var imm = (InputMethodManager)Control.Context.GetSystemService(Android.Content.Context.InputMethodService);
                        var result = imm.HideSoftInputFromWindow(Control.WindowToken, 0);
                    });
                };
                Control.FocusChange += (sender, evt) => {
                    new Handler().Post(delegate
                    {
                        var imm = (InputMethodManager)Control.Context.GetSystemService(Android.Content.Context.InputMethodService);
                        var result = imm.HideSoftInputFromWindow(Control.WindowToken, 0);
                    });
                };
            }
        }
    }
}