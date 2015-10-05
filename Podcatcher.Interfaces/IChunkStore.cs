using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Interfaces
{
	public interface IChunkStore : IReadonlyDataStore
    {
		Task<IChunkData> StoreChunk(uint start, byte[] data);
		Task<IEnumerable<IChunkData>> GetNextEmptyChunk();
    }
}
