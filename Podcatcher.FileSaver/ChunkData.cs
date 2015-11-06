using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.FileSaver
{
    public class ChunkData
    {
        public int Start { get; protected set; }
        public int Length { get; protected set; }

        protected ChunkData() { }

        public ChunkData(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
