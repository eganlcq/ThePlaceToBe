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
    public partial class AchievementView : ContentView
    {
        public AchievementView(string idUser)
        {
            InitializeComponent();
            RestService.dic = RestService.dic = new Dictionary<string, string> {

                {"idUser", idUser}
            };
            List<Succes> listSucces = RestService.RequestSucces(RestService.dic, "selectAchievement").Result;
            int nbSucces = listSucces.Count();
            double nbRow = Math.Ceiling(nbSucces / 3.0);
            double nbColumn = 3;
            int count = 0;

            for (int i = 0; i < nbRow; i++)
            {

                RowDefinition row = new RowDefinition
                {
                    Height = 100
                };
                succesGrid.RowDefinitions.Add(row);
            }


            for (int x = 0; x < nbRow; x++)
            {

                for (int y = 0; y < nbColumn && count < nbSucces; y++, count++)
                {

                    Succes succes = listSucces[count];
                    //string imgBiere = listSucces[count].Image;
                    //Image img;
                    Label tmpLab = new Label
                    {
                        Text = listSucces[count].nom,
                        TextColor = Color.Black,
                        FontSize = 11
                    };


                    succesGrid.Children.Add(tmpLab, y, x);
                }
            }
        }
    }
}