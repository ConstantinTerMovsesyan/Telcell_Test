using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;


namespace PasteWebApiApp.Services
{
    public class PasteRESTfulApiClientService
    {
        HttpClient client = new HttpClient();


        public PasteRESTfulApiClientService()
        {
            client.BaseAddress = new Uri("https://pastebin.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
        }


        // GET: https://pastebin.com/raw/xy8T3gSu
        public string GetPasteById(string id)
        {
            Task<HttpResponseMessage> httpResponseTask = client.GetAsync("/raw/" + id);
            HttpResponseMessage response = httpResponseTask.GetAwaiter().GetResult();

            if (! response.IsSuccessStatusCode)
            {
                return "";
            }

            Task<string> taskGetProduct = response.Content.ReadAsStringAsync();
            return taskGetProduct.GetAwaiter().GetResult();
        }
    }
}