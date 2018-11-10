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
		public DonneesView (string idUser)
		{
			InitializeComponent();
			RestService.dic = new Dictionary<string, string> {

				{"idUser", idUser}
			};
			List<User> listUser = RestService.Request<User>(RestService.dic, "selectUser").Result;
			User user = listUser[0];
			nom.Text = user.Nom;
			prenom.Text = user.Prenom;
			email.Text = user.Email;
		}
	}
}