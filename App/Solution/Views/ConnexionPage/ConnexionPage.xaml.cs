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
	public partial class ConnexionPage : ContentPage {
		public ConnexionPage() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            // Initialize the content of the xaml page
            Init();

		}

		// This method is running when the connection button is clicked
		private void ConnexionClicked(object sender, EventArgs e) {

			// Check if the pseudo and the password are correct
			bool isPasswordOrUsernameCorrect = CheckConnexion();
			// Complete the connection is a boolean is returned
			Connexion(isPasswordOrUsernameCorrect);
		}

		// Display the registration page (inscriptionPage)
		private void InscriptionClicked(object sender, EventArgs e) {

			this.Navigation.PushAsync(new InscriptionPage.InscriptionPage());
		}

        // Initialize the content of the xaml page
        private void Init() {

			imgLogo.Source = Constants.appImg + "logo.png";
			pseudoUser.Completed += (s, e) => pswdUser.Focus();
			pswdUser.Completed += (s, e) => ConnexionClicked(s, e);
		}

        // Check if the pseudo and the password are correct
        private bool CheckConnexion() {

			RestService.dic = new Dictionary<string, string> {
				{ "pseudo", pseudoUser.Text },
				{ "pswd", pswdUser.Text}
			};
			return RestService.Request<Check>(RestService.dic, "checkPassword").Result[0].Verif;
		}

        // Complete the connection is a boolean is returned
        private void Connexion(bool isPasswordOrUsernameCorrect) {

			if (!isPasswordOrUsernameCorrect) {

				lblError.Text = "Le pseudo ou le mot de passe est incorrect.";
			}
			else {

				List<User> listUser = RestService.Request<User>(RestService.dic, "userConnexion").Result;
				User.currentUser = listUser[0];
				
				Navigation.InsertPageBefore(new MainPage.MainPage(), this);
				Navigation.PopToRootAsync();
			}
		}
	}
}
