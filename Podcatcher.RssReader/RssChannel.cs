using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(AnonymousType = true)]
    public class rssChannel
    {
        [XmlElement("title", typeof(string))]
        public string Title { get; set; }

        [XmlElement("copyright", typeof(string))]
        public string Copyright { get; set; }

        [XmlElement("description", typeof(string))]
        public string Description { get; set; }

        [XmlElement("rating", typeof(string), Namespace = "http://search.yahoo.com/mrss/")]
        public string Rating { get; set; }

        [XmlElement("author", typeof(string), Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Author { get; set; }

        [XmlElement("language", typeof(string))]
        public string Language { get; set; }

        [XmlElement("lastBuildDate", typeof(string))]
        public string LastBuildDate { get; set; }

        [XmlElement("link", typeof(string))]
        public string Link { get; set; }

        [XmlElement("pubDate", typeof(string))]
        public string PubDate { get; set; }

        [XmlElement("explicit", typeof(string), Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string Explicit { get; set; }

        [XmlElement("subtitle", typeof(string), Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string SubTitle { get; set; }

        [XmlElement("webMaster", typeof(string))]
        public string WebMaster { get; set; }

        /// <remarks/>

        [XmlElement("info", typeof(info), Namespace = "http://rssnamespace.org/feedburner/ext/1.0")]
        [XmlElement("category", typeof(category), Namespace = "http://search.yahoo.com/mrss/")]
        [XmlElement("credit", typeof(credit), Namespace = "http://search.yahoo.com/mrss/")]
        [XmlElement("description", typeof(description), Namespace = "http://search.yahoo.com/mrss/")]
        [XmlElement("thumbnail", typeof(thumbnail), Namespace = "http://search.yahoo.com/mrss/")]
        [XmlElement("category", typeof(category1), Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        [XmlElement("image", typeof(image), Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        [XmlElement("owner", typeof(owner), Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        [XmlElement("link", typeof(link), Namespace = "http://www.w3.org/2005/Atom")]
        [XmlElement("image", typeof(rssChannelImage))]
        [XmlElement("ttl", typeof(byte))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public object[] Misc { get; set; }

        [XmlElement("item", typeof(rssChannelItem))]
        public rssChannelItem[] Items { get; set; }

        [XmlElement("ItemsElementName")]
        [XmlIgnore]
        public ItemsChoiceType[] ItemsElementName { get; set; }
    }

}
