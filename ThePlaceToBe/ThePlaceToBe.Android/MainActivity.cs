using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android;
using ThePlaceToBe.Data;
using Android.Support.Design.Widget;
using Android.Support.V7.App;

namespace ThePlaceToBe.Droid
{
    [Activity(Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
			await TryGetPermissionsAsync();
			base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

		readonly string[] permissions = {

			Manifest.Permission.AccessCoarseLocation,
			Manifest.Permission.AccessFineLocation
		};
		const int requestLocationId = 0;

		private async Task TryGetPermissionsAsync() {

			if ((int)Build.VERSION.SdkInt >= 23) {

				await GetPermissionsAsync();
				return;
			}
		}

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
		private async Task GetPermissionsAsync() {
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone

			const string permission = Manifest.Permission.AccessFineLocation;

			if (CheckSelfPermission(permission) != (int)Permission.Granted) {

				RequestPermissions(permissions, requestLocationId);
			}
		}

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
		public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone

			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			switch (requestCode) {

				case requestLocationId:

					if (grantResults[0] == Permission.Granted) {

						Toast.MakeText(this, "Permissions granted", ToastLength.Short);
					}
					else {

						Toast.MakeText(this, "Permissions denied", ToastLength.Short);
					}

					break;
			}
		}
	}

	[Activity(Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity {

		protected override void OnResume() {
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}