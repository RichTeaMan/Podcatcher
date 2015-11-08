using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Podcatcher.Domain;

namespace Podcatcher.Downloader.Tests
{
    [TestClass]
    public class HttpChunkedDownloaderTest
    {
        const string RESOURCE_LINK = "https://upload.wikimedia.org/wikipedia/commons/9/96/Macrocranion_tupaiodon_01.jpg";
        const int RESOURCE_LENGTH = 8248016;

        HttpChunkedDownloader Downloader;

        [TestInitialize]
        public void Initialise()
        {
            Downloader = new HttpChunkedDownloader();
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
                
                //var lengthHeader = response.Headers.GetValues("content-length");
                //var length = int.Parse(lengthHeader.First());
                Assert.AreEqual(200, (int)response.StatusCode);
                //Assert.AreEqual(RESOURCE_LENGTH, length);
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

        [TestMethod]
        public async Task DownloadNearEnd()
        {
            // get 1kb from near end
            var chunkInfo = new ChunkInfo(RESOURCE_LENGTH - 512, 1024);
            var chunk = await Downloader.DownloadChunk(RESOURCE_LINK, chunkInfo);

            Assert.AreEqual(chunkInfo.Start, chunk.Start);
            Assert.AreEqual(512, chunk.Length);
        }

        //[TestMethod]
        public async Task DownloadEnd()
        {
            // get 1kb from near end
            var chunkInfo = new ChunkInfo(RESOURCE_LENGTH, 1024);

            var chunk = await Downloader.DownloadChunk(RESOURCE_LINK, chunkInfo);
            Assert.AreEqual(chunkInfo.Start, chunk.Start);
            Assert.AreEqual(0, chunk.Length);
        }
    }
}
