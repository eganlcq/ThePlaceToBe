using Java.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThePlaceToBe.Data
{
    class User
    {
		public static User currentUser;

		public int Iduser { get; set; }
		public string Nom { get; set; }
		public string Prenom { get; set; }
		public string Pseudo { get; set; }
		public string Email { get; set; }
		public string Datenaiss { get; set; }
		public string Photo { get; set; }

		public User() {

		}
	}
}
