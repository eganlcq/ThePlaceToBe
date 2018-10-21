using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
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

			imgLogo.Source = Constants.appImg + "logo.png";
			imgAccount.Source = Constants.appImg + "imgAccount.png";

			List<Beer> listBiere = RestService.Request<Beer>(RestService.dic, "selectOneBeer").Result;
			RemplitChampsBiere(listBiere[0]);

			List<Bar> listBar = RestService.Request<Bar>(RestService.dic, "selectBar").Result;

			NavigationPage.SetHasNavigationBar(this, false);
			double lat = 50.669204;
			double lon = 4.613774;
			Position pos = new Position(lat, lon);
			List<Pin> listPin = new List<Pin>();

			foreach(Bar bar in listBar) {

				Pin pin = new Pin {
					Type = PinType.Generic,
					Position = new Position(bar.Latitude, bar.Longitude),
					Label = bar.Nombar,
					Address = ""
				};
				listPin.Add(pin);
			}
			
			map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(0.15)));
			map.MapType = MapType.Street;
			foreach(Pin pin in listPin) {

				map.Pins.Add(pin);
			}
		}

		// CLIC SUR PROFIL
		private void ProfilMainPageTapped(object sender, EventArgs e) {
			this.Navigation.PushAsync(new AchievementPage.AchievementPage());
		}

		// BOUTON RETOUR EN ARRIERE
		private void BtnRetourClicked(object sender, EventArgs e) {
			this.Navigation.PopAsync();
		}

		// BOUTON 
		private void BtnAjoutFavorisClicked(object sender, EventArgs e) {
			// AJOUT PAGE FAVORIS
		}

		private void RemplitChampsBiere(Beer beer) {
			lblName.Text = beer.Nombiere;
			lblAlcool.Text = beer.Alcoolemie.ToString() + '%';
			lblSaveur.Text = beer.Typebiere;
			if(beer.Image != "" && beer.Image != null) {

				imgBeer.Source = Constants.beersImg + beer.Image;
			}
			else {

				imgBeer.Source = Constants.beersImg + "oneBeer.png";
			}
		}
	}
}