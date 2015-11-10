using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace Podcatcher.RssReader.Tests
{
    [TestClass]
    public class RssFactoryTest
    {
        const string RESOURCE_NAME = "DenzelWashingtonIsTheGreatest.xml";
        const string RESOURCE_LINK = "http://feeds.feedburner.com/DenzelWashingtonIsTheGreatest?format=xml";

        const string RESOURCE_NAME2 = "JamesBonding.xml";


        private Stream RssStream1;
        private Stream RssStream2;
        private RssFactory rssFactory;

        [TestInitialize]
        public void Initialise()
        {
            RssStream1 = File.Open(RESOURCE_NAME, FileMode.Open);
            RssStream2 = File.Open(RESOURCE_NAME2, FileMode.Open);
            rssFactory = new RssFactory();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (RssStream1 != null)
            {
                RssStream1.Close();
            }
            if (RssStream2 != null)
            {
                RssStream2.Close();
            }
        }

        [TestMethod]
        public void RssDeserialisation()
        {
            var rss1 = rssFactory.CreateFromStream(RssStream1);
            Assert.AreEqual("Denzel Washington is the Greatest Actor of All Time Period", rss1.channel.Title);
            
            var rss2 = rssFactory.CreateFromStream(RssStream2);
            Assert.AreEqual("James Bonding", rss2.channel.Title);
        }

        [TestMethod]
        public async Task WebRssDeserialisation()
        {
            var rss = await rssFactory.CreateFromUrl(RESOURCE_LINK);
            Assert.AreEqual("Denzel Washington is the Greatest Actor of All Time Period", rss.channel.Title);
        }
    }
}
