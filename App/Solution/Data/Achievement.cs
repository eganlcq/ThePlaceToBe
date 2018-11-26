using System;
using System.Collections.Generic;
using System.Text;
using Test.Data;
using ThePlaceToBe.Data;

namespace Test
{
  //MODIFIER LE NAMESPACE !!!!
    public static class Achievement
    {
        const string ID_SUCCES_BIRTHDAY = "22";
        const string ID_SUCCES_ONE_ACH = "15";
        const string ID_SUCCES_FIVE_ACH = "16";
        const string ID_SUCCES_TEN_ACH = "17";
        const string ID_SUCCES_FIFTEEN_ACH = "18";

        public static bool CheckAnniversaireDate(DateTime date)
        {
            return (date.Month == DateTime.Now.Month && date.Day == DateTime.Now.Day);
        }


        public static void CheckFavoris(Views.ProductPage pp)
        {
            RestService.dic = new Dictionary<string, string>
                    {
                        {"idUser", "1" }, //User.currentUser.id.ToString()
                    };
           int a = RestService.Request<Count>(RestService.dic, "countFav").Result[0].Counter;

            RestService.dic = new Dictionary<string, string>();
            List<Beer> listBiere = RestService.Request<Beer>(RestService.dic, "countFav").Result;
            if (a == 1)
            {
                pp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Ajouter 1 bière en favoris'  ! ", "cool");
            }
            else if (a == 10)
            {
                pp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Ajouter 10 bières en favoris'  ! ", "cool");
            }
           else if(a == listBiere.Count)
            {
                pp.DisplayAlert("Bravo", "Vous avez débloqué le succès 'Ajouter toutes les bières en favoris'  ! ", "cool");
            }
            //check nbr achievment
            if(CheckNbreAch(1, ID_SUCCES_ONE_ACH))
            {
                pp.DisplayAlert("Bravo", "Vous avez 1 succes ! ", "cool");
            }
            else if (CheckNbreAch(5, ID_SUCCES_FIVE_ACH))
            {
                pp.DisplayAlert("Bravo", "Vous avez 5 succes ! ", "cool");
            }
            else if(CheckNbreAch(10, ID_SUCCES_TEN_ACH))
            {
                pp.DisplayAlert("Bravo", "Vous avez 10 succes ! ", "cool");
            }
            else if(CheckNbreAch(15, ID_SUCCES_FIFTEEN_ACH))
            {
                pp.DisplayAlert("Bravo", "Vous avez 15 succes ! ", "cool");
            }
        }

        public static bool CheckAnniversaire()
        {
            if (CheckAnniversaireDate(Convert.ToDateTime(User.currentUser.Datenaiss)))
            {
                //DisplayAlert("Achievement", "Joyeux anniversaire !!", "merci");
                RestService.dic = new Dictionary<string, string>
                    {
                        {"idUser", User.currentUser.Iduser.ToString() },
                        {"idSucces", ID_SUCCES_BIRTHDAY}
                    };
                RestService.Request(RestService.dic, "insertSucces");
                //rajouter insert de achievement
                return true;
            }
            return false;
        }

        public static int CheckRajoutBiere(int nbreDeBiere, string idSucces, Views.ConnexionPage connexion)
        {

            if (User.currentUser.NbAjout > User.currentUser.NbAjoutPrecedent && User.currentUser.NbAjout >= nbreDeBiere && User.currentUser.NbAjoutPrecedent < nbreDeBiere) //<======== HERON ET DAVID DOIVENT INCREMENTER LE nbAjout
            {
                //DisplayAlert("Ajout réussi", "Vous avez débloqué l'achievement 'ajouter 1 bières' ", "ok");
                RestService.dic = new Dictionary<string, string>
                    {
                        {"idUser", User.currentUser.Iduser.ToString() },
                        {"idSucces", idSucces}
                    };
                RestService.Request(RestService.dic, "insertSucces");
                if (CheckNbreAch(1, ID_SUCCES_ONE_ACH))
                {
                    connexion.DisplayAlert("Bravo", "Vous avez 1 succes ! ", "cool");
                }
                else if (CheckNbreAch(5, ID_SUCCES_FIVE_ACH))
                {
                    connexion.DisplayAlert("Bravo", "Vous avez 5 succes ! ", "cool");
                }
                else if (CheckNbreAch(10, ID_SUCCES_TEN_ACH))
                {
                    connexion.DisplayAlert("Bravo", "Vous avez 10 succes ! ", "cool");
                }
                else if (CheckNbreAch(15, ID_SUCCES_FIFTEEN_ACH))
                {
                    connexion.DisplayAlert("Bravo", "Vous avez 15 succes ! ", "cool");
                }
                //rajouter insert de achievement
                return nbreDeBiere;
            }
            return 0;
        }

        public static int ReturnNbrAch()
        {
            RestService.dic = new Dictionary<string, string>
            {
               {"idUser", User.currentUser.Iduser.ToString() },
            };
            int a =  RestService.Request<Count>(RestService.dic, "countAch").Result[0].Counter;
            return a;
        }

        public static bool CheckNbreAch(int nbrAch, string idSucces)
        {
            if(ReturnNbrAch() == nbrAch)
            {
                RestService.dic = new Dictionary<string, string>
                {
                        {"idUser", User.currentUser.Iduser.ToString() },
                        {"idSucces", idSucces}
                };
                RestService.Request(RestService.dic, "insertSucces");
                return true;
            }
            return false;
        }
    }
}

