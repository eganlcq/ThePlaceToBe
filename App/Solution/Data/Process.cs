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
		// BEGINNING OF THE REGISTRATION METHODE (INSCRIPTION)

		public static string VerifyInscription(string firstNameUser, string nameUser, string pseudoUser, string emailUser, string pswdUser, string confirmPswdUser, string birthDate) {

			// Check if all the fields have benn filled
			if (CheckInfo(null, firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate)
				&& 
				CheckInfo("", firstNameUser, nameUser, pseudoUser, emailUser, pswdUser, confirmPswdUser, birthDate)) {

				// Check if the user has entered the same password twice
				if (CheckSamePswd(pswdUser, confirmPswdUser)) {

					// Check if the pseudo already exists in the database
					if (CheckPseudo(pseudoUser)) {

						// Check if the email already exist in the database
						if (CheckMail(emailUser)) {

                            // check if the structure of the email is correct
							if(CheckMailRegex(emailUser)) {

								// The end of the verification is reached
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

        // Check if all the fields have benn filled
        public static bool CheckInfo(string obj, string firstNameUser, string nameUser, string pseudoUser, string emailUser, string pswdUser, string confirmPswdUser, string birthDate) {

			if (firstNameUser != obj && nameUser != obj && pseudoUser != obj && emailUser != obj && pswdUser != obj && confirmPswdUser != obj && birthDate != obj) {

				return true;
			}
			else return false;
		}

        // Check if the user has entered the same password twice
        public static bool CheckSamePswd(string pswdUser, string confirmPswdUser) {

			if (pswdUser == confirmPswdUser) return true;
			else return false;
		}

        // Check if the pseudo already exists in the database
        public static bool CheckPseudo(string pseudoUser) {

			RestService.dic = new Dictionary<string, string> {

				{ "pseudo", pseudoUser }
			};
			Check check = RestService.Request<Check>(RestService.dic, "checkPseudo").Result[0];
			return check.Verif;
		}

        // Check if the email already exist in the database
        public static bool CheckMail(string emailUser) {

			RestService.dic = new Dictionary<string, string> {

				{ "email", emailUser }
			};
			Check check = RestService.Request<Check>(RestService.dic, "checkMail").Result[0];
			return check.Verif;
		}

        // check if the structure of the email is correct
        public static bool CheckMailRegex(string emailUser) {

			Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = regex.Match(emailUser);
			if(match.Success) return true;
			else return false;
		}

        // END OF THE REGISTRATION METHODE (INSCRIPTION)
        #endregion inscriptionMethods

        #region connexionMethods
        // BEGINNING OF THE CONNECTION METHOD

        // Check if the pseudo and the password are both correct
        public static bool CheckConnexion(string pseudoUser, string pswdUser) {
			RestService.dic = new Dictionary<string, string>
			{
				{ "pseudo", pseudoUser },
				{ "pswd", pswdUser}
			};
			return RestService.Request<Check>(RestService.dic, "checkPassword").Result[0].Verif;
		}

        // Return the list of the beer types
		public static List<string> GetBeerTypes() {
			Dictionary<string, string> dic = new Dictionary<string, string>();

			//return RestService.Request<List<string>>(dic, "getBeerTypes");
			List<string> listeTypes = new List<string> { "amer", "sucré" };
			return listeTypes;

		}

        // END OF THE CONNECTION METHOD
        #endregion connexionMethods

        #region mainPageMethods

        // BEGINNING OF THE MAIN PAGE METHOD

        // Check if the picture already exists and if not, display a default picture
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

        // END OF THE MAIN PAGE METHOD

        #endregion mainPageMethods

        #region achievementsMethods

        // BEGINNING OF THE ACHIEVEMENTS PAGE METHOD

        public static List<User> GetUser(string idUser) {

			RestService.dic = RestService.dic = new Dictionary<string, string> {

				{ "idUser", idUser }
			};
			return RestService.Request<User>(RestService.dic, "selectUser").Result;
		}

        // END OF THE ACHIEVEMENTS PAGE METHOD

        #endregion

        #region prodcutMethods

        // BEGINNING OF THE PRODUCT PAGE METHOD

        public static bool CheckFavorite(int idBeer, int idUser) {

			RestService.dic = new Dictionary<string, string> {

				{ "idBiere", idBeer.ToString() },
				{ "idUser", idUser.ToString() }
			};
			Check check = RestService.Request<Check>(RestService.dic, "checkFavoris").Result[0];
			return check.Verif;
		}

        // END OF THE PRODUCT PAGE METHOD

        #endregion
    }
}
