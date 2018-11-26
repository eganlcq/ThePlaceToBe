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
	public partial class ProductPage : ContentPage {
		public ProductPage(Beer beer) {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            // Initialize the content of the xaml page
            Init(beer);
            // Initialize the elements necessary for the operation of the map
            InitMap();
		}

        // This method is running when the avatar picture is clicked
        private void ProfilMainPageTapped(object sender, EventArgs e) {
			this.Navigation.PushAsync(new AchievementPage.AchievementPage(User.currentUser.Iduser.ToString()));
		}

		// This method is running when th "retour" button is clicked
        // the scan is cancelled
		private void BtnRetourClicked(object sender, EventArgs e) {
			this.Navigation.PopAsync();
		}

		// This method is running when the button to add favorites is clicked
        // A beer is added as a favorite to a user
		private void AddFav(Beer beer) {
			RestService.dic = new Dictionary<string, string> {

				{ "idBeer", beer.Idbiere.ToString()},
				{ "idUser", User.currentUser.Iduser.ToString()}
			};
			RestService.Request(RestService.dic, "insertFavoris");
			btnFavoris.Text = "Remove Fav";
			btnFavoris.Clicked += (s, e) => RemoveFav(beer);
		}

        // This method is running when the button to remove favorites is clicked
        // The favorite beer is removed
        private void RemoveFav(Beer beer) {

			RestService.dic = new Dictionary<string, string> {

				{ "idBeer", beer.Idbiere.ToString()},
				{ "idUser", User.currentUser.Iduser.ToString()}
			};
			RestService.Request(RestService.dic, "deleteFavoris");
			btnFavoris.Text = "Add Fav";
			btnFavoris.Clicked += (s, e) => AddFav(beer);
		}

        // Display on the page the informations about the current beer
        private void DisplayBeerInfo(Beer beer) {

			lblName.Text = beer.Nombiere;
			lblAlcool.Text = beer.Alcoolemie.ToString() + '%';
			lblSaveur.Text = beer.Typebiere;
			if (beer.Image != "" && beer.Image != null) {

				imgBeer.Source = Constants.beersImg + beer.Image;
			}
			else {

				imgBeer.Source = Constants.beersImg + "oneBeer.png";
			}
		}

        // Initialize the content of the xaml page
        private void Init(Beer beer) {

			imgLogo.Source = Constants.appImg + "logo.png";
			imgAccount.Source = Constants.userImg + User.currentUser.Photo;
			lblPseudo.Text = User.currentUser.Pseudo;
			//Beer beer = RestService.Request<Beer>(RestService.dic, "selectOneBeer").Result[0];
			DisplayBeerInfo(beer);

			if(Process.CheckFavorite(beer.Idbiere, User.currentUser.Iduser)) {

				btnFavoris.Text = "Remove Fav";
				btnFavoris.Clicked += (s, e) => RemoveFav(beer);
			}
			else {
				btnFavoris.Text = "Add Fav";
				btnFavoris.Clicked += (s, e) => AddFav(beer);
			}
		}

        // Initialize the elements necessary for the operation of the map
        private void InitMap() {

			List<Bar> listBar = RestService.Request<Bar>(RestService.dic, "selectBar").Result;
			
			Position pos;
			double lat;
			double lon;
			List<Pin> listPin;

			if(listBar.Count != 0) {

				double sumLat = 0;
				double sumLon = 0;
				foreach (Bar bar in listBar) {

					sumLat += bar.Latitude;
					sumLon += bar.Longitude;
				}
				lat = sumLat / listBar.Count;
				lon = sumLon / listBar.Count;
				pos = new Position(lat, lon);
				listPin = AddPinsNeeded(listBar);
			}
			else {

				lat = 50.669204;
				lon = 4.613774;
				pos = new Position(lat, lon);
				listPin = new List<Pin>();
			}

			DisplayMap(pos, listPin);
		}

		// Add a defined number of pins in the list according to the number of bars received
		private List<Pin> AddPinsNeeded(List<Bar> listBar) {

			List<Pin> listPin = new List<Pin>();
			
			foreach (Bar bar in listBar) {

                Pin pin = new Pin {
                    Type = PinType.Generic,
                    Position = new Position(bar.Latitude, bar.Longitude),
                    Label = bar.Nombar,
                    Address = bar.Numero.ToString() + " " + bar.Rue
				};
				
				listPin.Add(pin);
			}

			return listPin;
		}

		// Add the pins on the map and change the position of the map
		private void DisplayMap(Position pos, List<Pin> listPin) {

			map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(0.15)));
			map.MapType = MapType.Street;
			foreach (Pin pin in listPin) {

				map.Pins.Add(pin);
			}
		}
	}
}