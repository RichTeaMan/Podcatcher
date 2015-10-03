using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Interfaces
{
    public interface IChunkStore
    {
        IEnumerable<IChunkData> GetChunkDatas();
        IEnumerable<IChunk> GetChunks();
        IChunkData StoreChunk(uint start, byte[] data);
    }
}
