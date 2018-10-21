using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ConnexionPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnexionPage : ContentPage
	{
		public ConnexionPage() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			imgLogo.Source = Constants.appImg + "logo.png";
		}

		// NAVIGATION MAINPAGE
		private void ConnexionClicked(object sender, EventArgs e) {
			RestService.dic = new Dictionary<string, string> {
				{ "pseudo", "JEJ" },
				{ "pswd", "jej111jej222"}
			};
			List<User> listUser = RestService.Request<User>(RestService.dic, "userConnexion").Result;
			User.currentUser = listUser[0];
			this.Navigation.PushAsync(new MainPage.MainPage());
		}

		private void InscriptionClicked(object sender, EventArgs e) {
			this.Navigation.PushAsync(new InscriptionPage.InscriptionPage());
		}
	}
}