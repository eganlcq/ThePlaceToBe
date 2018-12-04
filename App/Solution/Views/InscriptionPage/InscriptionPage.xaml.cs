using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.InscriptionPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InscriptionPage : ContentPage {
		public InscriptionPage() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			// Initialise des éléments présents dans le xaml
			Init();
		}

		// Méthode lancée lorsque le bouton d'inscription est utilisé
		private void InscriptionClicked(object sender, EventArgs e) {

			string response = Process.VerifyInscription(firstNameUser.Text, nameUser.Text, pseudoUser.Text, emailUser.Text, pswdUser.Text, confirmPswdUser.Text, datePickerNaissance.ToString());
			if (response == "OK") Inscription();
			else lblError.Text = response;
		}

		// Initialise des éléments présents dans le xaml
		private void Init() {

			imgLogo.Source = Constants.appImg + "logo.png";
			firstNameUser.Completed += (s, e) => nameUser.Focus();
			nameUser.Completed += (s, e) => pseudoUser.Focus();
			pseudoUser.Completed += (s, e) => emailUser.Focus();
			emailUser.Completed += (s, e) => datePickerNaissance.Focus();
			pswdUser.Completed += (s, e) => confirmPswdUser.Focus();
			confirmPswdUser.Completed += (s, e) => InscriptionClicked(s, e);
		}

		// Effectue l'inscription de l'utilisateur
		private async void Inscription() {

			RestService.dic = new Dictionary<string, string> {

				{ "firstName", firstNameUser.Text },
				{ "name", nameUser.Text },
				{ "pseudo", pseudoUser.Text },
				{ "email", emailUser.Text },
				{ "password", pswdUser.Text },
				{ "dateNaiss", datePickerNaissance.Date.Year + "-" + datePickerNaissance.Date.Month + "-" + datePickerNaissance.Date.Day }
			};
			await RestService.Request<User>(RestService.dic, "insertUser");

			RestService.dic = new Dictionary<string, string> {
				{ "pseudo", pseudoUser.Text },
				{ "pswd", pswdUser.Text}
			};
			User.currentUser = RestService.Request<User>(RestService.dic, "userConnexion").Result[0];
			Navigation.InsertPageBefore(new MainPage.MainPage(), Navigation.NavigationStack[0]);
			await Navigation.PopToRootAsync();

			Popup.DisplayPopup("Welcome, " + User.currentUser.Prenom + " " + User.currentUser.Nom);
		}
	}
}