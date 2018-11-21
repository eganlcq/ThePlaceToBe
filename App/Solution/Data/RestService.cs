using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ThePlaceToBe.Data
{
    public class RestService
    {

        public static HttpClient client;
        public static Dictionary<string, string> dic;

        public RestService()
        {
        }

        // This method allow to send a request to a webservice with any typeof returned elements
        public static async Task<List<T>> Request<T>(Dictionary<string, string> dic, string page)
        {

            try
            {
                if (client == null) client = new HttpClient();
                Dictionary<string, string> values = dic;
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync("http://theplacetobe.ovh/admin/" + page + ".php", content).ConfigureAwait(false);
                string responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(responseString);
            }
            catch (Exception e)
            {

                throw new HttpRequestException("Erreur de connexion : " + e.Message.ToString());
            }

        }

        public static async void Request(Dictionary<string, string> dic, string page)
        {

            try
            {
                if (client == null) client = new HttpClient();
                Dictionary<string, string> values = dic;
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                await client.PostAsync("http://theplacetobe.ovh/admin/" + page + ".php", content).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                throw new HttpRequestException("Erreur de connexion : " + e.Message.ToString());
            }

        }


    }

    
}
