using System;
using System.Linq;

using Podcatcher.FileSaver;
using Podcatcher.Downloader;
using System.Threading.Tasks;
using Podcatcher.Domain;

namespace Podcatcher.Manager
{
    public class PodcastDownload
    {
        public string SourceLink { get; private set; }
        public string Destination { get; private set; }

        public int ChunkLength { get; set; }

        protected ChunkedDownloader ChunkDownloader { get; set; }
        protected ChunkSaver ChunkSaver { get; set; }

        public async Task<IChunk> DownloadChunk()
        {
            var chunkData = await ChunkSaver.GetNextEmptyChunk(Destination);
            if (chunkData == null)
            {
                return new Chunk(0, 0, null);
            }
            else
            {
                var chunk = await ChunkDownloader.DownloadChunk(SourceLink, chunkData);
                return chunk;
            }
        }

        public async Task SaveChunk(IChunk chunk)
        {
            await ChunkSaver.SaveFile(Destination, chunk.Start, chunk.Data);
        }

        public async Task DownloadAndSaveChunk()
        {
            var chunk = await DownloadChunk();
            await SaveChunk(chunk);
        }
    }
}
