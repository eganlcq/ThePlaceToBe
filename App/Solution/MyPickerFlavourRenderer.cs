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
using Android.Graphics.Drawables;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ThePlaceToBe.Data;
using ThePlaceToBe.Droid;

[assembly: ExportRenderer(typeof(MyPickerFlavour), typeof(MyPickerFlavourRenderer))]
namespace ThePlaceToBe.Droid
{
    class MyPickerFlavourRenderer : PickerRenderer
    {
        public MyPickerFlavourRenderer(Context context) : base(context)
        {
        }

        private IElementController ElementController => Element as IElementController;
        private AlertDialog _dialog;


        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.SetPadding(20, 20, 20, 10);
                Control.SetTextSize(Android.Util.ComplexUnitType.Px, 35);

                Control.Click += Control_Click;
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {

            Picker model = Element;

            var picker = new NumberPicker(Context);
            if (model.Items != null && model.Items.Any())
            {
                picker.MaxValue = model.Items.Count - 1;
                picker.MinValue = 0;
                picker.SetDisplayedValues(model.Items.ToArray());
                picker.WrapSelectorWheel = false;
                picker.DescendantFocusability = DescendantFocusability.BlockDescendants;
                picker.Value = model.SelectedIndex;

            }

            var layout = new LinearLayout(Context) { Orientation = Orientation.Vertical };
            layout.AddView(picker);

            ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

            var builder = new AlertDialog.Builder(Context);
            builder.SetView(layout);
            builder.SetTitle(model.Title ?? "");
            builder.SetNegativeButton(global::Android.Resource.String.Cancel, (s, a) =>
            {
                ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                // It is possible for the Content of the Page to be changed when Focus is changed.
                // In this case, we'll lose our Control.
                Control?.ClearFocus();
                _dialog = null;
            });

            builder.SetPositiveButton(global::Android.Resource.String.Ok, (s, a) =>
            {
                ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged.
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                {
                    if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                        Control.Text = model.Items[Element.SelectedIndex];
                    ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
                    // It is also possible for the Content of the Page to be changed when Focus is changed.
                    // In this case, we'll lose our Control.
                    Control?.ClearFocus();
                }
                _dialog = null;
            });



            _dialog = builder.Create();
            _dialog.DismissEvent += (ssender, args) =>
            {
                ElementController?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
            };
            _dialog.Show();
            _dialog.Window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.White));
            //modify the color of the button in the dialog box
            Android.Widget.Button nbutton = _dialog.GetButton((int)Android.Content.DialogButtonType.Positive);
            nbutton.SetTextColor(Android.Graphics.Color.ParseColor("#3367b0"));
            nbutton = _dialog.GetButton((int)Android.Content.DialogButtonType.Negative);
            nbutton.SetTextColor(Android.Graphics.Color.ParseColor("#3367b0"));

            nbutton.TextSize = 16f;

        }

    }
}