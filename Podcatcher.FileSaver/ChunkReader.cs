using Podcatcher.Domain;
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

        public Task<IChunkInfo> GetNextEmptyChunk(string filepath, int startPosition = 0)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IChunkInfo> GetEmptyChunks(string filepath)
        {
            int position = 0;
            IChunkInfo chunkInfo;
            // TODO: Could this ever actually work?
            while((chunkInfo = GetNextEmptyChunk(filepath, position).Result) != null)
            {
                yield return chunkInfo;
            }
        }

        public Task<int> GetFileLength(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
