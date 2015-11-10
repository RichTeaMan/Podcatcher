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
    public class thumbnail
    {
        [XmlAttribute]
        public string url { get; set; }
    }

}
