using System.Threading.Tasks;
using Podcatcher.Domain;

namespace Podcatcher.Downloader
{
    public interface IChunkedDownloader
    {
        Task<IChunk> DownloadChunk(string url, IChunkInfo chunkData);
    }
}