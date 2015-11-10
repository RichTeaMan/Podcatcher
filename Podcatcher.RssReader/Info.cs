using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true, Namespace = "http://rssnamespace.org/feedburner/ext/1.0")]
    [XmlRoot(Namespace = "http://rssnamespace.org/feedburner/ext/1.0", IsNullable = false)]
    public class info
    {
        [XmlAttribute]
        public string uri { get; set; }
    }

}
