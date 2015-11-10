using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    public class RssFactory
    {

        public rss CreateFromStream(Stream xmlStream)
        {
            XmlSerializer ser = new XmlSerializer(typeof(rss));
            var rss = (rss)ser.Deserialize(xmlStream);
            return rss;
        }

        public async Task<rss> CreateFromUrl(string url)
        {
            using (var message = new HttpRequestMessage(
                HttpMethod.Get,
                url))
            using (var wc = new HttpClient())
            using (var response = await wc.SendAsync(message))
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return CreateFromStream(responseStream);
            }
        }

    }
}
