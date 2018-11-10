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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views.ScanPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
		public ScanPage(ImageSource img, ByteArrayContent ba) {

			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			photo.Source = img;

			btnUpload.Clicked += (s, e) => Upload(ba);
		}

		// BOUTON NON SCAN
		private void BtnNONClicked(object sender, EventArgs e) {
			// PAS DE CODE A EXECUTER CAR BIERE PAS OK
			this.Navigation.PopAsync();
		}

		private async void Upload(ByteArrayContent ba) {

			ba.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
			ba.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") {

				Name = "fileUpload",
				FileName = lblName.Text + ".jpg"
			};

			string boundary = "---8393774hhy37373773";
			MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);
			multipartContent.Add(ba);

			HttpClient httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.PostAsync("http://www.theplacetobe.ovh/admin/upload.php", multipartContent);
			string responseString = await response.Content.ReadAsStringAsync();
			response.EnsureSuccessStatusCode();

			await Navigation.PopAsync();
		}
	}
}