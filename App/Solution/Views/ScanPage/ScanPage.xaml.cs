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
using System.Threading.Tasks;
using ThePlaceToBe.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ScanPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage {
		public ScanPage(ImageSource img, ByteArrayContent ba) {

			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			photo.Source = img;
			TextRecognition(ba);

			btnRetour.Clicked += (s, e) => GoBack(s, e, ba);
			btnUpload.Clicked += (s, e) => Upload(ba, lblName.Text);
			btnNON.Clicked += (s, e) => BtnNONClicked(s, e, ba);
		}

		// BOUTON NON SCAN
		private void BtnNONClicked(object sender, EventArgs e, ByteArrayContent ba) {
			// ASK THE USER TO ENTER THE NAME OF THE BEER

			// Remove button "OUI" and button "NON"
			stackWithOUIAndNON.Children.RemoveAt(1);
			stackWithOUIAndNON.Children.RemoveAt(0);

			// Modification of the label
			labEstCeBienEcrit.Text = "Insérer le nom de la bière";

			// creation of a new entry
			Entry entryNameBeer = new MyEntryInscriptionConnexion {
				WidthRequest = 250,
				VerticalOptions = LayoutOptions.Center,
				HorizontalTextAlignment = TextAlignment.Center
			};

			// remove label lblName
			frameLblName.SetValue(IsVisibleProperty, false);

			Button validateButton = new Button {
				Text = "Valider",
				TextColor = Color.FromHex("fff"),
				FontAttributes = FontAttributes.Bold,
				CornerRadius = 15,
				BorderColor = Color.FromHex("3367b0"),
				BackgroundColor = Color.FromHex("3367b0"),
				BorderWidth = 2,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};

			// add the event to upload the image and the name
			validateButton.Clicked += (s, monEvent) => Upload(ba, entryNameBeer.Text);

			stackLblAndFrame.Children.Add(entryNameBeer);
			stackLblAndFrame.Children.Add(validateButton);
			//this.Navigation.PopAsync();

		}

		// BUTTON RETOUR
		private void GoBack(object sender, EventArgs e, ByteArrayContent ba) {
			ba.Dispose();
			// Cancel the scan
			this.Navigation.PopAsync();
		}

		private async void Upload(ByteArrayContent ba, string LabelName) {

			ba.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
			ba.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") {

				Name = "fileUpload",
				FileName = LabelName + ".jpg"
			};

			string boundary = "---8393774hhy37373773";
			MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
			multipartContent.Add(ba);

			HttpClient httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.PostAsync("http://www.theplacetobe.ovh/admin/upload.php", multipartContent);
			string responseString = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();
			ba.Dispose();

			await Navigation.PopAsync();
		}

		private async void TextRecognition(ByteArrayContent ba) {
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
				string result = "";
				foreach (var region in json["regions"]) {

					foreach (var line in region["lines"]) {

						foreach (var word in line["words"]) {

							result += word["text"] + "\n";
						}
					}
				}
				await DisplayAlert("TEST", result, "OK");
			}
			catch (Exception e) {
				await DisplayAlert("ERROR", e.ToString(), "OK");
			}
		}
	}
}