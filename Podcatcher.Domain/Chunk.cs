using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Domain
{
    public class Chunk : ChunkInfo, IChunk
    {
        public byte[] Data { get; private set; }

        public Chunk(int start, byte[] data) : base(start, data.Length)
        {
            Data = data;
        }
    }
}
