using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Podcatcher.RssReader.Tests
{
    [TestClass]
    public class UnitTest1
    {
        const string RESOURCE_NAME = "DenzelWashingtonIsTheGreatest.xml";

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
            var rss = rssFactory.createFromString(RssStream);
            Assert.AreEqual("Denzel Washington is the Greatest Actor of All Time Period", rss.channel.Items[0]);
        }
    }
}
