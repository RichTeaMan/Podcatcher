using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Downloader
{
    public class ChunkedDownloader
    {
        public ChunkedDownloader()
        {

        }

        public Task<byte[]> DownloadChunk(int start, int length)
        {
            throw new NotImplementedException();
        }
    }
}
