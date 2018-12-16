using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.MainPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        double nbRow;
        double nbColumn;
        int nbBiere;
        List<Beer> originalListBeer;
        List<Beer> tmpBeerList;

        public MainPage()
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            // Initialize the content of the xaml page
            RestService.dic = new Dictionary<string, string>();
            originalListBeer = RestService.Request<Beer>(RestService.dic, "selectBeer").Result;
            tmpBeerList = new List<Beer>();
            Init();
        }


        // Initialize the content of the xaml page
        private void Init()
        {

            imgLogo.Source = Constants.appImg + "logo.png";
            imgAccount.Source = Constants.userImg + User.currentUser.Photo;
            imgLoupe1.Source = Constants.appImg + "loupe.png";
            imgLoupe2.Source = Constants.appImg + "loupe.png";
            lblPseudo.Text = User.currentUser.Pseudo;
            InitPicker();
            flavourPicker.SelectedIndex = 0;
            disconnection.Source = Constants.appImg + "disconnect.png";
            menuPageImage.Source = Constants.appImg + "menuPageButton.png";
        }

        // Initialize the grid that will contain the list of the beers from tge database
        private void FillBeerGrid(string flav, string str)
        {

            FilterBeers(flav, str);

            nbBiere = tmpBeerList.Count();
            nbRow = Math.Ceiling(nbBiere / 3.0);
            nbColumn = 3;

            // Add a number of rows proportionnal at the number of beers from the database
            AddRows(nbRow);
            // Add the beers pictures in the grid
            AddBeers(nbRow, nbColumn, nbBiere, tmpBeerList);
        }

        // this method filters the beer according to the arguments selected by the user
        private void FilterBeers(string flav, string str)
        {
            // clear the temporary list
            tmpBeerList.Clear();

            // Filter WITH A TYPE AND A STRING
            if (flav != "ALL" && str != "" && str != null)
            {
                foreach (Beer beer in originalListBeer)
                {
                    if (beer.Typebiere == flav && (beer.Nombiere.ToLower().IndexOf(str.ToLower()) != -1))
                    {
                        tmpBeerList.Add(beer);
                    }

                }
            }
            // Filter WITH A STRING
            else if (flav == "ALL" && str != "" && str != null)
            {
                foreach (Beer beer in originalListBeer)
                {
                    if ((beer.Nombiere.ToLower().IndexOf(str.ToLower()) != -1))
                        tmpBeerList.Add(beer);
                }
            }
            // Filter WITH A TYPE
            else if ((flav != "ALL" && str == "") || (flav != "ALL" && str == null))
            {
                foreach (Beer beer in originalListBeer)
                {
                    if (beer.Typebiere == flav)
                        tmpBeerList.Add(beer);
                }
            }
            // NO FILTER
            else
            {
                foreach (Beer beer in originalListBeer)
                {
                    tmpBeerList.Add(beer);
                }
            }
        }

        // Initialize the content of the picker (allow to select a type of flavour for beers)
        private void InitPicker()
        {

            RestService.dic = new Dictionary<string, string>();
            List<Flavour> listeType = RestService.Request<Flavour>(RestService.dic, "selectType").Result;
            flavourPicker.Items.Add("ALL");
            foreach (Flavour type in listeType)
            {
                flavourPicker.Items.Add(type.Typebiere);
            }
        }

        // Add a number of rows proportionnal at the number of beers from the database
        private void AddRows(double nbRow)
        {

            for (int i = 0; i < nbRow; i++)
            {

                RowDefinition row = new RowDefinition
                {
                    Height = 100
                };
                beerGrid.RowDefinitions.Add(row);
            }
        }

        // Add the beers pictures in the grid
        private void AddBeers(double nbRow, double nbColumn, int nbBiere, List<Beer> listBiere)
        {

            int count = 0;
            for (int x = 0; x < nbRow; x++)
            {

                for (int y = 0; y < nbColumn && count < nbBiere; y++, count++)
                {

                    Beer beer = listBiere[count];
                    string imgBiere = listBiere[count].Image;
                    TapGestureRecognizer tap = new TapGestureRecognizer();
                    // check if the pitcure exists and if not, a default picture will be display
                    Image img = ChooseImage(imgBiere);
                    tap.Tapped += (s, e) => BeerTapped(s, e, beer);
                    img.GestureRecognizers.Add(tap);
                    beerGrid.Children.Add(img, y, x);
                }
            }
        }

        // Remove the beer in the grid
        private void RemoveAllBeer()
        {
            int count = 0;
            for (int x = 0; x < nbRow; x++)
            {
                for (int y = 0; y < nbColumn && count < nbBiere; y++, count++)
                {

                    beerGrid.Children.RemoveAt(0);
                }
            }

            foreach (var row in beerGrid.RowDefinitions.ToList())
            {
                beerGrid.RowDefinitions.Remove(row);
            }
        }

        // check if the pitcure exists and if not, a default picture will be display
        private Image ChooseImage(string imgBiere)
        {
            Image img;

            if (imgBiere != "" && imgBiere != null)
            {
                img = new Image
                {
                    Source = Constants.beersImg + imgBiere,
                    Margin = new Thickness(5, 5)
                };
            }
            else
            {
                img = new Image
                {
                    Source = Constants.beersImg + "oneBeer.png",
                    Margin = new Thickness(5, 5)
                };
            }
            return img;
        }


        // Display the beer in the grid with the parameter to apply a filter on the list
        private void DisplayBeerByTextAndFlavor(object sender, EventArgs e)
        {
            RemoveAllBeer();
            string flav = flavourPicker.SelectedItem.ToString();
            string str = textPicker.Text;
            FillBeerGrid(flav, str);
        }

        #region EVENEMENTS

        // This method is running when a picture of a beer is clicked
        private void BeerTapped(object s, EventArgs e, Beer beer)
        {

            Achievement.CheckRecherche();

            RestService.dic = new Dictionary<string, string> {

                {"idBiere", beer.Idbiere.ToString() }
            };
            this.Navigation.PushAsync(new ProductPage.ProductPage(beer));

            if (Popup.listString.Count != 0)
            {

                Popup.DisplayPopup();
            }
        }

        // This method is running when the avatar picture is clicked
        private void ProfilMainPageTapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AchievementPage.AchievementPage(User.currentUser.Iduser.ToString()));
        }

        // Disconnect the user
        private void Disconnect(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new ConnexionPage.ConnexionPage(), this);
            User.currentUser = null;
            Navigation.PopToRootAsync();
        }

        // Change the page to MenuPage
        private void MenuPageTapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MenuPage.MenuPage());
        }

        #endregion EVENEMENTS


    }
}