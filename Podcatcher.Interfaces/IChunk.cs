using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Interfaces
{
    public interface IChunk : IChunkData
    {
        IEnumerable<byte> Data { get; }
    }
}
