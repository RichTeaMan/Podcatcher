using System;
using System.Linq;

using Podcatcher.FileSaver;
using Podcatcher.Downloader;
using System.Threading.Tasks;
using Podcatcher.Domain;
using System.IO;

namespace Podcatcher.Manager
{
    public class FileDownload
    {
        public string SourceLink { get; private set; }
        public string Destination { get; private set; }

        public int ChunkLength { get; set; } = 1024 * 512;

        protected HttpChunkedDownloader ChunkDownloader { get; set; }
        protected ChunkSaver ChunkSaver { get; set; }

        public bool Complete { get; private set; }

        public FileDownload(string sourceLink, string destination)
        {
            SourceLink = sourceLink;
            Destination = destination;

            ChunkDownloader = new HttpChunkedDownloader();
            ChunkSaver = new ChunkSaver();

            Complete = false;
        }

        public async Task<IChunk> DownloadChunk()
        {
            var chunkInfo = await ChunkSaver.GetNextEmptyChunk(Destination);

            var checkedChunkInfo = new ChunkInfo(
                chunkInfo.Start,
                Math.Min(ChunkLength, chunkInfo.Length)
            );
            var chunk = await ChunkDownloader.DownloadChunk(SourceLink, checkedChunkInfo);
            return chunk;
        }

        public async Task SaveChunk(IChunk chunk)
        {
            await ChunkSaver.SaveFile(Destination, chunk.Start, chunk.Data);
        }

        public async Task DownloadAndSaveChunk()
        {
            var chunk = await DownloadChunk();
            if (chunk.Start > 0 && chunk.Length == 0)
            {
                Complete = true;
            }
            else
            {
                await SaveChunk(chunk);
            }
        }

        public async Task<Stream> GetCompleteFileStream()
        {
            var stream = await ChunkSaver.CreateCombinedStream(Destination);
            return stream;
        }
    }
}
