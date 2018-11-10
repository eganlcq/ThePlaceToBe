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
    public partial class AchievementView : ContentView
    {
        public AchievementView(string idUser)
        {
            InitializeComponent();
            RestService.dic = new Dictionary<string, string> {

               {"idUser", idUser}
            };
            List<Succes> listSucces = RestService.Request<Succes>(RestService.dic, "selectAchievement").Result;
            foreach (Succes s in listSucces)
            {

                Label tmpLabelNom = new Label
                {
                    Text = s.Nom,
                    TextColor = Color.Black,
                    FontSize = 20
                };
                Label tmpLabelDescr = new Label
                {
                    Text = s.Descr,
                    TextColor = Color.Black,
                };

                StackLayout tmpStack = new StackLayout
                {
                    Spacing = 5
                };
                tmpStack.Children.Add(tmpLabelNom);
                tmpStack.Children.Add(tmpLabelDescr);
                Image tmpImg = new Image
                {
                    Source = Constants.achievementImg + s.Image,
                    HeightRequest = 70
                };
                StackLayout tmpStack2 = new StackLayout
                {
                    BackgroundColor = Color.DimGray,
                };

                tmpStack2.Orientation = StackOrientation.Horizontal;
                tmpStack2.Children.Add(tmpImg);
                tmpStack2.Children.Add(tmpStack);
                succesGrid.Children.Add(tmpStack2);

            }
        }
    }
}
