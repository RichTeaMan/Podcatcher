using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Manager
{
    public interface IChunk
    {
        int Start { get; }
        int Length { get; }
        byte[] Data { get; }
    }
}
