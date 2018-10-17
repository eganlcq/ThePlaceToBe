using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.InscriptionPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InscriptionPage : ContentPage
	{
		public InscriptionPage() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		private void InscriptionClicked(object sender, EventArgs e) {

		}
	}
}