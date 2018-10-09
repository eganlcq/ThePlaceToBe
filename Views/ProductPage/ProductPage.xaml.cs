using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ProductPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProductPage : ContentPage
	{
		public ProductPage ()
		{
			InitializeComponent ();
			double lat = 50.669204;
			double lon = 4.613774;
			Position pos = new Position(lat, lon);
			Pin pin = new Pin {
				Type = PinType.Generic,
				Position = pos,
				Label = "Enorme étron fragile",
				Address = "Tu te calme"
			};
			map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(0.1)));
			map.MapType = MapType.Street;
			map.Pins.Add(pin);
		}
	}
}