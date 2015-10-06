using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Interfaces
{
    public interface IReadOnlyDataStore
    {
		/// <summary>
		/// Determines whether this instance is chunkable.
		/// </summary>
		/// <returns><c>true</c> if this instance is chunkable; otherwise, <c>false</c>.</returns>
        Task<bool> IsChunkable();
		/// <summary>
		/// Gets the specified chunk. The result should be checked that it is the chunk requested.
		/// </summary>
		/// <returns>The chunk.</returns>
		/// <param name="chunkData">Chunk data.</param>
        Task<IChunk> GetChunk(IChunkData chunkData);
		/// <summary>
		/// Gets all data as a continuous byte array. This should be used sparingly, such as
		/// if the store is not chunkable.
		/// </summary>
		/// <returns>The data.</returns>
        Task<byte[]> GetData();
    }
}
