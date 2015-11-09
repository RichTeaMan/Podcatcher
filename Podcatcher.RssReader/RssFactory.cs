﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Podcatcher.RssReader
{
    public class RssFactory
    {

        public rss CreateFromStream(Stream xmlStream)
        {
            XmlSerializer ser = new XmlSerializer(typeof(rss));
            var rss = (rss)ser.Deserialize(xmlStream);
            return rss;
        }

        public async Task<rss> CreateFromUrl(string url)
        {
            using (var message = new HttpRequestMessage(
                HttpMethod.Get,
                url))
            using (var wc = new HttpClient())
            using (var response = await wc.SendAsync(message))
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return CreateFromStream(responseStream);
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class rss
        {

            private rssChannel channelField;

            private decimal versionField;

            /// <remarks/>
            public rssChannel channel
            {
                get
                {
                    return this.channelField;
                }
                set
                {
                    this.channelField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannel
        {
            private ItemsChoiceType[] itemsElementNameField;

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

            /// <remarks/>
            [XmlElement("ItemsElementName")]
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public ItemsChoiceType[] ItemsElementName
            {
                get
                {
                    return this.itemsElementNameField;
                }
                set
                {
                    this.itemsElementNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://rssnamespace.org/feedburner/ext/1.0")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://rssnamespace.org/feedburner/ext/1.0", IsNullable = false)]
        public partial class info
        {

            private string uriField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string uri
            {
                get
                {
                    return this.uriField;
                }
                set
                {
                    this.uriField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
        public partial class category
        {

            private string schemeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string scheme
            {
                get
                {
                    return this.schemeField;
                }
                set
                {
                    this.schemeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
        public partial class credit
        {

            private string roleField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string role
            {
                get
                {
                    return this.roleField;
                }
                set
                {
                    this.roleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
        public partial class description
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
        public partial class thumbnail
        {

            private string urlField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        [System.Xml.Serialization.XmlRootAttribute("category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
        public partial class category1
        {

            private string textField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
        public partial class image
        {

            private string hrefField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string href
            {
                get
                {
                    return this.hrefField;
                }
                set
                {
                    this.hrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
        public partial class owner
        {

            private string emailField;

            private string nameField;

            /// <remarks/>
            public string email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }

            /// <remarks/>
            public string name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
        public partial class link
        {

            private string relField;

            private string typeField;

            private string hrefField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rel
            {
                get
                {
                    return this.relField;
                }
                set
                {
                    this.relField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string href
            {
                get
                {
                    return this.hrefField;
                }
                set
                {
                    this.hrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelImage
        {

            private string urlField;

            private string titleField;

            private string linkField;

            /// <remarks/>
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelItem
        {

            private rssChannelItemGuid guidField;

            private string titleField;

            private string pubDateField;

            private string linkField;

            private System.DateTime durationField;

            private string authorField;

            private string explicitField;

            private string summaryField;

            private string subtitleField;

            private string descriptionField;

            private rssChannelItemEnclosure enclosureField;

            private image imageField;

            private string author1Field;

            private content contentField;

            /// <remarks/>
            public rssChannelItemGuid guid
            {
                get
                {
                    return this.guidField;
                }
                set
                {
                    this.guidField = value;
                }
            }

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string pubDate
            {
                get
                {
                    return this.pubDateField;
                }
                set
                {
                    this.pubDateField = value;
                }
            }

            /// <remarks/>
            public string link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", DataType = "time")]
            public System.DateTime duration
            {
                get
                {
                    return this.durationField;
                }
                set
                {
                    this.durationField = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string author
            {
                get
                {
                    return this.authorField;
                }
                set
                {
                    this.authorField = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string @explicit
            {
                get
                {
                    return this.explicitField;
                }
                set
                {
                    this.explicitField = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string summary
            {
                get
                {
                    return this.summaryField;
                }
                set
                {
                    this.summaryField = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public string subtitle
            {
                get
                {
                    return this.subtitleField;
                }
                set
                {
                    this.subtitleField = value;
                }
            }

            /// <remarks/>
            public string description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            public rssChannelItemEnclosure enclosure
            {
                get
                {
                    return this.enclosureField;
                }
                set
                {
                    this.enclosureField = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
            public image image
            {
                get
                {
                    return this.imageField;
                }
                set
                {
                    this.imageField = value;
                }
            }

            /// <remarks/>
            [XmlElement("author")]
            public string author1
            {
                get
                {
                    return this.author1Field;
                }
                set
                {
                    this.author1Field = value;
                }
            }

            /// <remarks/>
            [XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
            public content content
            {
                get
                {
                    return this.contentField;
                }
                set
                {
                    this.contentField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelItemGuid
        {

            private bool isPermaLinkField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool isPermaLink
            {
                get
                {
                    return this.isPermaLinkField;
                }
                set
                {
                    this.isPermaLinkField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelItemEnclosure
        {

            private string typeField;

            private string urlField;

            private uint lengthField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint length
            {
                get
                {
                    return this.lengthField;
                }
                set
                {
                    this.lengthField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
        public partial class content
        {

            private string urlField;

            private uint fileSizeField;

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint fileSize
            {
                get
                {
                    return this.fileSizeField;
                }
                set
                {
                    this.fileSizeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
        public enum ItemsChoiceType
        {

            /// <remarks/>
            copyright,

            /// <remarks/>
            description,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://rssnamespace.org/feedburner/ext/1.0:info")]
            info,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://search.yahoo.com/mrss/:category")]
            category,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://search.yahoo.com/mrss/:copyright")]
            copyright1,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://search.yahoo.com/mrss/:credit")]
            credit,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://search.yahoo.com/mrss/:description")]
            description1,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://search.yahoo.com/mrss/:rating")]
            rating,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://search.yahoo.com/mrss/:thumbnail")]
            thumbnail,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.itunes.com/dtds/podcast-1.0.dtd:author")]
            author,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.itunes.com/dtds/podcast-1.0.dtd:category")]
            category1,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.itunes.com/dtds/podcast-1.0.dtd:explicit")]
            @explicit,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.itunes.com/dtds/podcast-1.0.dtd:image")]
            image,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.itunes.com/dtds/podcast-1.0.dtd:owner")]
            owner,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.itunes.com/dtds/podcast-1.0.dtd:subtitle")]
            subtitle,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("http://www.w3.org/2005/Atom:link")]
            link,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("image")]
            image1,

            /// <remarks/>
            item,

            /// <remarks/>
            language,

            /// <remarks/>
            lastBuildDate,

            /// <remarks/>
            [System.Xml.Serialization.XmlEnumAttribute("link")]
            link1,

            /// <remarks/>
            pubDate,

            /// <remarks/>
            title,

            /// <remarks/>
            ttl,

            /// <remarks/>
            webMaster,
        }


    }
}