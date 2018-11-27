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

		public MenuPage ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);

			Init();
		}

		private void Init() {

			imgLogo.Source = imgLogo.Source = Constants.appImg + "logo.png";
			imgAccount.Source = Constants.userImg + User.currentUser.Photo;
			lblPseudo.Text = User.currentUser.Pseudo;
			ChangeTapRecognizer(addBeerInBarStack, addBeerInBarFrame);

			InitTapRecognizers();
			InitBeerPicker();
			InitBarPicker();
		}

		private void InitTapRecognizers() {

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
		}

		private void InitBeerPicker() {

			RestService.dic = new Dictionary<string, string>();
			List<Beer> listBeer = RestService.Request<Beer>(RestService.dic, "selectBeer").Result;

			foreach (Beer beer in listBeer) {

				beerPicker.Items.Add(beer.Nombiere);
			}
		}

		private void InitBarPicker() {

			RestService.dic = new Dictionary<string, string>();
			List<Bar> listBar = RestService.Request<Bar>(RestService.dic, "selectAllBar").Result;

			foreach (Bar bar in listBar) {

				barPicker.Items.Add(bar.Nombar);
				barForBeerPicker.Items.Add(bar.Nombar);
			}
		}

		private void ChangeTapRecognizer(StackLayout stack, Frame frame) {

			TapGestureRecognizer tap = new TapGestureRecognizer();
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
			tap.Tapped += (s, e) => HideOrShowStack(stack);
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel

			if(frame.GestureRecognizers.Count != 0) {

				frame.GestureRecognizers.RemoveAt(0);
			}

			frame.GestureRecognizers.Add(tap);
		}

		private async Task<bool> HideOrShowStack(StackLayout stack) {

			if(!stack.IsVisible && stack.TranslationY == 0) {

				stack.Opacity = 0;
				stack.IsVisible = true;
				await stack.FadeTo(1, 250);
			}
			else if(stack.IsVisible) {
				
				await stack.TranslateTo(stack.TranslationX, stack.TranslationY - stack.Height);
				stack.IsVisible = false;

				if (stack != addBeerInBarStack) {
					
					if(stack == addBeerStack) {

						beerPickerTextRecognition.SelectedItem = null;
						barForBeerPicker.SelectedItem = null;
						entryChooseBeerName.Text = null;
						entryAlcohol.Text = null;
						entryType.Text = null;
					}

					if(stack == addBarStack) {

						entryNameBar.Text = null;
						entryNumStreet.Text = null;
						entryNameStreet.Text = null;
						entryZIPCode.Text = null;
						entryNameCity.Text = null;
					}

					ChangeTapRecognizer(addBeerInBarStack, addBeerInBarFrame);
				}
			}
			else {

				stack.IsVisible = true;
				await stack.TranslateTo(stack.TranslationX, stack.TranslationY + stack.Height);
			}
			return true;
		}

		private void ProfilMainPageTapped() {
			this.Navigation.PushAsync(new AchievementPage.AchievementPage(User.currentUser.Iduser.ToString()));
		}

		private async void ChangePickerIntoEntry(StackLayout stack, Entry entry) {

			await stack.FadeTo(0, 250);
			stack.IsVisible = false;

			entry.IsVisible = true;
			await entry.FadeTo(1, 250);
		}

		private async void ChangeEntryIntoPicker(StackLayout stack, Entry entry) {

			await entry.FadeTo(0, 250);
			entry.IsVisible = false;

			stack.IsVisible = true;
			await stack.FadeTo(1, 250);

		}

		private async void SendBeerInBar() {

			string result = CheckBeerInBar();
			if (result == "OK") {
				if(labelErrorBeerInBar.Opacity == 1) await labelErrorBeerInBar.FadeTo(0, 250);
				RestService.dic = new Dictionary<string, string> {

					{ "beer", beerPicker.SelectedItem.ToString() },
					{ "bar", barPicker.SelectedItem.ToString() }
				};
				await RestService.Request<Beer>(RestService.dic, "insertTmpBeerInBar");
				await HideOrShowStack(addBeerInBarStack);
				beerPicker.SelectedItem = null;
				barPicker.SelectedItem = null;
			}
			else {

				if(labelErrorBeerInBar.Opacity == 1) {

					await labelErrorBeerInBar.FadeTo(0, 250);
				}
				labelErrorBeerInBar.Text = result;
				await labelErrorBeerInBar.FadeTo(1, 250);
			}
		}

		private string CheckBeerInBar() {

			if (beerPicker.SelectedItem == null || barPicker.SelectedItem == null) {

				return "Please choose a beer and a bar.";
			}
			else {

				RestService.dic = new Dictionary<string, string> {

					{ "beer", beerPicker.SelectedItem.ToString() },
					{ "bar", barPicker.SelectedItem.ToString() }
				};
				bool check = RestService.Request<Check>(RestService.dic, "checkBeerInBar").Result[0].Verif;

				if(check) {

					return "The beer is already in the bar";
				}
				else {

					return "OK";
				}
			}
		}

		private async void SendBeer(byte[] byteArray) {

			if (labelErrorBeer.Opacity == 1) await labelErrorBeer.FadeTo(0, 250);

			string nameOfBeer;

			if (beerStackPicker.IsVisible) {

				if (beerPickerTextRecognition.SelectedItem != null) {
					nameOfBeer = beerPickerTextRecognition.SelectedItem.ToString();
				}
				else nameOfBeer = null;
			}
			else {
				
				nameOfBeer = entryChooseBeerName.Text;
			}

			string result = CheckBeer(nameOfBeer);
			
			if (result == "OK") {

				RestService.dic = new Dictionary<string, string> {

					{ "nameBeer", nameOfBeer },
					{ "alcool", entryAlcohol.Text },
					{ "type", entryType.Text },
					{ "imgBeer", nameOfBeer + ".jpg" },
					{ "bar", barForBeerPicker.SelectedItem.ToString() }
				};
				await RestService.Request<Beer>(RestService.dic, "insertTmpBeer");
				await HideOrShowStack(addBeerStack);
				Upload(byteArray, nameOfBeer);
			}
			else {

				if (labelErrorBeer.Opacity == 1) {

					await labelErrorBeer.FadeTo(0, 250);
				}
				labelErrorBeer.Text = result;
				await labelErrorBeer.FadeTo(1, 250);
			}
		}

		private string CheckBeer(string nameOfBeer) {

			bool isNotNull = CheckIfNotNull(nameOfBeer);
			if(!isNotNull) {

				return "Please choose a name and a bar.";
			}
			else {

				RestService.dic = new Dictionary<string, string> {

					{ "beer", nameOfBeer }
				};
				bool check = RestService.Request<Check>(RestService.dic, "checkBeer").Result[0].Verif;

				if (check) {

					return "The beer already exists";
				}
				else {

					return "OK";
				}
			}
		}

		private bool CheckIfNotNull(string nameOfBeer) {

			if(barForBeerPicker.SelectedItem == null) {

				return false;
			}
			else {

				if(nameOfBeer == "" || nameOfBeer == null) {

					return false;
				}
				else return true;
			}
		}

		private async void SendBar() {

			string result = CheckBar();
			if (result == "OK") {
				if (labelErrorBar.Opacity == 1) await labelErrorBar.FadeTo(0, 250);

				RestService.dic = new Dictionary<string, string> {

					{ "nameBar", entryNameBar.Text },
					{ "numStreet", entryNumStreet.Text },
					{ "nameStreet", entryNameStreet.Text },
					{ "ZIPCode", entryZIPCode.Text },
					{ "nameCity", entryNameCity.Text },
					{ "accessibility", "1" }
				};
				await RestService.Request<Beer>(RestService.dic, "insertTmpBar");
				await HideOrShowStack(addBarStack);
			}
			else {

				if (labelErrorBar.Opacity == 1) {

					await labelErrorBar.FadeTo(0, 250);
				}
				labelErrorBar.Text = result;
				await labelErrorBar.FadeTo(1, 250);
			}
		}

		private string CheckBar() {

			if(entryNameBar.Text == null || entryNameBar.Text == "") {

				return "Please choose a name for the bar.";
			}
			else if(entryNumStreet.Text != null && entryNumStreet.Text != "" && !Int32.TryParse(entryNumStreet.Text, out int valNumStreet)) {

				return "The number of the street must be a number.";
			}
			else if(entryZIPCode.Text != null && entryZIPCode.Text != "" && !Int32.TryParse(entryZIPCode.Text, out int valZIP)) {

				return "The ZIP code must be a number.";
			}
			else {

				RestService.dic = new Dictionary<string, string> {

					{ "bar", entryNameBar.Text }
				};
				bool check = RestService.Request<Check>(RestService.dic, "checkBar").Result[0].Verif;

				if (check) {

					return "The bar already exists";
				}
				else {

					return "OK";
				}
			}
		}

		private async void AddNewBar(StackLayout stackToHide) {

			await HideOrShowStack(stackToHide);
			ChangeTapRecognizer(addBarStack, addBeerInBarFrame);
			await HideOrShowStack(addBarStack);
		}

		private async void AddNewBeer() {

			var answer = await DisplayAlert("NOTE", "You will need to take a photo in order to add a beer, is it ok ?", "Yes", "No");

			if(answer) {

				try {
					await CrossMedia.Current.Initialize();

					var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
					var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

					if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted) {
						var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
						cameraStatus = results[Permission.Camera];
						storageStatus = results[Permission.Storage];
					}

					if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted) {

						if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {

#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
							DisplayAlert("No Camera", "No camera available.", "OK");
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
							return;
						}

						var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions {

							Directory = "Sample",
							Name = "test.jpg",
							PhotoSize = PhotoSize.Medium,
							CompressionQuality = 92
						});

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
						if(sendBeerFrame.GestureRecognizers.Count != 0) {

							for(int i = 0; i < sendBeerFrame.GestureRecognizers.Count; i++) {

								sendBeerFrame.GestureRecognizers.RemoveAt(0);
							}
						}
						sendBeerFrame.GestureRecognizers.Add(tapSendBeer);

						using (var fileContent = new ByteArrayContent(bitmapData)) {

							await TextRecognition(fileContent, beerPickerTextRecognition);
						}

						if(!beerStackPicker.IsVisible) {

							ChangeEntryIntoPicker(beerStackPicker, entryChooseBeerName);
						}

						await HideOrShowStack(addBeerInBarStack);
						ChangeTapRecognizer(addBeerStack, addBeerInBarFrame);
						await HideOrShowStack(addBeerStack);

						if (File.Exists(file.Path)) {

							File.Delete(file.Path);
						}

						file.Dispose();
					}
					else {

						await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
					}
				}
				catch (Exception e) {
					await DisplayAlert("ERROR PHOTO", e.ToString(), "OK");
				}

			}
		}

		private async Task<bool> TextRecognition(ByteArrayContent ba, Picker picker) {
			try {
				string key = "8bf0245b7b8c4c0cb26da1dcd95647b4";
				string uriBase = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/ocr";
				HttpClient client = new HttpClient();
				client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
				string param = "visualFeatures=Categories,Description,Color";
				string uri = uriBase + "?" + param;
				HttpResponseMessage response;
				ba.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
				response = await client.PostAsync(uri, ba);
				string contentString = await response.Content.ReadAsStringAsync();
				var json = JToken.Parse(contentString);
				foreach (var region in json["regions"]) {

					foreach (var line in region["lines"]) {

						foreach (var word in line["words"]) {

							picker.Items.Add(word["text"].ToString());
						}
					}
				}
			}
			catch(Java.IO.IOException) {


			}
			catch (Exception e) {

				await DisplayAlert("ERROR POST", e.ToString(), "OK");
			}

			if(picker.Items.Count == 0) {

				picker.Items.Add("default");
			}

			return true;
		}

		private async void Upload(byte[] byteArray, string nameOfBeer) {

			

			using (var ba = new ByteArrayContent(byteArray)) {

				ba.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
				ba.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") {

					Name = "fileUpload",
					FileName = nameOfBeer + ".jpg"
				};

				string boundary = "---8393774hhy37373773";
				MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
				multipartContent.Add(ba);

				HttpClient httpClient = new HttpClient();
				HttpResponseMessage response = await httpClient.PostAsync("http://www.theplacetobe.ovh/admin/upload.php", multipartContent);
				string responseString = await response.Content.ReadAsStringAsync();
				response.EnsureSuccessStatusCode();
			}
		}
	}
}