using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Views.Database;
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

			flavourPicker.Items.Add("Pouet");
			flavourPicker.Items.Add("Pouet");
			flavourPicker.Items.Add("Pouet");
			
			List<Beer> listBiere = RestService.Request();
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
					
					string imgBiere = listBiere[count].Image;
					Image img;
					TapGestureRecognizer tap = new TapGestureRecognizer();
					tap.Tapped += (s, e) => {

						this.Navigation.PushAsync(new ProductPage.ProductPage());
					};

					if (imgBiere != "" && imgBiere != null) {

						imgBiere = imgBiere.Substring(12);
						img = new Image {
							Source = imgBiere,
							Margin = new Thickness(5, 5)
						};
						
						img.GestureRecognizers.Add(tap);
					}
					else {

						img = new Image {
							Source = "oneBeer.png",
							Margin = new Thickness(5, 5)
						};

						img.GestureRecognizers.Add(tap);
					}
					
					beerGrid.Children.Add(img, y, x);
				}
			}
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