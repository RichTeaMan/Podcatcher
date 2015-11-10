using PCLStorage;
using Podcatcher.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.FileSaver
{
    public class ChunkSaver
    {

        public ChunkSaver()
        {
        }

        public string GetDirectoryName(string filepath)
        {
            return filepath + ".parts";
        }

        public async Task<IFolder> CreateFolder(string filepath)
        {
            IFolder folder;
            var exists = await FileSystem.Current.LocalStorage.CheckExistsAsync(GetDirectoryName(filepath));
            switch (exists)
            {
                case ExistenceCheckResult.FolderExists:
                case ExistenceCheckResult.NotFound:
                    folder = await FileSystem.Current.LocalStorage.CreateFolderAsync(GetDirectoryName(filepath), CreationCollisionOption.OpenIfExists);
                    break;
                default:
                    throw new InvalidOperationException("Cannot create directory, file already exists.");
            }
            return folder;
        }

        public async Task SaveFile(string filepath, int start, byte[] data)
        {
            await SaveFile(filepath, start, data, data.Length);
        }

        public async Task SaveFile(string filepath, int start, byte[] data, int length)
        {
            var folder = await CreateFolder(filepath);
            var file = await folder.CreateFileAsync(start.ToString(), CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite))
            {
                stream.Write(data, 0, length);
            }
        }

        public async Task<Stream> GetCombinedStream(string filepath)
        {
            var comFile = await FileSystem.Current.LocalStorage.GetFileAsync(filepath);
            var comStream = await comFile.OpenAsync(FileAccess.Read);
            return comStream;
        }

        public async Task<Stream> CreateCombinedStream(string filepath)
        {
            var comFile = await FileSystem.Current.LocalStorage.CreateFileAsync(filepath, CreationCollisionOption.ReplaceExisting);
            using (var comStream = await comFile.OpenAsync(FileAccess.ReadAndWrite))
            {
                var fileNameMap = await GetChunkMap(filepath);

                int position = 0;
                foreach (var chunk in fileNameMap.OrderBy(f => f.Key))
                {
                    if (chunk.Key == position)
                    {
                        using (var file = await chunk.Value.OpenAsync(PCLStorage.FileAccess.Read))
                        {
                            position += (int)file.Length;
                            var data = await ReadBytes(file);
                            comStream.Write(data, 0, data.Length);
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(String.Format("Cannot combine chunks. Chunk at position {0} is missing.", position));
                    }
                }
            }
            return await GetCombinedStream(filepath);
        }

        protected async Task<Dictionary<int, IFile>> GetChunkMap(string filepath)
        {
            var folder = await CreateFolder(filepath);
            var files = await folder.GetFilesAsync();
            var fileNameMap = new Dictionary<int, IFile>();
            foreach (var file in files)
            {
                int chunkPosition;
                if (int.TryParse(file.Name, out chunkPosition))
                {
                    fileNameMap.Add(chunkPosition, file);
                }
            }

            return fileNameMap;
        }

        protected async Task<byte[]> ReadBytes(Stream stream)
        {
            int current = 0;
            int length = (int)stream.Length;
            var data = new byte[length];
            while (current < length)
            {
                int bytesRead = await stream.ReadAsync(data, current, length - current);
                current += bytesRead;
            }
            return data;
        }

        public async Task<IChunkInfo> GetNextEmptyChunk(string filepath, int startPosition = 0)
        {
            ChunkInfo previousChunk = null;

            int previousStart = startPosition;
            var chunks = await GetChunkMap(filepath);

            foreach (var chunk in chunks.OrderBy(c => c.Key))
            {
                using (var file = await chunk.Value.OpenAsync(FileAccess.Read))
                {
                    int chunkStart = chunk.Key;
                    int chunkEnd = chunkStart + (int)file.Length;

                    if (previousChunk != null)
                    {
                        if ((previousChunk.Start + previousChunk.Length) < chunkStart)
                        {
                            if (startPosition < chunkStart)
                            {
                                int gapStart = Math.Max(startPosition, previousChunk.Start + previousChunk.Length);
                                var gapChunk = new ChunkInfo(gapStart, chunkStart - gapStart);
                                return gapChunk;
                            }
                        }
                    }
                    previousChunk = new ChunkInfo(chunkStart, (int)file.Length);
                }
            }
            ChunkInfo fullChunk;
            if (previousChunk == null)
            {
                fullChunk = new ChunkInfo(0, int.MaxValue);
            }
            else
            {
                fullChunk = new ChunkInfo(previousChunk.Start + previousChunk.Length, int.MaxValue);
            }
            return fullChunk;
        }

        public IEnumerable<IChunkInfo> GetEmptyChunks(string filepath)
        {
            int position = 0;
            IChunkInfo chunkInfo;
            // TODO: Could this ever actually work?
            while ((chunkInfo = GetNextEmptyChunk(filepath, position).Result) != null)
            {
                yield return chunkInfo;
            }
        }

        /// <summary>
        /// Gets how many bytes have been saved.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public async Task<int> GetBytesSavedCount(string filepath)
        {
            var chunkMap = await GetChunkMap(filepath);
            long length = 0;
            foreach(var chunk in chunkMap.Values)
            {
                using (var fs = await chunk.OpenAsync(FileAccess.Read))
                {
                    length += fs.Length;
                }
            }
            return (int)length;
        }

    }
}
