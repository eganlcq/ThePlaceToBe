using Java.Sql;
using System;
using System.Collections.Generic;
using System.Text;

namespace ThePlaceToBe.Data
{
    public class User
    {
        public static User currentUser;

        public int Iduser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string Datenaiss { get; set; }
        public string Photo { get; set; }
        public int NbRecherche { get; set; }
        public int NbAjoutPrecedent { get; set; }
        public int NbAjout { get; set; }
        public int NbAch { get; set; }

        public User()
        {

        }
    }
}
