using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ScanPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
		public ScanPage() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		// BOUTON OUI SCAN
		private void BtnOUIClicked(object sender, EventArgs e) {

			// EXECUTER CODE AJOUT BIERE

			this.Navigation.PopAsync();
		}

		// BOUTON NON SCAN
		private void BtnNONClicked(object sender, EventArgs e) {
			// PAS DE CODE A EXECUTER CAR BIERE PAS OK
			this.Navigation.PopAsync();
		}
	}
}