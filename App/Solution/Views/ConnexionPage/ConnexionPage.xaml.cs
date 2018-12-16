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
        public ConnexionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Initialise des éléments présents dans le xaml
            Init();

        }

        // Méthode lancée lorsque le bouton de connexion est utilisé
        private void ConnexionClicked(object sender, EventArgs e)
        {

            // Vérification si le pseudo et le mot de passe sont corrects
            bool isPasswordOrUsernameCorrect = CheckConnexion();
            // Effectue la connexion si le booléen renvoie true
            Connexion(isPasswordOrUsernameCorrect);
        }

        // Affiche la page d'inscription
        private void InscriptionClicked(object sender, EventArgs e)
        {

            this.Navigation.PushAsync(new InscriptionPage.InscriptionPage());
        }

        // Initialise des éléments présents dans le xaml
        private void Init()
        {

            imgLogo.Source = Constants.appImg + "logo.png";
            pseudoUser.Completed += (s, e) => pswdUser.Focus();
            pswdUser.Completed += (s, e) => ConnexionClicked(s, e);
        }

        // Vérification si le pseudo et le mot de passe sont corrects
        private bool CheckConnexion()
        {

            RestService.dic = new Dictionary<string, string> {
                { "pseudo", pseudoUser.Text },
                { "pswd", pswdUser.Text}
            };
            return RestService.Request<Check>(RestService.dic, "checkPassword").Result[0].Verif;
        }

        // Effectue la connexion si le booléen renvoie true
        private void Connexion(bool isPasswordOrUsernameCorrect)
        {

            if (!isPasswordOrUsernameCorrect)
            {

                lblError.Text = "Le pseudo ou le mot de passe est incorrect.";
            }
            else
            {

                List<User> listUser = RestService.Request<User>(RestService.dic, "userConnexion").Result;
                User.currentUser = listUser[0];

                if (Achievement.CheckAnniversaire())
                {
                    Popup.listString.Add("Joyeux anniversaire, " + User.currentUser.Prenom + " " + User.currentUser.Nom + " !!!");
                }
                Achievement.CheckRajoutBiere();

                Navigation.InsertPageBefore(new MainPage.MainPage(), this);
                Navigation.PopToRootAsync();

                Popup.listString.Add("Bon retour, " + User.currentUser.Prenom + " " + User.currentUser.Nom);

                Popup.DisplayPopup();
            }
        }
    }
}