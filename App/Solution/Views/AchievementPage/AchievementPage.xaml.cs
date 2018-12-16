using System;
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
        const int LONGUEUR_PSEUDO_PETITE = 7;
        const int LONGUEUR_PSEUDO_MOYENNE = 12;
        const int LONGUEUR_PSEUDO_GRANDE = 18;

        const int FONTSIZE_TRES_PETITE = 12;
        const int FONTSIZE_PETITE = 20;
        const int FONTSIZE_MOYENNE = 25;
        const int FONTSIZE_GRANDE = 30;

        public AchievementPage()
        {
            InitializeComponent();
        }

        public AchievementPage(string idUser)
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Initialize the content of the xaml page
            Init(idUser);
        }

        // Display the personnal informations about the user
        public void DisplayData()
        {

            DataBox.Children.RemoveAt(0);
            var data = new DonneesView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(data);
            FavorisButton.BackgroundColor = Color.FromHex("3367b0");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
            DataButton.BackgroundColor = Color.FromHex("#4D97FF");

        }

        // Display the achievements about the user
        public void DisplayAchievement()
        {
            DataBox.Children.RemoveAt(0);
            var ach = new AchievementView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(ach);
            FavorisButton.BackgroundColor = Color.FromHex("#3367b0");
            AchievementButton.BackgroundColor = Color.FromHex("#4D97FF");
            DataButton.BackgroundColor = Color.FromHex("#3367b0");
        }

        // Display the favorites beers of the user
        public void DisplayFavoris()
        {
            DataBox.Children.RemoveAt(0);
            var ach = new FavorisView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(ach);
            FavorisButton.BackgroundColor = Color.FromHex("#4D97FF");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
            DataButton.BackgroundColor = Color.FromHex("#3367b0");
        }

        // This method is running when the retour button is clicked
        private void BtnRetourClicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        // Initialize the content of the xaml page
        private void Init(string idUser)
        {
            // return the user with the associated id
            List<User> listUser = Process.GetUser(idUser);

            pseudo.Text = listUser[0].Pseudo;
            pseudo.FontSize = InitPseudo(listUser[0].Pseudo);
            avatar.Source = Constants.userImg + listUser[0].Photo;
            var data = new DonneesView(User.currentUser.Iduser.ToString());
            DataBox.Children.Add(data);
            DataButton.BackgroundColor = Color.FromHex("#4D97FF");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");
            FavorisButton.BackgroundColor = Color.FromHex("3367b0");
            retour.Source = Constants.appImg + "retourBleu.png";
        }

        // initialize the font for the pseudo according to is size
        private int InitPseudo(string pseudoUser)
        {
            if (pseudoUser.Length <= LONGUEUR_PSEUDO_PETITE)
            {
                return FONTSIZE_GRANDE;
            }
            if (pseudoUser.Length <= LONGUEUR_PSEUDO_MOYENNE)
            {
                return FONTSIZE_MOYENNE;
                
            }
            if (pseudoUser.Length <= LONGUEUR_PSEUDO_GRANDE)
            {
                return FONTSIZE_PETITE;
                
            }
            else
            {
                return FONTSIZE_TRES_PETITE;
            }
        }
    }
}
