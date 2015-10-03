using Podcatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.ChunkedDownloader
{
    public class ChunkData : IChunkData

    {
        public uint Length { get; set; }

        public uint Start { get; set; }
    }
}
