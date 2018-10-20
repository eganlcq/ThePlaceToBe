using System;
using System.Collections.Generic;
using System.Text;

namespace ThePlaceToBe.Views.Data
{
    public class User
    {
        public int Iduser { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public string Datenaiss { get; set; }
        public DateTime DatenaissDateTime { get; set; }

        public string Photo { get; set; }


        public User(){
            
        }
    }
}
