using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using Podcatcher.Domain;

namespace Podcatcher.Downloader.Tests
{
    [TestClass]
    public class ChunkedDownloaderTest
    {
        const string RESOURCE_LINK = "https://upload.wikimedia.org/wikipedia/commons/9/96/Macrocranion_tupaiodon_01.jpg";

        ChunkedDownloader Downloader;

        [TestInitialize]
        public void Initialise()
        {
            Downloader = new ChunkedDownloader();
        }

        [TestMethod]
        public async Task ResourceAvailable()
        {
            using (var message = new HttpRequestMessage(
                HttpMethod.Head,
                RESOURCE_LINK))
            using (var wc = new HttpClient())
            using (var response = await wc.SendAsync(message))
            {
                Assert.AreEqual(200, (int)response.StatusCode);
            }
        }

        [TestMethod]
        public async Task Download()
        {
            // get 1kb from start
            var chunkInfo = new ChunkInfo(0, 1024);
            var chunk = await Downloader.DownloadChunk(RESOURCE_LINK, chunkInfo);

            Assert.AreEqual(chunkInfo.Start, chunk.Start);
            Assert.AreEqual(chunkInfo.Length, chunk.Length);
        }
    }
}
