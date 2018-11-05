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
    public partial class InscriptionPage : ContentPage
    {
        public InscriptionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Initialise des éléments présents dans le xaml
            Init();
        }

        // Méthode lancée lorsque le bouton d'inscription est utilisé
        private void InscriptionClicked(object sender, EventArgs e)
        {

            // Vérification si tout les champs sont remplis
            if (CheckInfo(null) && CheckInfo(""))
            {

                // Vérification si l'utilisateur a rentré deux fois le même mot de passe
                if (CheckSamePswd())
                {

                    // Vérifie si le pseudo existe déjà dans la base de donnée
                    if (CheckPseudo())
                    {

                        // Vérifie si le mail existe déjà dans la base de donnée
                        if (CheckMail())
                        {

                            // Effectue l'inscription de l'utilisateur
                            Inscription();
                        }
                        else
                        {

                            lblError.Text = "Le mail existe déjà.";
                        }
                    }
                    else
                    {

                        lblError.Text = "Le pseudo existe déjà.";
                    }
                }
                else
                {

                    lblError.Text = "Les mots de passe ne correspondent pas.";
                }
            }
            else
            {

                lblError.Text = "Veuillez remplir tous les champs s'il vous plaît";
            }
        }

        // Vérification si tout les champs sont remplis
        bool CheckInfo(string obj)
        {

            if (firstNameUser.Text != obj && nameUser.Text != obj && pseudoUser.Text != obj && emailUser.Text != obj && pswdUser.Text != obj && confirmPswdUser.Text != obj && datePickerNaissance.ToString() != obj)
            {

                return true;
            }
            else return false;
        }

        // Vérification si l'utilisateur a rentré deux fois le même mot de passe
        bool CheckSamePswd()
        {

            if (pswdUser.Text == confirmPswdUser.Text) return true;
            else return false;
        }

        // Vérifie si le pseudo existe déjà dans la base de donnée
        private bool CheckPseudo()
        {

            RestService.dic = new Dictionary<string, string> {

                { "pseudo", pseudoUser.Text }
            };
            Check check = RestService.Request<Check>(RestService.dic, "checkPseudo").Result[0];
            return check.Verif;
        }

        // Vérifie si le mail existe déjà dans la base de donnée
        private bool CheckMail()
        {

            RestService.dic = new Dictionary<string, string> {

                { "email", emailUser.Text }
            };
            Check check = RestService.Request<Check>(RestService.dic, "checkMail").Result[0];
            return check.Verif;
        }

        // Initialise des éléments présents dans le xaml
        private void Init()
        {

            imgLogo.Source = Constants.appImg + "logo.png";
            firstNameUser.Completed += (s, e) => nameUser.Focus();
            nameUser.Completed += (s, e) => pseudoUser.Focus();
            pseudoUser.Completed += (s, e) => emailUser.Focus();
            emailUser.Completed += (s, e) => datePickerNaissance.Focus();
            pswdUser.Completed += (s, e) => confirmPswdUser.Focus();
            confirmPswdUser.Completed += (s, e) => InscriptionClicked(s, e);
        }

        // Effectue l'inscription de l'utilisateur
        private void Inscription()
        {

            RestService.dic = new Dictionary<string, string> {

                { "firstName", firstNameUser.Text },
                { "name", nameUser.Text },
                { "pseudo", pseudoUser.Text },
                { "email", emailUser.Text },
                { "password", pswdUser.Text },
                { "dateNaiss", datePickerNaissance.Date.Year + "-" + datePickerNaissance.Date.Month + "-" + datePickerNaissance.Date.Day }
            };
            RestService.Request(RestService.dic, "insertUser");

            RestService.dic = new Dictionary<string, string> {
                { "pseudo", pseudoUser.Text },
                { "pswd", pswdUser.Text}
            };
            User.currentUser = RestService.Request<User>(RestService.dic, "userConnexion").Result[0];
            this.Navigation.PushAsync(new MainPage.MainPage());
        }
    }
}