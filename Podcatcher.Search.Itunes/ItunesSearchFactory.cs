using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Search.Itunes
{
    public class ItunesSearchFactory
    {
        public string GetSearchUrl(string terms)
        {
            string cleanTerms = terms.Trim().Replace(' ', '+');
            string url = string.Format("http://itunes.apple.com/search?term={0}&entity=podcast", cleanTerms);
            return url;
        }

        public async Task<SearchObject> Search(string terms)
        {
            string url = GetSearchUrl(terms);

            using (var message = new HttpRequestMessage(
                HttpMethod.Get,
                url))
            using (var wc = new HttpClient())
            using (var response = await wc.SendAsync(message))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = DeserialiseJsonResult(responseString);
                return result;
            }
        }

        public SearchObject DeserialiseJsonResult(string json)
        {
            var result = JsonConvert.DeserializeObject<SearchObject>(json);
            return result;
        }
    }
}
