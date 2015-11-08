using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Podcatcher.Manager.Tests
{
    [TestClass]
    public class FileDownloadTest
    {
        const string RESOURCE_LINK = "https://upload.wikimedia.org/wikipedia/commons/9/96/Macrocranion_tupaiodon_01.jpg";
        const int RESOURCE_LENGTH = 8248016;

        const string OUTPUT = "test.jpg";

        FileDownload downloader;

        [TestInitialize]
        public void Initialise()
        {
            downloader = new FileDownload(RESOURCE_LINK, OUTPUT);
        }

        [TestMethod]
        public async Task CompleteDownload()
        {
            while(!downloader.Complete)
            {
                await downloader.DownloadAndSaveChunk();
            }

            using (var fs = await downloader.GetCompleteFileStream())
            {
                Assert.AreEqual(RESOURCE_LENGTH, fs.Length);
            }
        }
    }
}
