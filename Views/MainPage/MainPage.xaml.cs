using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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