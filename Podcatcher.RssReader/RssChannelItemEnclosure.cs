using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true)]
    public class rssChannelItemEnclosure
    {
        [XmlAttribute]
        public string type { get; set; }

        [XmlAttribute]
        public string url { get; set; }

        [XmlAttribute]
        public uint length { get; set; }
    }

}
