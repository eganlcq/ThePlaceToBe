using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lelab1.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent();
		}

        public void displayData()
        {

            DataBox.Children.RemoveAt(0);
            var data = new View.DonnéesView();
            DataBox.Children.Add(data);
            DataButton.BackgroundColor =Color.FromHex("#4D97FF");
            AchievementButton.BackgroundColor = Color.FromHex("#3367b0");

        }

        public void displayAchievement()
        {
            DataBox.Children.RemoveAt(0);
            var ach = new View.AchievementView();
            DataBox.Children.Add(ach);
            AchievementButton.BackgroundColor = Color.FromHex("#4D97FF");
            DataButton.BackgroundColor = Color.FromHex("#3367b0");
        }
	}
}