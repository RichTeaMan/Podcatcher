﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Podcatcher.Domain;

namespace Podcatcher.Downloader
{
    public class ChunkedDownloader
    {
        public ChunkedDownloader()
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
                    chunkData.Start + chunkData.Length
                );

                using (var wc = new HttpClient())
                using (var response = await wc.SendAsync(message))
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    int start = chunkData.Start;
                    int length = content.Length;
                    var chunk = new Chunk(start, length, content);
                    return chunk;
                }
            }
        }
    }
}
