using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DonneesView : ContentView
	{
        public DonneesView(string idUser)
        {
            InitializeComponent();
            RestService.dic = RestService.dic = new Dictionary<string, string> {

                {"idUser", idUser}
            };
            List<User> listUser = RestService.RequestUser(RestService.dic, "selectUser").Result;
            User user = listUser[0];
            nom.Text = user.nom;
            prenom.Text = user.prenom;
            email.Text = user.email;
        }
    }
}