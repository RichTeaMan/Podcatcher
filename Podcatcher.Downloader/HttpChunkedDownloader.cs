using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Podcatcher.Domain;

namespace Podcatcher.Downloader
{
    public class HttpChunkedDownloader : IChunkedDownloader
    {
        public HttpChunkedDownloader()
        {
        }

        public async Task<IChunk> DownloadChunk(string url, IChunkInfo chunkData)
        {
            using (var message = new HttpRequestMessage(
                HttpMethod.Get,
                url))
            {
                message.Headers.Range = new RangeHeaderValue(
                    chunkData.Start,
                    chunkData.Start + (chunkData.Length - 1)
                );

                using (var wc = new HttpClient())
                using (var response = await wc.SendAsync(message))
                {
                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.PartialContent:
                            {
                                var content = await response.Content.ReadAsByteArrayAsync();
                                int start = chunkData.Start;
                                int length = content.Length;
                                var chunk = new Chunk(start, length, content);
                                return chunk;
                            }
                        case System.Net.HttpStatusCode.RequestedRangeNotSatisfiable:
                            return null;
                        default:
                            throw new Exception("Unknown response");
                    }
                }
            }
        }

    }
}
