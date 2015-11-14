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
        const string RESOURCE_NAME1 = "itunesJamesBondingResult.json";
        const string RESOURCE_NAME2 = "itunesDenzelWashingtonResult.json";

        private ItunesSearchFactory factory;

        private string GetFileContents(string filename)
        {
            using (var resultStream = File.Open(filename, FileMode.Open))
            using (var reader = new StreamReader(resultStream, Encoding.UTF8))
            {
                var resultJson = reader.ReadToEnd();
                return resultJson;
            }
        }

        [TestInitialize]
        public void Initialise()
        {
            
            factory = new ItunesSearchFactory();
        }

        [TestMethod]
        public void ItunesResult1Deserialisation()
        {
            var resultJson = GetFileContents(RESOURCE_NAME1);
            var response = factory.DeserialiseJsonResult(resultJson);
            Assert.AreEqual(5, response.resultCount);
            Assert.AreEqual("http://feeds.feedburner.com/JamesBonding", response.results[0].feedUrl);
        }

        [TestMethod]
        public void ItunesResult2Deserialisation()
        {
            var resultJson = GetFileContents(RESOURCE_NAME2);
            var response = factory.DeserialiseJsonResult(resultJson);
        }

        [TestMethod]
        public async Task WebItunesSearch()
        {
            await factory.Search("James Bonding");
        }
    }
}
