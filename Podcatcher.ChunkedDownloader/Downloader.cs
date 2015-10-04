using Podcatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.ChunkedDownloader
{
    public class Downloader
    {
        public IChunkStore ChunkStore { get; private set; }

        public string DownloadUrl { get; private set; }

        public ushort DefaultLength { get; set; } = 500;

        protected Downloader() { }

        public Downloader(IChunkStore chunkStore, string downloadUrl)
        {
            ChunkStore = chunkStore;
            DownloadUrl = downloadUrl;
        }

        protected async Task<IChunkData> SelectChunk()
        {
            var storechunks = await ChunkStore.GetChunkDatas();
            var chunks = storechunks.OrderBy(c => c.Start);
            if (chunks.Count() == 0)
            {
                return new ChunkData() { Start = 0, Length = DefaultLength };
            }
            
            uint length = DefaultLength;
            IChunkData lastChunk = chunks.First();

            foreach (var c in chunks.Skip(1))
            {
                // chunks should start and end on the same byte
                if (lastChunk.End() < c.Start)
                {
                    uint l = Math.Min(length, c.Start - lastChunk.End());
                    return new ChunkData()
                    {
                        Start = lastChunk.Start + lastChunk.Length + 1,
                        Length = l
                    };
                }
                else
                {
                    lastChunk = c;
                }
            }

            if(lastChunk == null)
            {
                throw new Exception("No chunks found.");
            }
            else
            {
                return new ChunkData()
                {
                    Start = lastChunk.End(),
                    Length = length
                };
            }
        }

        protected async Task StoreChunk(IChunk chunk)
        {
            await ChunkStore.StoreChunk(chunk.Start, chunk.Data.ToArray());
        }

        protected async Task<IChunk> DownloadChunk(IChunkData chunkData)
        {
            using (var message = new HttpRequestMessage(HttpMethod.Get, DownloadUrl))
            using (var wc = new HttpClient())
            {
                message.Headers.Range = new RangeHeaderValue(chunkData.Start, chunkData.Start + chunkData.Length);
                using (var response = await wc.SendAsync(message))
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var chunk = new Chunk()
                    {
                        Start = chunkData.Start,
                        Length = (uint)content.Length,
                        Data = content
                    };
                    return chunk;
                }
            }
        }

        public async Task DownloadChunk()
        {
            var chunkData = await SelectChunk();
            var chunk = await DownloadChunk(chunkData);
        }


    }
}
