using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Libra_Nest.Data
{
    public class Books
    {
        public dynamic book;

        public string title;
        public string author;
        public string series;
        public string type;
        public string category;
        private string uri;
        public bool done;
        public bool spinner;
        public bool couldNotFindResults;
        public int copies;

        public Books(string title)
        {
            this.title = formatString(title);
            uri = $"https://book-finder1.p.rapidapi.com/api/search?title={title}{author}{series}{type}{category}&lexile_min=600&lexile_max=800&results_per_page=25&page=1";
            // getBook();
            // getBook();
        }
        public Books(string title, string author)
        {
            this.title = formatString(title);
            this.author = author != "" ? formatString("&author=" + author) : "";
            uri = $"https://book-finder1.p.rapidapi.com/api/search?title={title}&author={author}&lexile_min=600&lexile_max=800&results_per_page=25&page=1";
            // getBook();
            // getBook();
        }

        public Books() { }
        public async Task getBook()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri),
                Headers =
                {
                    { "X-RapidAPI-Key", "2a7766b8f2msh0a7201216f47101p168a81jsnda430dacfd80" },
                    { "X-RapidAPI-Host", "book-finder1.p.rapidapi.com" },
                },
            };
            // spinner = true;
            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject(body);
                    // return body;
                }
            }
            catch (Exception ex)
            {
                couldNotFindResults = true;
            }

            spinner = false;
        }

        private static string formatString(string text)
        {
            if(text!=null)
                text = text.Replace(" ", "%20");
            return text;
        }

    }
}
