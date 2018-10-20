using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ThePlaceToBe.Views.Data;

namespace ThePlaceToBe.Data
{
    public class RestService
    {

        public static HttpClient client;
        public static Dictionary<string, string> dic;

        public RestService()
        {
        }

        public static async Task<List<Beer>> Request(Dictionary<string, string> dic, string page)
        {

            if (client == null) client = new HttpClient();
            Dictionary<string, string> values = dic;
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("http://51.38.239.219/admin/" + page + ".php", content).ConfigureAwait(false);
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Beer>>(responseString);
        }

        public static async Task<List<User>> RequestProfil(Dictionary<string, string> dic, string page)
        {

            if (client == null) client = new HttpClient();
            Dictionary<string, string> values = dic;
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("http://51.38.239.219/admin/" + page + ".php", content).ConfigureAwait(false);
            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<User>>(responseString);
        }
    }
}