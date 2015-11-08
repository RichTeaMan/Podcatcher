using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Domain
{
    public class ChunkInfo : IChunkInfo
    {
        public int Start { get; private set;  }
        public int Length { get; private set; }

        public ChunkInfo(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
