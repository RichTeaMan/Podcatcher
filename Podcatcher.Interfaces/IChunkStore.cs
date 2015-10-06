using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Interfaces
{
	public interface IChunkStore : IReadOnlyDataStore
    {
		Task<IChunkData> StoreChunk(uint start, byte[] data);
		IEnumerable<IChunkData> GetNextEmptyChunk();
    }
}
