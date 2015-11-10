using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class rss
    {
        public rssChannel channel { get; set; }

        [XmlAttribute]
        public decimal version { get; set; }
    }

}
