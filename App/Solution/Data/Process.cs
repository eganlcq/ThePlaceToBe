using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ThePlaceToBe.Data
{
    public static class Process
    {
		#region inscriptionMethods
		// DEBUT METHODES INSCRIPTION

		public static string VerifyInscription(string firstNameUser, string nameUser, string pseudoUser, string emailUser, string pswdUser, string confirmPswdUser, string birthDate) {

			// Vérification si tout les champs sont remplis
			if (CheckInfo(null, firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate)
				&& 
				CheckInfo("", firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate)) {

				// Vérification si l'utilisateur a rentré deux fois le même mot de passe
				if (CheckSamePswd(pswdUser, confirmPswdUser)) {

					// Vérifie si le pseudo existe déjà dans la base de donnée
					if (CheckPseudo(pseudoUser)) {

						// Vérifie si le mail existe déjà dans la base de donnée
						if (CheckMail(emailUser)) {

							if(CheckMailRegex(emailUser)) {

								// Effectue l'inscription de l'utilisateur
								return "OK";
							}
							else {

								return "Le mail est invalide.";
							}
						}
						else {

							return "Le mail existe déjà.";
						}
					}
					else {

						return "Le pseudo existe déjà.";
					}
				}
				else {

					return "Les mots de passe ne correspondent pas.";
				}
			}
			else {

				return "Veuillez remplir tous les champs s'il vous plaît";
			}
		}

		public static bool CheckInfo(string obj, string firstNameUser, string nameUser, string pseudoUser, string emailUser, string pswdUser, string confirmPswdUser, string birthDate) {

			if (firstNameUser != obj && nameUser != obj && pseudoUser != obj && emailUser != obj && pswdUser != obj && confirmPswdUser != obj && birthDate != obj) {

				return true;
			}
			else return false;
		}

		public static bool CheckSamePswd(string pswdUser, string confirmPswdUser) {

			if (pswdUser == confirmPswdUser) return true;
			else return false;
		}

		public static bool CheckPseudo(string pseudoUser) {

			RestService.dic = new Dictionary<string, string> {

				{ "pseudo", pseudoUser }
			};
			Check check = RestService.Request<Check>(RestService.dic, "checkPseudo").Result[0];
			return check.Verif;
		}

		public static bool CheckMail(string emailUser) {

			RestService.dic = new Dictionary<string, string> {

				{ "email", emailUser }
			};
			Check check = RestService.Request<Check>(RestService.dic, "checkMail").Result[0];
			return check.Verif;
		}

		public static bool CheckMailRegex(string emailUser) {

			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(emailUser);
			if(match.Success) return true;
			else return false;
		}

		// FIN METHODES INSCRIPTION
		#endregion inscriptionMethods

		#region connexionMethods
		// DEBUT METHODES CONNEXION

		// Vérification si le pseudo et le mot de passe sont corrects
		public static bool CheckConnexion(string pseudoUser, string pswdUser) {
			RestService.dic = new Dictionary<string, string>
			{
				{ "pseudo", pseudoUser },
				{ "pswd", pswdUser}
			};
			return RestService.Request<Check>(RestService.dic, "checkPassword").Result[0].Verif;
		}


		public static List<string> GetBeerTypes() {
			Dictionary<string, string> dic = new Dictionary<string, string>();

			//return RestService.Request<List<string>>(dic, "getBeerTypes");
			List<string> listeTypes = new List<string> { "amer", "sucré" };
			return listeTypes;

		}

		// FIN METHODES CONNEXION
		#endregion connexionMethods

		#region mainPageMethods

		// DEBUT METHODES MAIN PAGE

		// Vérifie si l'image existe, si elle n'existe pas, affiche l'image par défaut
		public static Image ChooseImage(string imgBiere) {

			Image img;

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
			return img;
		}

		// FIN METHODES MAIN PAGE

		#endregion mainPageMethods

		#region achievementsMethods

		// DEBUT METHODES ACHIEVEMENTS PAGE

		public static List<User> GetUser(string idUser) {

			RestService.dic = RestService.dic = new Dictionary<string, string> {

				{ "idUser", idUser }
			};
			return RestService.Request<User>(RestService.dic, "selectUser").Result;
		}

		// FIN METHODES ACHIEVEMENTS PAGE

		#endregion
	}
}
