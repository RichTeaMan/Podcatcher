using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.Loader
{
    public class RssDocumentFactory
    {
		public RssDocumentFactory()
        { }

		public async RssDocument LoadDocumentAsync(string address)
        {
            var wc = new WebClient();
            var document = await wc.DownloadStringAsync(new Uri(address));
            wc.DownloadStringTaskAsync(new Uri(address));

                XmlSerializer ser = new XmlSerializer(typeof(RssDocument));
            RssDocument cars;
            using (XmlReader reader = XmlReader.Create(path))
            {
                cars = (Cars)ser.Deserialize(reader);
            }
        }
    }
}
