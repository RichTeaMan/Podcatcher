using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Podcatcher.Interfaces;
using PCLStorage;
using System.IO;

namespace Podcatcher.ChunkedDownloader
{
	public class FileDataStore : IChunkStore
	{
		protected static byte[] UnusedFlag = { 'D', 'E', 'D' };
		protected const int BufferLength = 1024;

		/// <summary>
		/// Gets the file path that data will be written and
		/// read from. DOES NOT SUPPORT DIRECTORIES.
		/// </summary>
		/// <value>The file path.</value>
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

		protected IFileSystem FS {
			get {
				return FileSystem.Current;
			}
		}

		protected async Task<IFile> GetFile()
		{
			var file = await FS.GetFileFromPathAsync(FilePath);
			return file;
		}

		protected async Task InitialiseFile()
		{
			var file = await GetFile();
			if (file == null) {
				await FS.LocalStorage.CreateFileAsync(FilePath);
				using (var stream = await file.OpenAsync()) {
					WriteUnusedFlag(stream);
				}
			}
		}

		public async Task<IChunk> GetChunk(IChunkData chunkData)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<IChunkData>> GetNextEmptyChunk()
		{
			var chunks = await GetNextEmptyChunk(0);
			foreach (var chunk in chunks) {
				yield return chunk;
			}
		}

		public async Task<IEnumerable<IChunkData>> GetNextEmptyChunk(int startPosition)
		{
			var indexs = await GetNextUnusedIndex (startPosition);
			if (indexs.Count() == 0) {
				return;
			}
			uint previousIndex = indexs.First();
			foreach (var index in indexs.Skip(1)) {
				var chunk = new ChunkData () {
					Start = previousIndex,
					Length = index - previousIndex
				};
				yield return chunk;
				previousIndex = index;
			}
			var finalChunk = new ChunkData () {
				Start = previousIndex,
				Length = Math.Max
			};
		}

		protected async Task<IEnumerable<uint>> GetNextUnusedIndex(int startPosition)
		{
			var file = await GetFile();
			using (var stream = await file.OpenAsync()) {
				// load in a buffer from a file and find instances of Unused.
				// loading a large buffer rather than checking each byte from
				// a file has better performance.

				// on buffer boundaries....
				// an issue here is that an UnusedFlag may straddle buffer
				// boundaries which would be missed. The idea here is too
				// read into buffer with an offset then place the end of
				// the buffer into the beginning of the next buffer so that
				// flag will be read in the next iteration.

				byte[] buffer = new byte[BufferLength + (UnusedFlag - 1)];
				while (stream.Position < stream.Length) {
					uint relativeStreamPosition = stream.Position - (UnusedFlag - 1);

					await stream.ReadAsync(buffer, UnusedFlag.Length - 1, BufferLength);

					var indexs = FindArrayInArray(buffer, UnusedFlag);

					foreach (var index in indexs) {
						yield return relativeStreamPosition + index;
					}

					foreach (var i in Enumerable.Range(0, UnusedFlag.Length - 1)) {
						buffer [i] = buffer[buffer.Length - (i + 1)];
					}
				}
			}
		}

		protected IEnumerable<uint> FindArrayInArray<T>(T[] sourceArray, T[] match, int stopIndex = Math.Max)
		{
			if (sourceArray == null || match == null) {
				throw new NullReferenceException ();
			}

			if (match.Length > sourceArray.Length) {
				return;
			}

			int currentMatchIndex = 0;
			uint index = 0;
			foreach(var s in sourceArray)
			{
				if (s == match [currentMatchIndex]) {
					if (currentMatchIndex == match.Length) {
						yield return index;
						currentMatchIndex = 0;
					} else {
						currentMatchIndex++;
					}
				} else {
					currentMatchIndex = 0;
				}
				index++;
				if (index >= stopIndex) {
					break;
				}
			}
		}

		public async Task<IChunkData> StoreChunk(uint start, byte[] data)
		{
			var file = await GetFile ();
			using (var stream = await file.OpenAsync ()) {
				if (stream.CanSeek && stream.CanWrite) {
					
					stream.Position = start;
					await stream.WriteAsync (data, 0, data.Length);

				} else {
					throw new Exception ("File cannot be written too, it does not support seeking and/or writing.");
				}
			}

			// consider checking the result here
		}

		public async Task<bool> IsChunkable()
		{
			return true;
		}

		public async Task<byte[]> GetData()
		{
			throw new NotImplementedException ();
		}

		protected async Task WriteUnusedFlag(Stream stream)
		{
			await stream.WriteAsync (UnusedFlag, 0, UnusedFlag.Length);
		}

	}
}

