using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ThePlaceToBe.Views.Database {
	class Beer {

		public int Idbiere { get; set; }
		public string Nombiere { get; set; }
		public float Alcoolemie { get; set; }
		public string Typebiere { get; set; }
		public string Image { get; set; }

		public Beer() {
			
		}
	}
}
