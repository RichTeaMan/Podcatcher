﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace Podcatcher.FileSaver.Tests
{
    [TestClass]
    public class ChunkSaverTest
    {
        const string RESOURCE_NAME = "technology-computer-chips-gigabyte.jpg";
        const int RESOURCE_LENGTH = 3437019;
        const string TEST_OUTPUT = "testDirectory";
        private ChunkSaver ChunkSaver;
        private MemoryStream resourceStream;

        private void CreateResource()
        {
            var bytes = File.ReadAllBytes(RESOURCE_NAME);
            resourceStream = new MemoryStream(bytes);
        }

        private async Task DeleteDirectory()
        {
            var directory = await ChunkSaver.CreateFolder(TEST_OUTPUT);
            await directory.DeleteAsync();
        }

        [TestInitialize]
        public async void Initialise()
        {
            ChunkSaver = new ChunkSaver();
            CreateResource();
            await DeleteDirectory();
        }

        [TestCleanup]
        public async void Cleanup()
        {
            await DeleteDirectory();
        }

        [TestMethod]
        public void HasResource()
        {
            Assert.IsNotNull(resourceStream);
            Assert.AreEqual(resourceStream.Length, RESOURCE_LENGTH);
        }

        [TestMethod]
        public async Task SaveChunksInOrder()
        {
            int position = 0;

            while (position < RESOURCE_LENGTH)
            {
                var buffer = new byte[1024 * 50];
                int read = resourceStream.Read(buffer, 0, buffer.Length);
                await ChunkSaver.SaveFile(TEST_OUTPUT, position, buffer, read);
                position += read;
            }
            Assert.AreEqual(RESOURCE_LENGTH, position);
        }

        [TestMethod]
        public async Task GetNextChunk()
        {
            var firstChunk = await ChunkSaver.GetNextEmptyChunk(TEST_OUTPUT, 0);

            Assert.AreEqual(0, firstChunk.Start);
            Assert.AreEqual(int.MaxValue, firstChunk.Length);

            int bufferLength = 1024 * 50;
            var buffer = new byte[bufferLength];
            int read1 = resourceStream.Read(buffer, 0, bufferLength);
            await ChunkSaver.SaveFile(TEST_OUTPUT, 0, buffer, read1);

            var secondChunk = await ChunkSaver.GetNextEmptyChunk(TEST_OUTPUT, 0);

            Assert.AreEqual(read1, secondChunk.Start);
            Assert.AreEqual(int.MaxValue, secondChunk.Length);

            int read2 = resourceStream.Read(buffer, 0, bufferLength);
            int read3 = resourceStream.Read(buffer, 0, bufferLength);
            await ChunkSaver.SaveFile(TEST_OUTPUT, read2, buffer, read3);

            var thirdChunk = await ChunkSaver.GetNextEmptyChunk(TEST_OUTPUT, 0);

            Assert.AreEqual(read1, thirdChunk.Start);
            Assert.AreEqual(read3, thirdChunk.Length);
        }

        [TestMethod]
        public async Task SaveFile()
        {
            await SaveChunksInOrder();
            var comStream = await ChunkSaver.CreateCombinedStream(TEST_OUTPUT);

            byte[] comData;
            using (var ms = new MemoryStream())
            {
                comStream.CopyTo(ms);
                ms.Position = 0;
                comData = ms.ToArray();
            }
            var resourceData = resourceStream.ToArray();
            CollectionAssert.AreEqual(resourceData, comData);
        }

    }
}
