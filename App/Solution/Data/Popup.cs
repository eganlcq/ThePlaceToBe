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
		public static List<string> listString = new List<string>();

		public static void DisplayPopup() {

			for(int i = 0; i < listString.Count; i++) {

				Toast toast = Toast.MakeText(Android.App.Application.Context, listString[i], ToastLength.Long);
				var view = toast.View;
				TextView textView = (TextView)view.FindViewById(Resource.Id.Message);
				toast.SetGravity(GravityFlags.Bottom | GravityFlags.FillHorizontal, 0, i * 130);
				textView.SetTextColor(Android.Graphics.Color.LightGray);
				view.SetBackgroundColor(Android.Graphics.Color.DarkGray);
				toast.Show();
			}
			listString = new List<string>();
		}
    }
}
