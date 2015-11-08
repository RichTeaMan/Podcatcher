using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Domain
{
    public interface IChunkInfo
    {
        int Start { get; }
        int Length { get; }
    }
}
