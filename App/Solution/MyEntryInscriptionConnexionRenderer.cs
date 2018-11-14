using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using ThePlaceToBe.Data;
using ThePlaceToBe.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(MyEntryInscriptionConnexion), typeof(MyEntryInscriptionConnexionRenderer))]
namespace ThePlaceToBe.Droid
{
    public class MyEntryInscriptionConnexionRenderer : EntryRenderer
    {
        public MyEntryInscriptionConnexionRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background.SetColorFilter(Android.Graphics.Color.Rgb(51, 103, 176), PorterDuff.Mode.SrcAtop);
                
                //Control.SetCursorVisible(false);
            }

        }
    }
}