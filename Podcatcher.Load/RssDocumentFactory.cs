using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using Podcatcher.Interfaces;

namespace Podcatcher.Load
{
    public class RssDocumentFactory
    {
        public RssDocumentFactory()
        {
            
        }

        public async Task<IRssDocument> GetRssDocument(string address)
        {
            var stream = await GetRssStreamFromWeb(address);
            var document = GetRssDocument(stream);
            return document;
        }

        public IRssDocument GetRssDocument(Stream stream)
        {
            XmlSerializer ser = new XmlSerializer(typeof(rss));
            rss rssDocument;
            using (XmlReader reader = XmlReader.Create(stream))
            {

                rssDocument = (rss)ser.Deserialize(reader);
                return rssDocument;
            }
        }

        private async Task<Stream> GetRssStreamFromWeb(string address)
        {
            using (var wc = new HttpClient())
            {
                var response = await wc.GetAsync(address);
                var stream = await response.Content.ReadAsStreamAsync();
                return stream;
            }
        }
    }
}
