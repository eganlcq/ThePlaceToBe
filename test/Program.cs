using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace test {
	class Program {
		static void Main(string[] args) {
			string server = "51.38.239.219";
			string database = "theplacetobe";
			string uid = "admin";
			string password = "jej111jej222";
			string connectionString = "SERVER=" + server + ";PORT=3306;DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";
			MySqlConnection mycon = new MySqlConnection(connectionString);
			MySqlCommand cmd = mycon.CreateCommand();
			cmd.CommandText = "SELECT * from TbBiere;";
			mycon.Open();
			MySqlDataReader reader = cmd.ExecuteReader();
			List<string> listBiere = new List<string>();
			while (reader.Read()) {
				listBiere.Add(reader["nombiere"].ToString());
				
			}
			
			foreach(string biere in listBiere) {
				Console.WriteLine(biere);
			}
			mycon.Close();
		}
	}
}
