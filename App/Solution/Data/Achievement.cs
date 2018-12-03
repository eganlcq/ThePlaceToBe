using System;
using System.Collections.Generic;
using System.Text;
using ThePlaceToBe.Data;
using ThePlaceToBe.Views.ConnexionPage;
using ThePlaceToBe.Views.MainPage;
using ThePlaceToBe.Views.ProductPage;

namespace ThePlaceToBe.Data {
	//MODIFIER LE NAMESPACE !!!!
	public static class Achievement {
		const string ID_SUCCES_BIRTHDAY = "22";

		const string ID_SUCCES_ONE_ACH = "15";
		const string ID_SUCCES_FIVE_ACH = "16";
		const string ID_SUCCES_TEN_ACH = "17";
		const string ID_SUCCES_FIFTEEN_ACH = "18";

		const string ID_SEARCH_ONE_ACH = "1";
		const string ID_SEARCH_FIVE_ACH = "2";
		const string ID_SEARCH_TEN_ACH = "3";
		const string ID_SEARCH_FIFTEEN_ACH = "4";
		const string ID_SEARCH_TWENTY_ACH = "5";

		const string ID_FAV_ONE_ACH = "7";
		const string ID_FAV_TEN_ACH = "8";
		const string ID_FAV_ALL_ACH = "9";

		const string ID_SUCCES_ONE_BIERE = "10";
		const string ID_SUCCES_FIVE_BIERE = "11";
		const string ID_SUCCES_TEN_BIERE = "12";
		const string ID_SUCCES_FIFTEEN_BIERE = "13";
		const string ID_SUCCES_TWENTY_BIERE = "14";

		public static bool CheckAnniversaireDate(DateTime date) {
			return (date.Month == DateTime.Now.Month && date.Day == DateTime.Now.Day);
		}


		public static void CheckRecherche(MainPage mp) {
			RestService.dic = new Dictionary<string, string> {

				{"idUser", User.currentUser.Iduser.ToString() }
			};
			int a = RestService.Request<Count>(RestService.dic, "countResearch").Result[0].Counter;

			if (a == 1) {
				modifyDic(ID_SEARCH_ONE_ACH);
				mp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Faire une recherche'  ! ", "cool");
			}
			else if (a == 5) {
				modifyDic(ID_SEARCH_FIVE_ACH);
				mp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Faire 5 recherche'  ! ", "cool");
			}
			else if (a == 10) {
				modifyDic(ID_SEARCH_TEN_ACH);
				mp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Faire 10 recherche'  ! ", "cool");
			}
			else if (a == 15) {
				modifyDic(ID_SEARCH_FIFTEEN_ACH);
				mp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Faire 15 recherche'  ! ", "cool");
			}
			else if (a == 20) {
				modifyDic(ID_SEARCH_TWENTY_ACH);
				mp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Faire 20 recherche'  ! ", "cool");
			}
			RestService.Request(RestService.dic, "insertSucces");

			if (CheckNbreAch(1, ID_SUCCES_ONE_ACH)) {
				mp.DisplayAlert("Bravo", "Vous avez 1 succes ! ", "cool");
			}
			else if (CheckNbreAch(5, ID_SUCCES_FIVE_ACH)) {
				mp.DisplayAlert("Bravo", "Vous avez 5 succes ! ", "cool");
			}
			else if (CheckNbreAch(10, ID_SUCCES_TEN_ACH)) {
				mp.DisplayAlert("Bravo", "Vous avez 10 succes ! ", "cool");
			}
			else if (CheckNbreAch(15, ID_SUCCES_FIFTEEN_ACH)) {
				mp.DisplayAlert("Bravo", "Vous avez 15 succes ! ", "cool");
			}

		}

		public static void CheckFavoris(ProductPage pp) {
			RestService.dic = new Dictionary<string, string>
					{
						{"idUser", "1" }, //User.currentUser.id.ToString()
                    };
			int a = RestService.Request<Count>(RestService.dic, "countFav").Result[0].Counter;

			RestService.dic = new Dictionary<string, string>();
			List<Beer> listBiere = RestService.Request<Beer>(RestService.dic, "countFav").Result;
			if (a == 1) {
				modifyDic(ID_FAV_ONE_ACH);
				pp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Ajouter 1 bière en favoris'  ! ", "cool");
			}
			else if (a == 10) {
				modifyDic(ID_FAV_TEN_ACH);
				pp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Ajouter 10 bières en favoris'  ! ", "cool");
			}
			else if (a == listBiere.Count) {
				modifyDic(ID_FAV_ALL_ACH);
				pp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Ajouter plein de bières en favoris'  ! ", "cool");
			}
			//check nbr achievment
			if (CheckNbreAch(1, ID_SUCCES_ONE_ACH)) {
				pp.DisplayAlert("Bravo", "Vous avez 1 succes ! ", "cool");
			}
			else if (CheckNbreAch(5, ID_SUCCES_FIVE_ACH)) {
				pp.DisplayAlert("Bravo", "Vous avez 5 succes ! ", "cool");
			}
			else if (CheckNbreAch(10, ID_SUCCES_TEN_ACH)) {
				pp.DisplayAlert("Bravo", "Vous avez 10 succes ! ", "cool");
			}
			else if (CheckNbreAch(15, ID_SUCCES_FIFTEEN_ACH)) {
				pp.DisplayAlert("Bravo", "Vous avez 15 succes ! ", "cool");
			}
		}

		public static bool CheckAnniversaire(ConnexionPage connexion) {
			if (CheckAnniversaireDate(Convert.ToDateTime(User.currentUser.Datenaiss))) {
				modifyDic(ID_SUCCES_BIRTHDAY);
				RestService.Request(RestService.dic, "insertSucces");
				//rajouter insert de achievement
				if (CheckNbreAch(1, ID_SUCCES_ONE_ACH)) {
					connexion.DisplayAlert("Bravo", "Vous avez 1 succes ! ", "cool");
				}
				else if (CheckNbreAch(5, ID_SUCCES_FIVE_ACH)) {
					connexion.DisplayAlert("Bravo", "Vous avez 5 succes ! ", "cool");
				}
				else if (CheckNbreAch(10, ID_SUCCES_TEN_ACH)) {
					connexion.DisplayAlert("Bravo", "Vous avez 10 succes ! ", "cool");
				}
				else if (CheckNbreAch(15, ID_SUCCES_FIFTEEN_ACH)) {
					connexion.DisplayAlert("Bravo", "Vous avez 15 succes ! ", "cool");
				}
				return true;
			}
			return false;
		}

		public static void CheckRajoutBiere(ConnexionPage connexion) {
			if (User.currentUser.NbAjout > User.currentUser.NbAjoutPrecedent && User.currentUser.NbAjout >= 1 && User.currentUser.NbAjoutPrecedent < 1) {
				modifyDic(ID_SUCCES_ONE_BIERE);
				connexion.DisplayAlert("Merci", "Vous avez obtenu le succes 'Rajouter une bière dans l'application' ! ", "ok");
			}
			else if (User.currentUser.NbAjout > User.currentUser.NbAjoutPrecedent && User.currentUser.NbAjout >= 5 && User.currentUser.NbAjoutPrecedent < 5) {
				modifyDic(ID_SUCCES_FIVE_BIERE);
				connexion.DisplayAlert("Merci", "Vous avez obtenu le succes 'Rajouter 5 bière dans l'application' ! ", "ok");
			}
			else if (User.currentUser.NbAjout > User.currentUser.NbAjoutPrecedent && User.currentUser.NbAjout >= 10 && User.currentUser.NbAjoutPrecedent < 10) {
				modifyDic(ID_SUCCES_TEN_BIERE);
				connexion.DisplayAlert("Merci", "Vous avez obtenu le succes 'Rajouter 10 bière dans l'application' ! ", "ok");
			}
			else if (User.currentUser.NbAjout > User.currentUser.NbAjoutPrecedent && User.currentUser.NbAjout >= 15 && User.currentUser.NbAjoutPrecedent < 15) {
				modifyDic(ID_SUCCES_FIFTEEN_BIERE);
				connexion.DisplayAlert("Merci", "Vous avez obtenu le succes 'Rajouter 15 bière dans l'application' ! ", "ok");
			}
			else if (User.currentUser.NbAjout > User.currentUser.NbAjoutPrecedent && User.currentUser.NbAjout >= 20 && User.currentUser.NbAjoutPrecedent < 20) {
				modifyDic(ID_SUCCES_TWENTY_BIERE);
				connexion.DisplayAlert("Merci", "Vous avez obtenu le succes 'Rajouter 20 bière dans l'application' ! ", "ok");
			}
			RestService.Request(RestService.dic, "insertSucces");


			if (CheckNbreAch(1, ID_SUCCES_ONE_ACH)) {
				connexion.DisplayAlert("Bravo", "Vous avez 1 succes ! ", "cool");
			}
			else if (CheckNbreAch(5, ID_SUCCES_FIVE_ACH)) {
				connexion.DisplayAlert("Bravo", "Vous avez 5 succes ! ", "cool");
			}
			else if (CheckNbreAch(10, ID_SUCCES_TEN_ACH)) {
				connexion.DisplayAlert("Bravo", "Vous avez 10 succes ! ", "cool");
			}
			else if (CheckNbreAch(15, ID_SUCCES_FIFTEEN_ACH)) {
				connexion.DisplayAlert("Bravo", "Vous avez 15 succes ! ", "cool");
			}
		}

		public static int ReturnNbrAch() {
			RestService.dic = new Dictionary<string, string>
			{
			   {"idUser", User.currentUser.Iduser.ToString() },
			};
			int a = RestService.Request<Count>(RestService.dic, "countAch").Result[0].Counter;
			return a;
		}

		public static bool CheckNbreAch(int nbrAch, string idSucces) {
			if (ReturnNbrAch() == nbrAch) {
				modifyDic(idSucces);
				RestService.Request(RestService.dic, "insertSucces");
				return true;
			}
			return false;
		}

		public static void modifyDic(string idSucces) {
			RestService.dic = new Dictionary<string, string>
					{
						{"idUser", User.currentUser.Iduser.ToString() },
						{"idSucces", idSucces}
					};
		}
	}
}

