﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.AchievementPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AchievementPage : ContentPage
    {
        public AchievementPage()
        {
            InitializeComponent();
        }

        public AchievementPage(string idUser)
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Initialise des éléments présents dans le xaml
            Init(idUser);
        }

        // Affiche les données personnelles de l'user
        public void DisplayData()
        {

            DataBox.Children.RemoveAt(0);
            var data = new DonneesView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(data);
            FavorisButton.BackgroundColor = Color.FromHex("3367b0");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
            DataButton.BackgroundColor = Color.FromHex("#4D97FF");

        }

        // Affiche les achievements de l'user
        public void DisplayAchievement()
        {
            DataBox.Children.RemoveAt(0);
            var ach = new AchievementView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(ach);
            FavorisButton.BackgroundColor = Color.FromHex("#3367b0");
            AchievementButton.BackgroundColor = Color.FromHex("#4D97FF");
            DataButton.BackgroundColor = Color.FromHex("#3367b0");
        }

        // Affiche les bières favorites de l'user
        public void DisplayFavoris()
        {
            DataBox.Children.RemoveAt(0);
            var ach = new FavorisView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(ach);
            FavorisButton.BackgroundColor = Color.FromHex("#4D97FF");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
            DataButton.BackgroundColor = Color.FromHex("#3367b0");
        }

        // Cette méthode s'exécute lorsque le bouton de retour est utilisé
        private void BtnRetourClicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        // Initialise des éléments présents dans le xaml
        private void Init(string idUser)
        {

            RestService.dic = RestService.dic = new Dictionary<string, string> {

               {"idUser", idUser}
            };
            List<User> listUser = RestService.Request<User>(RestService.dic, "selectUser").Result;
            pseudo.Text = listUser[0].Pseudo;
            var data = new DonneesView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(data);
            DataButton.BackgroundColor = Color.FromHex("#4D97FF");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
            FavorisButton.BackgroundColor = Color.FromHex("3367b0");
        }
    }
}