using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThePlaceToBe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Test : ContentPage
	{
		public Test ()
		{
			InitializeComponent ();
			string server = "51.38.239.219";
			string database = "theplacetobe";
			string uid = "root";
			string password = "jej111jej222";
			string connectionString = "SERVER=" + server + ";PORT=3306;DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
			MySqlConnection mycon = new MySqlConnection(connectionString);
			MySqlCommand cmd = mycon.CreateCommand();
			cmd.CommandText = "SELECT * from TbBiere;";
			mycon.Open();
			MySqlDataReader reader = cmd.ExecuteReader();
			int i = 0;
			while(reader.Read()) {
				i++;
			}

		}
	}
}