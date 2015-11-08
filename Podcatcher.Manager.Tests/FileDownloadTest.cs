using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using Podcatcher.Downloader;

namespace Podcatcher.Manager.Tests
{
    [TestClass]
    public class FileDownloadTest
    {
        const string EXT_RESOURCE_LINK = "https://upload.wikimedia.org/wikipedia/commons/9/96/Macrocranion_tupaiodon_01.jpg";
        const int EXT_RESOURCE_LENGTH = 8248016;

        const string RESOURCE_NAME = "Macrocranion_tupaiodon_01.jpg";
        const int RESOURCE_LENGTH = 8248016;

        const string OUTPUT = "test.jpg";

        FileDownload downloader;

        [TestInitialize]
        public void Initialise()
        {
            downloader = new FileDownload(EXT_RESOURCE_LINK, OUTPUT);
        }

        [TestMethod]
        public async Task CompleteDownload()
        {
            var bytes = File.ReadAllBytes(RESOURCE_NAME);
            using (var ms = new MemoryStream(bytes))
            {
                downloader.ChunkDownloader = new FileChunkedDownloader() { FileStream = ms };
                while (!downloader.Complete)
                {
                    await downloader.DownloadAndSaveChunk();
                }
            }
            using (var fs = await downloader.GetCompleteFileStream())
            {
                Assert.AreEqual(EXT_RESOURCE_LENGTH, fs.Length);
            }
        }
    }
}
