using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.AchievementPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FavorisView : ContentView {
		public FavorisView() {

			InitializeComponent();
		}

		public FavorisView(string idUser) {

			InitializeComponent();
			RestService.dic = RestService.dic = new Dictionary<string, string> {

				{"idUser", idUser}
			};
			List<Beer> listFavoris = RestService.Request<Beer>(RestService.dic, "selectFavoris").Result;

			int nbBiere = listFavoris.Count();
			double nbRow = Math.Ceiling(nbBiere / 3.0);
			double nbColumn = 3;
			int count = 0;

			for (int i = 0; i < nbRow; i++) {

				RowDefinition row = new RowDefinition {

					Height = 100
				};
				favoris.RowDefinitions.Add(row);
			}


			for (int x = 0; x < nbRow; x++) {

				for (int y = 0; y < nbColumn && count < nbBiere; y++, count++) {

					Beer beer = listFavoris[count];
					string imgBiere = listFavoris[count].Image;
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

					favoris.Children.Add(img, y, x);
				}
			}
		}

		private void BeerTapped(object s, EventArgs e, Beer beer) {

			RestService.dic = new Dictionary<string, string> {

				{"idBiere", beer.Idbiere.ToString() }
			};
			this.Navigation.PushAsync(new ProductPage.ProductPage(beer));
		}
	}
}