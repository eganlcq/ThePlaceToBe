using Android.Content;
using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace ThePlaceToBe.Data {
	public class MyEntryRenderer : EntryRenderer {
		public MyEntryRenderer(Context context) : base(context) {
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
			base.OnElementChanged(e);

			if (Control != null) {
				Control.Background.SetColorFilter(Android.Graphics.Color.Rgb(51, 103, 176), PorterDuff.Mode.SrcAtop);
			}

		}
	}
}
