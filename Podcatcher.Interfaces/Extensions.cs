using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Interfaces
{
   public static class Extensions
    {
        public static uint End(this IChunkData chunkData)
        {
            return chunkData.Start + chunkData.Length + 1;
        }
    }
}
