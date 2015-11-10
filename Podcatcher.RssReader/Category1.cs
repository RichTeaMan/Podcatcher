using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    [XmlRoot("category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
    public class category1
    {
        [XmlAttribute]
        public string text { get; set; }
    }


}
