using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using ThePlaceToBe.Views.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.MainPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage() {

			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			imgLogo.Source = Constants.appImg + "logo.png";
			imgAccount.Source = Constants.appImg + User.currentUser.Photo;
			imgLoupe.Source = Constants.appImg + "loupe.png";

			flavourPicker.Items.Add("Pouet");
			flavourPicker.Items.Add("Pouet");
			flavourPicker.Items.Add("Pouet");
			
			RestService.dic = new Dictionary<string, string>();
			List<Beer> listBiere = RestService.Request<Beer>(RestService.dic, "selectBeer").Result;
			int nbBiere = listBiere.Count();
			double nbRow = Math.Ceiling(nbBiere / 3.0);
			double nbColumn = 3;
			int count = 0;
			
			for (int i = 0; i < nbRow; i++) {

				RowDefinition row = new RowDefinition {
					Height = 100
				};
				beerGrid.RowDefinitions.Add(row);
			}


			for(int x = 0; x < nbRow; x++) {

				for(int y = 0; y < nbColumn && count < nbBiere; y++, count ++) {
					
					Beer beer = listBiere[count];
					string imgBiere = listBiere[count].Image;
					Image img;
					TapGestureRecognizer tap = new TapGestureRecognizer();
					

					if (imgBiere != "" && imgBiere != null) {
						
						img = new Image {
							Source = Constants.beersImg + imgBiere,
							Margin = new Thickness(5, 5)
						};
					}
					else {

						img = new Image {
							Source = Constants.beersImg + "oneBeer.png",
							Margin = new Thickness(5, 5)
						};
					}

					tap.Tapped += (s, e) => BeerTapped(s, e, beer);
					img.GestureRecognizers.Add(tap);
					beerGrid.Children.Add(img, y, x);
				}
			}
		}

		private void BeerTapped(object s, EventArgs e, Beer beer) {

			RestService.dic = new Dictionary<string, string> {

				{"idBiere", beer.Idbiere.ToString() }
			};
			this.Navigation.PushAsync(new ProductPage.ProductPage());
		}

		// CLIC SUR PROFIL
		private void ProfilMainPageTapped(object sender, EventArgs e) {
			this.Navigation.PushAsync(new AchievementPage.AchievementPage());
		}

		// CLIC SUR UN BIERE
		private void BiereTapped(object sender, EventArgs e) {
			this.Navigation.PushAsync(new ProductPage.ProductPage());
		}

		// CLIC BOUTON SCAN
		private void ScanClicked(object sender, EventArgs e) {
			this.Navigation.PushAsync(new ScanPage.ScanPage());
		}
	}

}