using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true)]
    public class rssChannelItem
    {
        public rssChannelItemGuid guid { get; set; }

        /// <remarks/>
        public string title { get; set; }

        /// <remarks/>
        public string pubDate { get; set; }

        /// <remarks/>
        public string link { get; set; }

        //TODO: Make this a TimeSpan
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string duration { get; set; }

        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string author { get; set; }

        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string @explicit { get; set; }

        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string summary { get; set; }

        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string subtitle { get; set; }

        public string description { get; set; }

        public rssChannelItemEnclosure enclosure { get; set; }

        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public image image { get; set; }

        [XmlElement("author")]
        public string author1 { get; set; }

        [XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public content content { get; set; }
    }

}
