using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.FileSaver
{
    public class ChunkReader
    {
        public ChunkReader()
        {

        }

        public async Task<ChunkData> GetNextEmptyChunk(string filepath, int startPosition = 0)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChunkData> GetEmptyChunks(string filepath)
        {
            int position = 0;
            ChunkData chunkData;
            // TODO: Could this ever actually work?
            while((chunkData = GetNextEmptyChunk(filepath, position).Result) != null)
            {
                yield return chunkData;
            }
        }

        public async Task<int> GetFileLength(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
