using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntryFilterBeer), typeof(MyEntryFilterBeerRenderer))]
namespace ThePlaceToBe.Data {
	public class MyEntryFilterBeerRenderer : EntryRenderer {
		public MyEntryFilterBeerRenderer(Context context) : base(context) {
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
			base.OnElementChanged(e);

			if (Control != null) {
				Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
				Control.SetPadding(20, 20, 20, 10);
				Control.SetTextSize(Android.Util.ComplexUnitType.Px, 35);
			}
		}
	}
}
