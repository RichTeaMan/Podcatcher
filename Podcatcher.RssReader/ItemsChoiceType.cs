using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    [XmlType(IncludeInSchema = false)]
    public enum ItemsChoiceType
    {
        copyright,
        description,
        [XmlEnum("http://rssnamespace.org/feedburner/ext/1.0:info")]
        info,
        [XmlEnum("http://search.yahoo.com/mrss/:category")]
        category,
        [XmlEnum("http://search.yahoo.com/mrss/:copyright")]
        copyright1,
        [XmlEnum("http://search.yahoo.com/mrss/:credit")]
        credit,
        [XmlEnum("http://search.yahoo.com/mrss/:description")]
        description1,
        [XmlEnum("http://search.yahoo.com/mrss/:rating")]
        rating,
        [XmlEnum("http://search.yahoo.com/mrss/:thumbnail")]
        thumbnail,
        [XmlEnum("http://www.itunes.com/dtds/podcast-1.0.dtd:author")]
        author,
        [XmlEnum("http://www.itunes.com/dtds/podcast-1.0.dtd:category")]
        category1,
        [XmlEnum("http://www.itunes.com/dtds/podcast-1.0.dtd:explicit")]
        @explicit,
        [XmlEnum("http://www.itunes.com/dtds/podcast-1.0.dtd:image")]
        image,
        [XmlEnum("http://www.itunes.com/dtds/podcast-1.0.dtd:owner")]
        owner,
        [XmlEnum("http://www.itunes.com/dtds/podcast-1.0.dtd:subtitle")]
        subtitle,
        [XmlEnum("http://www.w3.org/2005/Atom:link")]
        link,
        [XmlEnum("image")]
        image1,
        item,
        language,
        lastBuildDate,
        [XmlEnum("link")]
        link1,
        pubDate,
        title,
        ttl,
        webMaster,
    }
}
