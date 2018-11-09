using System;
using System.Collections.Generic;
using System.Text;

namespace ThePlaceToBe.Data
{
    public static class Process
    {
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

							// Effectue l'inscription de l'utilisateur
							return "OK";
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

		// FIN METHODES INSCRIPTION
	}
}
