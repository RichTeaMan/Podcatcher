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
                var folder = await CreateFolder(filepath);
                int position = 0;
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

        public Task<IChunkInfo> GetNextEmptyChunk(string filepath, int startPosition = 0)
        {
            throw new NotImplementedException();
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

    }
}
