using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Podcatcher.Interfaces;

namespace Podcatcher.ChunkedDownloader
{
	public class FileDataStore : IChunkStore
	{
		public string FilePath { get; private set; }
		public uint FileSize { get; private set; }

		protected FileDataStore()
		{
		}

		public FileDataStore (string filePath, uint fileSize) : this()
		{
			FilePath = filePath;
			FileSize = fileSize;
		}

		protected async Task InitialiseFile()
		{
			
		}

		public async Task<IChunkData> GetNextEmptyChunk()
		{
			throw new NotImplementedException ();
		}

		public Task<bool> IsChunkable()
		{
			return true;
		}

		public async Task<byte[]> GetData()
		{
			throw new NotImplementedException ();
		}

	}
}

