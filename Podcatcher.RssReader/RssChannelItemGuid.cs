using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true)]
    public class rssChannelItemGuid
    {
        [XmlAttribute]
        public bool isPermaLink { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

}
