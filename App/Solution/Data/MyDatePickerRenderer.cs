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
using ThePlaceToBe.Data;
using ThePlaceToBe.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using DatePicker = Xamarin.Forms.DatePicker;

/*
    An alternative has been found for our problem.
    This class is not use but we keep it in case of further changement.
*/

[assembly: ExportRenderer(typeof(MyDatePicker), typeof(MyDatePickerRenderer))]
namespace ThePlaceToBe.Droid
{
    class MyDatePickerRenderer : DatePickerRenderer
    {

        public MyDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            Control.SetTextColor(Android.Graphics.Color.Rgb(51, 103, 176));
            Control.SetCursorVisible(false);

        }
    }
}