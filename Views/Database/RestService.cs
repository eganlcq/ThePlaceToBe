using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ThePlaceToBe.Views.Database {
	static class RestService {

		private static readonly string rxcui = "198440";
		private static readonly string uri = "http://51.38.239.219/admin/test.php";

		public static List<Beer> Request() {

			WebRequest request = HttpWebRequest.Create(string.Format(@uri, rxcui));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse) {

				if (response.StatusCode != HttpStatusCode.OK) {

					Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
				}

				using (StreamReader reader = new StreamReader(response.GetResponseStream())) {

					string content = reader.ReadToEnd();
					if (string.IsNullOrWhiteSpace(content)) {

						Console.Out.WriteLine("Response contained empty body...");
						return null;
					}
					else {
						return JsonConvert.DeserializeObject<List<Beer>>(content);
					}


				}
			}
		}
	}
}
