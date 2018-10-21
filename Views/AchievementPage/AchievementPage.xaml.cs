using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.AchievementPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AchievementPage : ContentPage
	{
		public AchievementPage() {
			InitializeComponent();
			var data = new DonneesView("1"); // A CHANGER DYNAMIQUEMENT
			DataBox.Children.Add(data);
			DataButton.BackgroundColor = Color.FromHex("#4D97FF");
			AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
			NavigationPage.SetHasNavigationBar(this, false);
		}

		public void displayData() {

			DataBox.Children.RemoveAt(0);
			var data = new DonneesView("1"); // A CHANGER DYNAMIQUEMENT
			DataBox.Children.Add(data);
			DataButton.BackgroundColor = Color.FromHex("#4D97FF");
			AchievementButton.BackgroundColor = Color.FromHex("#3367b0");

		}

		public void displayAchievement() {
			DataBox.Children.RemoveAt(0);
			var ach = new AchievementView("1"); // A CHANGER DYNAMIQUEMENT
			DataBox.Children.Add(ach);
			AchievementButton.BackgroundColor = Color.FromHex("#4D97FF");
			DataButton.BackgroundColor = Color.FromHex("#3367b0");
		}

		// RETOUR PAGE PRECEDENTE
		private void BtnRetourClicked(object sender, EventArgs e) {
			this.Navigation.PopAsync();
		}
	}
}