using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ProductPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductPage : ContentPage
    {
        public ProductPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            double lat = 50.669204;
            double lon = 4.613774;
            Position pos = new Position(lat, lon);
            Pin pin = new Pin
            {
                Type = PinType.Generic,
                Position = pos,
                Label = "Beer Bar",
                Address = "Grand-rue 5"
            };
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromKilometers(0.1)));
            map.MapType = MapType.Street;
            map.Pins.Add(pin);

            List<Beer> listBiere = RestService.RequestT<Beer>(RestService.dic, "selectOneBeer").Result;
            RemplitChampsBiere(listBiere);

            
        }

        // CLIC SUR PROFIL
        private void ProfilProductPageTapped(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new AchievementPage.AchievementPage());
        }

        // BOUTON RETOUR EN ARRIERE
        private void BtnRetourClicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        // BOUTON 
        private void BtnAjoutFavorisClicked(object sender, EventArgs e)
        {
            // AJOUT PAGE FAVORIS
        }


        // remplissage des champs d'information sur une bière
        private void RemplitChampsBiere(List<Beer> listBiere)
        {
            lblName.Text = listBiere[0].Nombiere;
            lblAlcool.Text = listBiere[0].Alcoolemie.ToString() + '%';
            lblSaveur.Text = listBiere[0].Typebiere;
            imgBeer.Source = listBiere[0].Image;
        }
    }
}