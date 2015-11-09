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

        private Stream RssStream;
        private RssFactory rssFactory;

        [TestInitialize]
        public void Initialise()
        {
            RssStream = File.Open(RESOURCE_NAME, FileMode.Open);
            rssFactory = new RssFactory();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (RssStream != null)
            {
                RssStream.Close();
            }
        }

        [TestMethod]
        public void RssDeserialisation()
        {
            var rss = rssFactory.CreateFromStream(RssStream);
            Assert.AreEqual("Denzel Washington is the Greatest Actor of All Time Period", rss.channel.Title);
        }

        [TestMethod]
        public async Task WebRssDeserialisation()
        {
            var rss = await rssFactory.CreateFromUrl(RESOURCE_LINK);
            Assert.AreEqual("Denzel Washington is the Greatest Actor of All Time Period", rss.channel.Title);
        }
    }
}
