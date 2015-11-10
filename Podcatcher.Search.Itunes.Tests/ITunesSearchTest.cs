using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Search.Itunes.Tests
{
    [TestClass]
    public class ITunesSearchTest
    {
        const string RESOURCE_NAME = "itunesJamesBondingResult.json";

        private Stream resultStream;
        private string resultJson;
        private ItunesSearchFactory factory;

        [TestInitialize]
        public void Initialise()
        {
            resultStream = File.Open(RESOURCE_NAME, FileMode.Open);
            using (var reader = new StreamReader(resultStream, Encoding.UTF8))
            {
                resultJson = reader.ReadToEnd();
            }
            factory = new ItunesSearchFactory();
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (resultStream != null)
            {
                resultStream.Close();
            }
        }

        [TestMethod]
        public void ItunesResultDeserialisation()
        {
            var response = factory.DeserialiseJsonResult(resultJson);
            Assert.AreEqual(5, response.resultCount);
            Assert.AreEqual("http://feeds.feedburner.com/JamesBonding", response.results[0].feedUrl);
        }

        [TestMethod]
        public async Task WebItunesSearch()
        {
            await factory.Search("James Bonding");
        }
    }
}
