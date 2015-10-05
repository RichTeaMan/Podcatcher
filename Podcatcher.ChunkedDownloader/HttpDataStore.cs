using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Podcatcher.Interfaces;

namespace Podcatcher.ChunkedDownloader
{
	public class HttpDataStore : IReadOnlyDataStore
	{
		public string Url { get; private set; }
		protected HttpDataStore()
		{
		}

		public HttpDataStore (string url) : this()
		{
			Url = url;
		}

		protected async Task<IChunk> DownloadChunk(IChunkData chunkData)
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get, DownloadUrl))
			using (var wc = new HttpClient())
			{
				message.Headers.Range = new RangeHeaderValue(chunkData.Start, chunkData.Start + chunkData.Length);
				using (var response = await wc.SendAsync(message))
				{
					var content = await response.Content.ReadAsByteArrayAsync();
					var chunk = new Chunk()
					{
						Start = chunkData.Start,
						Length = (uint)content.Length,
						Data = content
					};
					return chunk;
				}
			}
		}

		public Task<bool> IsChunkable()
		{
			// do this better
			return true;
		}

		public async Task<byte[]> GetData()
		{
			using (var message = new HttpRequestMessage(HttpMethod.Get, DownloadUrl))
			using (var wc = new HttpClient())
			{
				using (var response = await wc.SendAsync(message))
				{
					var content = await response.Content.ReadAsByteArrayAsync();
					var chunk = new Chunk()
					{
						Start = chunkData.Start,
						Length = (uint)content.Length,
						Data = content
					};
					return chunk;
				}
			}
		}
	}
}

