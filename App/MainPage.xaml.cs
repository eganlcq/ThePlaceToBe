using System;
using System.Diagnostics;
using Xamarin.Forms;
using System.IO;
using Plugin.Media;
using Plugin.Permissions.Abstractions;
using Plugin.Permissions;
using Plugin.Media.Abstractions;
using System.Net.Http;
using Microsoft.ProjectOxford.Vision.Contract;
using Microsoft.ProjectOxford.Vision;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Test {
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();

			takePhoto.Clicked += (sender, args) => TakePhoto();
		}

		private async void TakePhoto() {

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
					
					await DisplayAlert("No Camera", "No camera available.", "OK");
					return;
				}

				/*var photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions {

					Directory = "Sample",
					Name = "test.jpg",
					PhotoSize = PhotoSize.Medium,
					CompressionQuality = 92
				});*/
				var photo = await CrossMedia.Current.PickPhotoAsync();

				if (photo == null) return;
				
				ImageSource img = ImageSource.FromStream(() => {

					var streamImg = photo.GetStream();
					return streamImg;
				});

				image.Source = img;

				try {

					string key = "8bf0245b7b8c4c0cb26da1dcd95647b4";
					string uriBase = "https://westeurope.api.cognitive.microsoft.com/vision/v1.0/ocr";

					HttpClient client = new HttpClient();
					client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

					string param = "visualFeatures=Categories,Description,Color";
					string uri = uriBase + "?" + param;
					HttpResponseMessage response;

					byte[] byteData;
					var stream = new MemoryStream();
					photo.GetStream().CopyTo(stream);
					byteData = stream.ToArray();

					using(ByteArrayContent content = new ByteArrayContent(byteData)) {

						content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
						response = await client.PostAsync(uri, content);
					}

					string contentString = await response.Content.ReadAsStringAsync();
					var json = JToken.Parse(contentString);
					string result = "";
					foreach(var region in json["regions"]) {

						foreach(var line in region["lines"]) {

							foreach(var word in line["words"]) {

								result += word["text"] + "\n";
							}
						}
					}
					await DisplayAlert("TEST", result, "OK");
				}
				catch(Exception e) {

					await DisplayAlert("ERROR", e.ToString(), "OK");
				}

				if (File.Exists(photo.Path)) {

					File.Delete(photo.Path);
				}

				photo.Dispose();
			}
			else {

				await DisplayAlert("Permissions Denied", "Unable to take photos.", "OK");
			}
		}
	}
}
