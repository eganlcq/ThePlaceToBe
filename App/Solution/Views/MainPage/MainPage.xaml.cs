using MySql.Data.MySqlClient;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
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
            // Initialise des éléments présents dans le xaml
            Init();
            // Initialise la grille qui contiendra la liste des bières se trouvant dans la base de données
            InitBeerGrid();
        }

        // Cette méthode se lance lorque l'on clique sur une image de bière
        private void BeerTapped(object s, EventArgs e, Beer beer)
        {

            RestService.dic = new Dictionary<string, string> {

                {"idBiere", beer.Idbiere.ToString() }
            };
            this.Navigation.PushAsync(new ProductPage.ProductPage());
        }

        // Cette méthode se lance lorque l'on clique sur l'image du user
        private void ProfilMainPageTapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AchievementPage.AchievementPage(User.currentUser.Iduser.ToString()));
        }

        // Cette méthode se lance lorque l'on clique sur le bouton de scan
        /*private void ScanClicked(object sender, EventArgs e) {
			this.Navigation.PushAsync(new ScanPage.ScanPage());
		}*/

        // Initialise des éléments présents dans le xaml
        private void Init()
        {

            imgLogo.Source = Constants.appImg + "logo.png";
            imgAccount.Source = Constants.userImg + User.currentUser.Photo;
            imgLoupe.Source = Constants.appImg + "loupe.png";
            lblPseudo.Text = User.currentUser.Pseudo;
            flavourPicker.Items.Add("Pouet");
            flavourPicker.Items.Add("Pouet");
            flavourPicker.Items.Add("Pouet");

            btnScan.Clicked += (s, e) => TakePhoto();
        }

        // Initialise la grille qui contiendra la liste des bières se trouvant dans la base de données
        private void InitBeerGrid()
        {

            RestService.dic = new Dictionary<string, string>();
            List<Beer> listBiere = RestService.Request<Beer>(RestService.dic, "selectBeer").Result;
            int nbBiere = listBiere.Count();
            double nbRow = Math.Ceiling(nbBiere / 3.0);
            double nbColumn = 3;

            // Ajoute un nombre de ligne proportionnel au nombre de bières récupérées de la base de données
            AddRows(nbRow);
            // Ajoute les images de bière dans les cases de la grille
            AddBeers(nbRow, nbColumn, nbBiere, listBiere);
        }

        // Ajoute un nombre de ligne proportionnel au nombre de bières récupérées de la base de données
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

        // Ajoute les images de bière dans les cases de la grille
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
                    // Vérifie si l'image existe, si elle n'existe pas, affiche l'image par défaut
                    Image img = ChooseImage(imgBiere);
                    tap.Tapped += (s, e) => BeerTapped(s, e, beer);
                    img.GestureRecognizers.Add(tap);
                    beerGrid.Children.Add(img, y, x);
                }
            }
        }

        // Vérifie si l'image existe, si elle n'existe pas, affiche l'image par défaut
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

        private async void TakePhoto()
        {

            await CrossMedia.Current.Initialize();

            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }

            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {

#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
                    DisplayAlert("No Camera", "No camera available.", "OK");
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {

                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 92
                });

                if (file == null) return;

                await DisplayAlert("File Location", file.Path, "OK");

                ImageSource img = ImageSource.FromStream(() => {

                    var streamImg = file.GetStream();
                    return streamImg;
                });

                byte[] bitmapData;
                var stream = new MemoryStream();
                file.GetStream().CopyTo(stream);
                bitmapData = stream.ToArray();
                var fileContent = new ByteArrayContent(bitmapData);

                await this.Navigation.PushAsync(new ScanPage.ScanPage(img, fileContent));

                if (File.Exists(file.Path))
                {

                    File.Delete(file.Path);
                }

                file.Dispose();
            }
            else
            {

                await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
            }
        }
    }
}