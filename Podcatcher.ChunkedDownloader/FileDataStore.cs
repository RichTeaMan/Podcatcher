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
		protected static byte[] UnusedFlag = { 1, 2, 4, 8, 16, 32 };
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
				await FS.LocalStorage.CreateFileAsync(FilePath, CreationCollisionOption.OpenIfExists);
				using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite)) {
					await WriteUnusedFlag(stream);
				}
			}
		}

        /// <summary>
        /// Gets data for the given chunk. Returns null if no data exists.
        /// </summary>
        /// <param name="chunkData"></param>
        /// <returns></returns>
		public async Task<IChunk> GetChunk(IChunkData chunkData)
		{
            var file = await GetFile();
            using (var stream = await file.OpenAsync(FileAccess.Read))
            {
                if(chunkData.Start > stream.Length)
                {
                    return null;
                }
                else
                {
                    byte[] buffer = new byte[chunkData.Length];
                    stream.Position = chunkData.Start;

                    int read = await stream.ReadAsync(buffer, 0, (int)chunkData.Length);
                    if(read != chunkData.Length)
                    {
                        buffer = buffer.Take(read).ToArray();
                    }
                    var chunk = new Chunk() { Start = chunkData.Start, Length = (uint)read, Data = buffer };
                    return chunk;
                }
            }
		}

		public IEnumerable<IChunkData> GetNextEmptyChunk()
		{
			var chunks = GetNextEmptyChunk(0);
			foreach (var chunk in chunks) {
				yield return chunk;
			}
		}

		public IEnumerable<IChunkData> GetNextEmptyChunk(uint startPosition)
		{
			var indexs = GetNextUnusedIndex((int)startPosition);
			if (indexs.Count() == 0) {
				yield break;
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
				Length = uint.MaxValue
			};
			yield return finalChunk;
		}

		protected IEnumerable<uint> GetNextUnusedIndex(int startPosition)
		{
			var file = GetFile().Result;
			using (var stream = file.OpenAsync(FileAccess.Read).Result) {
				// load in a buffer from a file and find instances of Unused.
				// loading a large buffer rather than checking each byte from
				// a file has better performance.

				// on buffer boundaries....
				// an issue here is that an UnusedFlag may straddle buffer
				// boundaries which would be missed. The idea here is too
				// read into buffer with an offset then place the end of
				// the buffer into the beginning of the next buffer so that
				// flag will be read in the next iteration.

				byte[] buffer = new byte[BufferLength + (UnusedFlag.Length - 1)];
				while (stream.Position < stream.Length) {
					uint relativeStreamPosition = ((uint)stream.Position) - (uint)(UnusedFlag.Length - 1);

					stream.Read(buffer, UnusedFlag.Length - 1, BufferLength);

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

		protected IEnumerable<uint> FindArrayInArray<T>(T[] sourceArray, T[] match, int stopIndex = int.MaxValue)
		{
			if (sourceArray == null || match == null) {
				throw new NullReferenceException ();
			}

			if (match.Length > sourceArray.Length) {
				yield break;
			}

			int currentMatchIndex = 0;
			uint index = 0;
			foreach(var s in sourceArray)
			{
				if (s.Equals(match[currentMatchIndex])) {
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

		public async Task StoreChunk(uint start, byte[] data)
		{
			var file = await GetFile ();
			using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite)) {
				if (stream.CanSeek && stream.CanWrite) {
					
					stream.Position = start;
                    // only write unused flag at end of stream if there is something tailing and data does not immediately follow.
                    var nextEmpty = GetNextEmptyChunk(start).FirstOrDefault();
                    bool writeUnused = nextEmpty != null && nextEmpty.End() != start + data.Length;
					await stream.WriteAsync(data, 0, data.Length);
                    if (writeUnused)
                    {
                        await WriteUnusedFlag(stream);
                    }

				} else {
					throw new Exception ("File cannot be written too, it does not support seeking and/or writing.");
				}
			}

			// consider checking the result here
		}

		public async Task<bool> IsChunkable()
		{
            // shuts up warning.
            var result = await Task.Factory.StartNew(() => { return true; });
            return result;
		}

		public async Task<byte[]> GetData()
		{
            var file = await GetFile();
            using (var stream = await file.OpenAsync(FileAccess.Read))
            {
                var buffer = new byte[stream.Length];
                int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                if(read != buffer.Length)
                {
                    throw new Exception("Incorrect data length.");
                }
                return buffer;
            }
		}

		protected async Task WriteUnusedFlag(Stream stream)
		{
			await stream.WriteAsync (UnusedFlag, 0, UnusedFlag.Length);
		}

	}
}

