using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
    [XmlRoot(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
    public class link
    {
        [XmlAttribute]
        public string rel { get; set; }

        [XmlAttribute]
        public string type { get; set; }

        [XmlAttribute]
        public string href { get; set; }
    }

}
