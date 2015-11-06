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

        public IEnumerable<ChunkData> GetEmptyChunks(string filepath)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetFileLength(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
