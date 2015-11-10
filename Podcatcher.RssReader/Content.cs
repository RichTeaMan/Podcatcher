using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
    [XmlRoot(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
    public class content
    {
        [XmlAttribute]
        public string url { get; set; }

        [XmlAttribute]
        public uint fileSize { get; set; }

        [XmlAttribute]
        public string type { get; set; }
    }

}
