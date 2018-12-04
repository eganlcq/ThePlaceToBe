using Android;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThePlaceToBe.Data
{
    class Popup
    {
		public static void DisplayPopup(string str) {

			Toast toast = Toast.MakeText(Android.App.Application.Context, str, ToastLength.Long);
			var view = toast.View;
			TextView textView = (TextView)view.FindViewById(Resource.Id.Message);
			toast.SetGravity(GravityFlags.Bottom | GravityFlags.FillHorizontal, 0, 0);
			textView.SetTextColor(Android.Graphics.Color.LightGray);
			view.SetBackgroundColor(Android.Graphics.Color.DarkGray);
			toast.Show();
		}
    }
}
