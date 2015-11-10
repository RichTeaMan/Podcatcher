using System.Threading.Tasks;
using Podcatcher.Domain;

namespace Podcatcher.Downloader
{
    public interface IChunkedDownloader
    {
        /// <summary>
        /// Downloads an IChunk from the given IChunkInfo.
        /// 
        /// The data returned may be smaller than the requested
        /// length if there is not enough data.
        /// 
        /// Return null if there is no data at the given position.
        /// </summary>
        /// <param name="chunkInfo"></param>
        /// <returns></returns>
        Task<IChunk> DownloadChunk(IChunkInfo chunkInfo);
    }
}