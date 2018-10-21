using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using ThePlaceToBe.Views.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            flavourPicker.Items.Add("Pouet");
            flavourPicker.Items.Add("Pouet");
            flavourPicker.Items.Add("Pouet");

            RestService.dic = new Dictionary<string, string>();
            List<Beer> listBiere = RestService.RequestT<Beer>(RestService.dic, "selectBeer").Result;
            int nbBiere = listBiere.Count();
            double nbRow = Math.Ceiling(nbBiere / 3.0);
            double nbColumn = 3;
            int count = 0;

            for (int i = 0; i < nbRow; i++)
            {

                RowDefinition row = new RowDefinition
                {
                    Height = 100
                };
                beerGrid.RowDefinitions.Add(row);
            }


            for (int x = 0; x < nbRow; x++)
            {

                for (int y = 0; y < nbColumn && count < nbBiere; y++, count++)
                {

                    Beer beer = listBiere[count];
                    string imgBiere = listBiere[count].Image;
                    Image img;
                    TapGestureRecognizer tap = new TapGestureRecognizer();


                    if (imgBiere != "" && imgBiere != null)
                    {

                        
                        img = new Image
                        {
                            Source = imgBiere,
                            Margin = new Thickness(5, 5)
                        };
                    }
                    else
                    {

                        img = new Image
                        {
                            Source = "oneBeer.png",
                            Margin = new Thickness(5, 5)
                        };
                    }

                    tap.Tapped += (s, e) => BeerTapped(s, e, beer);
                    img.GestureRecognizers.Add(tap);
                    beerGrid.Children.Add(img, y, x);
                }
            }

            // PARTIE PROFIL
            // CHARGER LE PROFIL AU CHARGEMENT
            List<User> utilisateur = RestService.RequestT<User>(RestService.dic, "selectProfil").Result;
            TapGestureRecognizer tap2 = new TapGestureRecognizer();
            tap2.Tapped += (s, e) => ProfilMainPageTapped(s, e, utilisateur[0]);
            imgAccount.Source = "theplacetobe.ovh/image/app/imgAccount.png"; //+ utilisateur[0].Photo.Substring(12);
            imgAccount.GestureRecognizers.Add(tap2);

        }

        private void BeerTapped(object s, EventArgs e, Beer beer)
        {

            RestService.dic = new Dictionary<string, string> {

                {"idBiere", beer.Idbiere.ToString() }
            };
            this.Navigation.PushAsync(new ProductPage.ProductPage());
        }

        // CLIC SUR PROFIL
        private void ProfilMainPageTapped(object sender, EventArgs e, User utilisateur)
        {
            RestService.dic = new Dictionary<string, string>
            {
                //{"objUser", utilisateur}
                {"objUser", utilisateur.Iduser.ToString()}
            };
            this.Navigation.PushAsync(new AchievementPage.AchievementPage());
        }

        // CLIC BOUTON SCAN
        private void ScanClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new ScanPage.ScanPage());
        }
    }
}