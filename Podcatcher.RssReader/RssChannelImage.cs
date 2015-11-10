using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true)]
    public class rssChannelImage
    {
        public string url { get; set; }

        public string title { get; set; }

        public string link { get; set; }
    }

}
