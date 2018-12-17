using Newtonsoft.Json.Linq;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.MenuPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {

        List<StackLayout> listStack = new List<StackLayout>();
        List<Bar> listBars = new List<Bar>();

        public MenuPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            // Initialize the content of the xaml page
            Init();
        }

        // Initialize the content of the xaml page
        private void Init()
        {
            imgLogo.Source = imgLogo.Source = Constants.appImg + "logo.png";
            imgAccount.Source = Constants.userImg + User.currentUser.Photo;
            retour.Source = Constants.appImg + "retourBlanc.png";
            lblPseudo.Text = User.currentUser.Pseudo;
            disconnection.Source = Constants.appImg + "disconnect.png";
            logoHandicape.Source = Constants.appImg + "logoHandicapeBlanc.png";
            boutonPlus1.Source = Constants.appImg + "iconePlus.png";
            boutonPlus2.Source = Constants.appImg + "iconePlus.png";

            ChangeTapRecognizer(addBeerInBarStack, addBeerInBarFrame);
            ChangeTapRecognizer(dataProtectionStack, showDataProtectionFrame);
            ChangeTapRecognizer(TauxAlcoolParVerreStack, showTauxAlcoolParVerreFrame);
            ChangeTapRecognizer(RappelDangerAlcoolStack, showRappelDangerAlcoolFrame);
            ChangeTapRecognizer(ListBarsStack, showListBarsFrame);

            InitListStackLayout();
            DisplayText();
            InitTapRecognizers();
            InitBeerPicker();
            InitBarPicker();
            InitListBars();

            HideOrShowStack(addBeerInBarStack);

        }

        // this method initialize the content of listBars with the bars present in the database
        private void InitListBars()
        {
            RestService.dic = new Dictionary<string, string>();
            listBars = RestService.Request<Bar>(RestService.dic, "selectAllBar").Result;

            foreach (Bar b in listBars)
            {

                Label tmpBarNom = new Label
                {
                    Text = b.Nombar,
                    TextColor = Color.Black,
                    FontSize = 12
                };

                string tmpAccessibilite = "";
                string tmpIMG = "";
                if (b.Accessibilite.ToString() == "0")
                {
                    tmpAccessibilite = "Ce bar ne possède pas d'accès adapté aux handicapés.";
                    tmpIMG = "logoHandicapeRouge.png";
                }
                else if (b.Accessibilite.ToString() == "1")
                {
                    tmpAccessibilite = "Ce bar est facile d'accès aux handicapés.";
                    tmpIMG = "logoHandicapeVert.png";
                }
                else
                {
                    tmpAccessibilite = "Erreur de récupération de donnée.";
                }

                Label accessibilite = new Label
                {
                    Text = tmpAccessibilite,
                    TextColor = Color.Black,
                    FontSize = 10
                };
                Image img = new Image
                {
                    Source = Constants.appImg + tmpIMG,
                    HeightRequest = 12
                };
                Label adresse = new Label
                {
                    Text = b.Rue + " " + b.Numero.ToString(),
                    TextColor = Color.Black,
                    FontSize = 10
                };

                StackLayout tmpstackAcces = new StackLayout();
                tmpstackAcces.Orientation = StackOrientation.Horizontal;
                tmpstackAcces.Children.Add(accessibilite);
                tmpstackAcces.Children.Add(img);

                StackLayout tmpStack = new StackLayout
                {
                    Spacing = 5,
                    Margin = new Thickness(5)
                };
                StackLayout tmpStack2 = new StackLayout
                {
                    BackgroundColor = Color.White,
                };

                tmpStack.Children.Add(tmpBarNom);
                tmpStack.Children.Add(tmpstackAcces);
                tmpStack.Children.Add(adresse);
                tmpStack2.Orientation = StackOrientation.Horizontal;
                tmpStack2.Children.Add(tmpStack);
                ListBarsGrid.Children.Add(tmpStack2);

            }

        }

        // set all the stack from the tableview in a list
        private void InitListStackLayout()
        {
            listStack.Add(addBeerInBarStack);
            listStack.Add(addBeerStack);
            listStack.Add(addBarStack);
            listStack.Add(dataProtectionStack);
            listStack.Add(TauxAlcoolParVerreStack);
            listStack.Add(RappelDangerAlcoolStack);
            listStack.Add(ListBarsStack);

        }

        // initialize the evenements when an element is clicked
        private void InitTapRecognizers()
        {
            var tapAddBeer = new TapGestureRecognizer();
            tapAddBeer.Tapped += (s, e) => AddNewBeer();
            addBeerFrame.GestureRecognizers.Add(tapAddBeer);

            var tapAddBar = new TapGestureRecognizer();
            tapAddBar.Tapped += (s, e) => AddNewBar(addBeerInBarStack);
            addBarFrame.GestureRecognizers.Add(tapAddBar);

            var tapChooseNameBeer = new TapGestureRecognizer();
            tapChooseNameBeer.Tapped += (s, e) => ChangePickerIntoEntry(beerStackPicker, entryChooseBeerName);
            chooseNameBeerFrame.GestureRecognizers.Add(tapChooseNameBeer);

            var tapAddBarForBeer = new TapGestureRecognizer();
            tapAddBarForBeer.Tapped += (s, e) => AddNewBar(addBeerStack);
            addBarForBeerFrame.GestureRecognizers.Add(tapAddBarForBeer);

            var tapSendBeerInBar = new TapGestureRecognizer();
            tapSendBeerInBar.Tapped += (s, e) => SendBeerInBar();
            sendBeerInBarFrame.GestureRecognizers.Add(tapSendBeerInBar);

            var tapSendBar = new TapGestureRecognizer();
            tapSendBar.Tapped += (s, e) => SendBar();
            sendBarFrame.GestureRecognizers.Add(tapSendBar);

            var tapChangeHandicapeAcces = new TapGestureRecognizer();
            tapChangeHandicapeAcces.Tapped += (s, e) => ChangeHandicapeAcces();
            logoHandicape.GestureRecognizers.Add(tapChangeHandicapeAcces);
        }

        private void ChangeHandicapeAcces()
        {
            if(logoHandicape.Source.ToString().Split('/').Last() == "logoHandicapeBlanc.png")
            {
                logoHandicape.Source = Constants.appImg + "logoHandicapeBleu.png";
            }
            else if (logoHandicape.Source.ToString().Split('/').Last() == "logoHandicapeBleu.png")
            {
                logoHandicape.Source = Constants.appImg + "logoHandicapeBlanc.png";
            }
        }

        // Send the texts in the xaml page
        private void DisplayText()
        {
            List<Text> textL = RestService.Request<Text>(RestService.dic, "getTextFromFileTXT").Result;

            dataProtectionText.Text = textL[0].TexteProdData;
            TauxAlcoolParVerreText.Text = textL[1].TexteTauxAlcool;
            RappelDangerAlcoolText.Text = textL[2].TexteRappel;
        }

        // initialize the content of the picker for beers
        private void InitBeerPicker()
        {
            RestService.dic = new Dictionary<string, string>();
            List<Beer> listBeer = RestService.Request<Beer>(RestService.dic, "selectBeer").Result;

            foreach (Beer beer in listBeer)
            {
                beerPicker.Items.Add(beer.Nombiere);
            }
        }

        // initialize the content of the picker for bars
        private void InitBarPicker()
        {
            RestService.dic = new Dictionary<string, string>();
            List<Bar> listBar = RestService.Request<Bar>(RestService.dic, "selectAllBar").Result;

            foreach (Bar bar in listBar)
            {
                barPicker.Items.Add(bar.Nombar);
                barForBeerPicker.Items.Add(bar.Nombar);
            }
        }

        // change the evenement of the main frame
        private void ChangeTapRecognizer(StackLayout stack, Frame frame)
        {
            TapGestureRecognizer tap = new TapGestureRecognizer();
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
            tap.Tapped += (s, e) => HideOrShowStack(stack);
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel

            if (frame.GestureRecognizers.Count != 0)
            {
                frame.GestureRecognizers.RemoveAt(0);
            }

            frame.GestureRecognizers.Add(tap);
        }

        private async Task<bool> HideOrShowStack(StackLayout stack)
        {
            if (!stack.IsVisible && stack.TranslationY == 0)
            {
                foreach (StackLayout stackLayout in listStack)
                {
                    if (stackLayout.IsVisible)
                    {
                        stackLayout.IsVisible = false;
                    }
                }
                stack.Opacity = 0;
                stack.IsVisible = true;
                await stack.FadeTo(1, 250);
            }
            else if (stack.IsVisible)
            {
                await stack.TranslateTo(stack.TranslationX, stack.TranslationY - stack.Height);
                stack.IsVisible = false;

                if (stack == addBeerStack || stack == addBarStack)
                {
                    if (stack == addBeerStack)
                    {
                        beerPickerTextRecognition.SelectedItem = null;
                        barForBeerPicker.SelectedItem = null;
                        entryChooseBeerName.Text = null;
                        entryAlcohol.Text = null;
                        entryType.Text = null;
                    }

                    if (stack == addBarStack)
                    {
                        entryNameBar.Text = null;
                        entryNumStreet.Text = null;
                        entryNameStreet.Text = null;
                        entryZIPCode.Text = null;
                        entryNameCity.Text = null;
                    }

                    ChangeTapRecognizer(addBeerInBarStack, addBeerInBarFrame);
                }
            }
            else
            {
                foreach (StackLayout stackLayout in listStack)
                {
                    if (stackLayout.IsVisible)
                    {
                        stackLayout.IsVisible = false;
                    }
                }
                stack.IsVisible = true;
                await stack.TranslateTo(stack.TranslationX, stack.TranslationY + stack.Height);
            }
            return true;
        }

        // This method is running when the avatar picture is clicked
        private void ProfilMenuPageTapped()
        {
            this.Navigation.PushAsync(new AchievementPage.AchievementPage(User.currentUser.Iduser.ToString()));
        }

        // change the display of the screen to hide the picker and show an entry
        private async void ChangePickerIntoEntry(StackLayout stack, Entry entry)
        {
            await stack.FadeTo(0, 250);
            stack.IsVisible = false;

            entry.IsVisible = true;
            await entry.FadeTo(1, 250);
        }

        // change the display of the screen to hide the entry and show a picker
        private async void ChangeEntryIntoPicker(StackLayout stack, Entry entry)
        {
            await entry.FadeTo(0, 250);
            entry.IsVisible = false;

            stack.IsVisible = true;
            await stack.FadeTo(1, 250);
        }

        // this method send a request to insert a beer in a bar in the database
        private async void SendBeerInBar()
        {
            string result = CheckBeerInBar();
            if (result == "OK")
            {
                if (labelErrorBeerInBar.Opacity == 1) await labelErrorBeerInBar.FadeTo(0, 250);
                RestService.dic = new Dictionary<string, string> {

                    { "beer", beerPicker.SelectedItem.ToString() },
                    { "bar", barPicker.SelectedItem.ToString() },
                    { "idUser", User.currentUser.Iduser.ToString() }
                };
                await RestService.Request<Beer>(RestService.dic, "insertTmpBeerInBar");
                await HideOrShowStack(addBeerInBarStack);
                beerPicker.SelectedItem = null;
                barPicker.SelectedItem = null;
            }
            else
            {
                if (labelErrorBeerInBar.Opacity == 1)
                {
                    await labelErrorBeerInBar.FadeTo(0, 250);
                }
                labelErrorBeerInBar.Text = result;
                await labelErrorBeerInBar.FadeTo(1, 250);
            }
        }

        // This method verify if a beer is in a bar or not
        private string CheckBeerInBar()
        {
            if (beerPicker.SelectedItem == null || barPicker.SelectedItem == null)
            {
                return "Choisissez une bière et un bar.";
            }
            else
            {
                RestService.dic = new Dictionary<string, string> {

                    { "beer", beerPicker.SelectedItem.ToString() },
                    { "bar", barPicker.SelectedItem.ToString() }
                };
                bool check = RestService.Request<Check>(RestService.dic, "checkBeerInBar").Result[0].Verif;

                if (check)
                {
                    return "La bière existe déjà.";
                }
                else
                {
                    return "OK";
                }
            }
        }

        // this method send a request to insert a beer in the database
        private async void SendBeer(byte[] byteArray)
        {
            if (labelErrorBeer.Opacity == 1) await labelErrorBeer.FadeTo(0, 250);

            string nameOfBeer;

            if (beerStackPicker.IsVisible)
            {
                if (beerPickerTextRecognition.SelectedItem != null)
                {
                    nameOfBeer = beerPickerTextRecognition.SelectedItem.ToString();
                }
                else nameOfBeer = null;
            }
            else
            {
                nameOfBeer = entryChooseBeerName.Text;
            }

            string result = CheckBeer(nameOfBeer);

            if (result == "OK")
            {
                RestService.dic = new Dictionary<string, string> {

                    { "nameBeer", nameOfBeer },
                    { "alcool", entryAlcohol.Text },
                    { "type", entryType.Text },
                    { "imgBeer", nameOfBeer + ".jpg" },
                    { "bar", barForBeerPicker.SelectedItem.ToString() },
                    { "idUser", User.currentUser.Iduser.ToString() }
                };
                await RestService.Request<Beer>(RestService.dic, "insertTmpBeer");
                await HideOrShowStack(addBeerStack);
                using (ByteArrayContent ba = new ByteArrayContent(byteArray))
                {

                    Upload(ba, nameOfBeer, "upload");
                }
            }
            else
            {

                if (labelErrorBeer.Opacity == 1)
                {

                    await labelErrorBeer.FadeTo(0, 250);
                }
                labelErrorBeer.Text = result;
                await labelErrorBeer.FadeTo(1, 250);
            }
        }

        // this method verify if a beer is in the database or not
        private string CheckBeer(string nameOfBeer)
        {
            bool isNotNull = CheckIfNotNull(nameOfBeer);
            if (!isNotNull)
            {
                return "Choisissez un nom et un bar.";
            }
            else
            {
                RestService.dic = new Dictionary<string, string> {

                    { "beer", nameOfBeer }
                };
                bool check = RestService.Request<Check>(RestService.dic, "checkBeer").Result[0].Verif;

                if (check)
                {
                    return "La bière existe déjà.";
                }
                else
                {
                    return "OK";
                }
            }
        }

        private bool CheckIfNotNull(string nameOfBeer)
        {
            if (barForBeerPicker.SelectedItem == null)
            {
                return false;
            }
            else
            {

                if (nameOfBeer == "" || nameOfBeer == null)
                {
                    return false;
                }
                else return true;
            }
        }

        // this method send a request to insert a bar in the database
        private async void SendBar()
        {
            string result = CheckBar();
            if (result == "OK")
            {
                if (labelErrorBar.Opacity == 1) await labelErrorBar.FadeTo(0, 250);

                var accessible = CheckAccessibility(); ;

                RestService.dic = new Dictionary<string, string> {

                    { "nameBar", entryNameBar.Text },
                    { "numStreet", entryNumStreet.Text },
                    { "nameStreet", entryNameStreet.Text },
                    { "ZIPCode", entryZIPCode.Text },
                    { "nameCity", entryNameCity.Text },
                    { "accessibility", accessible.ToString() },
                    { "idUser", User.currentUser.Iduser.ToString() }
                };
                await RestService.Request<Beer>(RestService.dic, "insertTmpBar");
                await HideOrShowStack(addBarStack);
            }
            else
            {
                if (labelErrorBar.Opacity == 1)
                {
                    await labelErrorBar.FadeTo(0, 250);
                }
                labelErrorBar.Text = result;
                await labelErrorBar.FadeTo(1, 250);
            }
        }

        private int CheckAccessibility()
        {
            if (logoHandicape.Source.ToString().Split('/').Last() == "logoHandicapeBlanc.png")
            {
                return 0;
            }
            else if(logoHandicape.Source.ToString().Split('/').Last() == "logoHandicapeBleu.png")
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // this method verify if the bar exist or not
        private string CheckBar()
        {
            if (entryNameBar.Text == null || entryNameBar.Text == "")
            {
                return "Choisissez un  nom pour le bar.";
            }
            else if (entryNumStreet.Text != null && entryNumStreet.Text != "" && !Int32.TryParse(entryNumStreet.Text, out int valNumStreet))
            {
                return "Le numéro de rue doit être un chiffre.";
            }
            else if (entryZIPCode.Text != null && entryZIPCode.Text != "" && !Int32.TryParse(entryZIPCode.Text, out int valZIP))
            {
                return "Le code postal doit être un numéro.";
            }
            else
            {
                RestService.dic = new Dictionary<string, string> {

                    { "bar", entryNameBar.Text }
                };
                bool check = RestService.Request<Check>(RestService.dic, "checkBar").Result[0].Verif;

                if (check)
                {
                    return "Le bar existe déjà.";
                }
                else
                {
                    return "OK";
                }
            }
        }

        private async void AddNewBar(StackLayout stackToHide)
        {
            await HideOrShowStack(stackToHide);
            ChangeTapRecognizer(addBarStack, addBeerInBarFrame);
            await HideOrShowStack(addBarStack);
        }

        // this method add a bar by using the scan
        private async void AddNewBeer()
        {
            var answer = await DisplayAlert("NOTE", "Vous aurez besoin de prendre une photo pour ajouter une bière, acceptez-vous ?", "Oui", "Non");

            if (answer)
            {
                try
                {
                    await CrossMedia.Current.Initialize();

                    var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                    var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

                    if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                        cameraStatus = results[Permission.Camera];
                        storageStatus = results[Permission.Storage];
                    }

                    if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
                    {
                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
                            DisplayAlert("Pas de camera", "Pas de camera disponible.", "OK");
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
                            return;
                        }

                        var takePhoto = await DisplayAlert("NOTE", "Voulez-vous prendre une photo ou choisir un photo dans votre librairie ?", "Prendre une photo", "Choisir une photo");

                        MediaFile file;
                        if (takePhoto)
                        {

                            file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                            {
                                Directory = "Sample",
                                Name = "test.jpg",
                                PhotoSize = PhotoSize.Medium,
                                CompressionQuality = 92
                            });
                        }
                        else
                        {

                            file = await CrossMedia.Current.PickPhotoAsync();
                        }


                        if (file == null) return;


                        ImageSource img = ImageSource.FromStream(() => {

                            var streamImg = file.GetStream();
                            return streamImg;
                        });

                        photoBeer.Source = img;

                        var stream = new MemoryStream();
                        file.GetStream().CopyTo(stream);
                        byte[] bitmapData = stream.ToArray();

                        var tapSendBeer = new TapGestureRecognizer();
                        tapSendBeer.Tapped += (s, e) => SendBeer(bitmapData);
                        if (sendBeerFrame.GestureRecognizers.Count != 0)
                        {

                            for (int i = 0; i < sendBeerFrame.GestureRecognizers.Count; i++)
                            {
                                sendBeerFrame.GestureRecognizers.RemoveAt(0);
                            }
                        }
                        sendBeerFrame.GestureRecognizers.Add(tapSendBeer);

                        using (var fileContent = new ByteArrayContent(bitmapData))
                        {

                            Upload(fileContent, "img", "uploadForTextRecognition");
                        }
                        TextRecognition(beerPickerTextRecognition);

                        if (!beerStackPicker.IsVisible)
                        {
                            ChangeEntryIntoPicker(beerStackPicker, entryChooseBeerName);
                        }

                        await HideOrShowStack(addBeerInBarStack);
                        ChangeTapRecognizer(addBeerStack, addBeerInBarFrame);
                        await HideOrShowStack(addBeerStack);

                        if (File.Exists(file.Path))
                        {
                            File.Delete(file.Path);
                        }

                        file.Dispose();
                    }
                    else
                    {
                        await DisplayAlert("Permissions refusées", "La photo ne peut pas être prise.", "OK");
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("ERREUR PHOTO", e.ToString(), "OK");
                }
            }
        }

        // this method is used to send an picture an get the text on it
        private bool TextRecognition(Picker picker)
        {
            RestService.dic = new Dictionary<string, string>() {

                { "image", "img.jpg" }
            };
            List<string> list = RestService.Request<string>(RestService.dic, "textRecognition/textRecognition").Result;

            if (picker.Items.Count != 0)
            {
                for (int i = 0; i < picker.Items.Count; i++)
                {
                    picker.Items.RemoveAt(0);
                }
            }

            foreach (string str in list)
            {
                if (str != "" && str != null)
                {
                    picker.Items.Add(str);
                }
            }

            if (picker.Items.Count == 0)
            {
                picker.Items.Add("default");
            }

            return true;
        }

        // this method upload the a pitcure on the server
        private async void Upload(ByteArrayContent ba, string nameOfBeer, string url)
        {
            {
                try
                {
                    ba.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    ba.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {

                        Name = "fileUpload",
                        FileName = nameOfBeer + ".jpg"
                    };

                    MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                    multipartContent.Add(ba);

                    HttpClient httpClient = new HttpClient();
                    HttpResponseMessage response = await httpClient.PostAsync("http://www.theplacetobe.ovh/admin/" + url + ".php", multipartContent);
                    string responseString = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                }
                catch (Exception e)
                {

                    await DisplayAlert("ERREUR", e.Message.ToString(), "OK");
                }
            }
        }

        // this method method is running when the back icon is clicked
        // return to the previous page
        private void Retour(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        // this method is running when the disconnect button is clicked
        private void Disconnect(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new ConnexionPage.ConnexionPage(), Navigation.NavigationStack[0]);
            User.currentUser = null;
            Navigation.PopToRootAsync();
        }
    }
}