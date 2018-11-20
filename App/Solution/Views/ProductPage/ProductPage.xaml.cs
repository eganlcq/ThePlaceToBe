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
			// Initialise des éléments présents dans le xaml
			Init(beer);
			// Initialise des éléments pour faire fonctionner la map
			InitMap();
		}

		// Méthode lancée lorsque l'on clique sur l'image du user
		private void ProfilMainPageTapped(object sender, EventArgs e) {
			this.Navigation.PushAsync(new AchievementPage.AchievementPage(User.currentUser.Iduser.ToString()));
		}

		// Méthode lancée lorsque le bouton de retour est utilisé
		private void BtnRetourClicked(object sender, EventArgs e) {
			this.Navigation.PopAsync();
		}

		// Méthode lancée lorsque le bouton d'ajout de favoris est utilisé
		private void AddFav(object sender, EventArgs e, Beer beer) {
			RestService.dic = new Dictionary<string, string> {

				{ "idBeer", beer.Idbiere.ToString()},
				{ "idUser", User.currentUser.Iduser.ToString()}
			};
			RestService.Request(RestService.dic, "insertFavoris");
		}

		private void RemoveFav(object sender, EventArgs e, Beer beer) {

			RestService.dic = new Dictionary<string, string> {

				{ "idBeer", beer.Idbiere.ToString()},
				{ "idUser", User.currentUser.Iduser.ToString()}
			};
			RestService.Request(RestService.dic, "deleteFavoris");
		}

		// Affiche les info de la bière courante sur la page
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

		// Initialise des éléments présents dans le xaml
		private void Init(Beer beer) {

			imgLogo.Source = Constants.appImg + "logo.png";
			imgAccount.Source = Constants.userImg + User.currentUser.Photo;
			lblPseudo.Text = User.currentUser.Pseudo;
			//Beer beer = RestService.Request<Beer>(RestService.dic, "selectOneBeer").Result[0];
			DisplayBeerInfo(beer);

			if(Process.CheckFavorite(beer.Idbiere, User.currentUser.Iduser)) {

				btnFavoris.Text = "Remove Fav";
				btnFavoris.Clicked += (s, e) => RemoveFav(s, e, beer);
			}
			else {
				btnFavoris.Text = "Add Fav";
				btnFavoris.Clicked += (s, e) => AddFav(s, e, beer);
			}
		}

		// Initialise des éléments pour faire fonctionner la map
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

		// Ajoute un nombre définis de pins dans la liste en fonction du nombre de bar reçu
		private List<Pin> AddPinsNeeded(List<Bar> listBar) {

			List<Pin> listPin = new List<Pin>();
			
			foreach (Bar bar in listBar) {

				Pin pin = new Pin {
					Type = PinType.Generic,
					Position = new Position(bar.Latitude, bar.Longitude),
					Label = bar.Nombar,
					Address = ""
				};
				
				listPin.Add(pin);
			}

			return listPin;
		}

		// Ajoute des pins à la map et change la position de la map
		private void DisplayMap(Position pos, List<Pin> listPin) {

			map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(0.15)));
			map.MapType = MapType.Street;
			foreach (Pin pin in listPin) {

				map.Pins.Add(pin);
			}
		}
	}
}