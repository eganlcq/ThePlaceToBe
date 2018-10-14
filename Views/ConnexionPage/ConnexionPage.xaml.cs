using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ConnexionPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConnexionPage : ContentPage
	{
		public ConnexionPage() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		// NAVIGATION MAINPAGE
		private void ConnexionClicked(object sender, EventArgs e) {
			this.Navigation.PushAsync(new MainPage.MainPage());
		}

		private void InscriptionClicked(object sender, EventArgs e) {
			this.Navigation.PushAsync(new InscriptionPage.InscriptionPage());
		}
	}
}