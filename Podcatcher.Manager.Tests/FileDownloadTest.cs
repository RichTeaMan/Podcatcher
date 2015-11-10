using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using Podcatcher.Downloader;
using PCLStorage;

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

        private async Task DeleteDirectory()
        {
            var fileExists = await FileSystem.Current.LocalStorage.CheckExistsAsync(OUTPUT);
            if (fileExists == ExistenceCheckResult.FileExists)
            {
                var file = await FileSystem.Current.LocalStorage.GetFileAsync(OUTPUT);
                await file.DeleteAsync();
            }

            var directory = await downloader.ChunkSaver.CreateFolder(OUTPUT);
            await directory.DeleteAsync();
        }

        [TestInitialize]
        public async void Initialise()
        {
            downloader = new FileDownload(EXT_RESOURCE_LINK, OUTPUT);
            await DeleteDirectory();
        }

        [TestCleanup]
        public async void Cleanup()
        {
            await DeleteDirectory();
        }

        [TestMethod]
        public async Task CompleteDownloadProgress()
        {
            var bytes = File.ReadAllBytes(RESOURCE_NAME);
            int length = 0;
            downloader.ChunkSaved += (sender, chunk) =>
            {
                length += chunk.Length;
                var bytesSaved = sender.GetBytesSavedCount().Result;
                Assert.AreEqual(length, bytesSaved);
            };
            using (var ms = new MemoryStream(bytes))
            {
                downloader.ChunkDownloader = new FileChunkedDownloader() { FileStream = ms };
                while (!downloader.Complete)
                {
                    await downloader.DownloadAndSaveChunk();
                }
            }
        }

        [TestMethod]
        public async Task CompleteDownloadFromFile()
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

        //[TestMethod]
        public async Task CompleteDownloadFromWeb()
        {
            while (!downloader.Complete)
            {
                await downloader.DownloadAndSaveChunk();
            }

            using (var fs = await downloader.GetCompleteFileStream())
            {
                Assert.AreEqual(EXT_RESOURCE_LENGTH, fs.Length);
            }
        }
    }
}
