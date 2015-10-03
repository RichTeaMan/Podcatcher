using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Interfaces
{
    public interface IChunkData
    {
        uint Start { get; }
        uint Length { get; }
    }
}
