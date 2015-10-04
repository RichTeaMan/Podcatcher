using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Interfaces
{
    public interface IChunkStore
    {
        Task<IEnumerable<IChunkData>> GetChunkDatas();
        Task<IEnumerable<IChunk>> GetChunks();
        Task<IChunkData> StoreChunk(uint start, byte[] data);
    }
}
