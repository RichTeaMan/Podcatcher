using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Interfaces
{
    public interface IReadonlyDataStore
    {
        Task<bool> IsChunkable();

        Task<IChunk> DownloadChunk(IChunkData chunkData);

        Task<byte[]> DownloadData();
    }
}
