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
    public partial class DonneesView : ContentView
    {
        public DonneesView(string idUser)
        {
            InitializeComponent();

            // return the user with the associated id
            List<User> listUser = Process.GetUser(idUser);

            User user = listUser[0];
            nom.Text = user.Nom;
            prenom.Text = user.Prenom;
            email.Text = user.Email;
        }
    }
}