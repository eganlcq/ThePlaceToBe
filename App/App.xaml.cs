
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using System;
using ThePlaceToBe.Views;
using ThePlaceToBe.Views.AchievementPage;
using ThePlaceToBe.Views.ConnexionPage;
using ThePlaceToBe.Views.InscriptionPage;
using ThePlaceToBe.Views.MainPage;
using ThePlaceToBe.Views.ProductPage;
using ThePlaceToBe.Views.ScanPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ThePlaceToBe {
	public partial class App : Application {


		public App() {
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			MainPage = new NavigationPage(new ConnexionPage());
			//MainPage = new Test();
		}

		protected override void OnStart() {

			
		}

		protected override void OnSleep() {
			// Handle when your app sleeps
		}

		protected override void OnResume() {
			// Handle when your app resumes
		}
	}
}
