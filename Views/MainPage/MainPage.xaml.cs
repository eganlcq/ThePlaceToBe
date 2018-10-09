using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.MainPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();

			flavourPicker.Items.Add("Pouet");
			flavourPicker.Items.Add("Pouet");
			flavourPicker.Items.Add("Pouet");
		}
		
		private void BiereTapped(object sender, EventArgs e)
		{
		    this.Navigation.PushAsync(new ProductPage());
		}
	}
}
