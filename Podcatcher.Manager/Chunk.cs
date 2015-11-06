using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Manager
{
    public class Chunk : IChunk
    {
        public byte[] Data
        {
            get; private set;
        }

        public int Length
        {
            get; private set;
        }

        public int Start
        {
            get; private set;
        }

        public Chunk(int start, int length, byte[] data)
        {
            Start = start;
            Length = length;
            Data = data;
        }
    }
}
