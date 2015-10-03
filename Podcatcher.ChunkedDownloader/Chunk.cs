using Podcatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.ChunkedDownloader
{
    public class Chunk : ChunkData, IChunk
    {
        public byte[] Data { get; set; }

        IEnumerable<byte> IChunk.Data
        {
            get
            {
                return Data;
            }
        }
    }
}
