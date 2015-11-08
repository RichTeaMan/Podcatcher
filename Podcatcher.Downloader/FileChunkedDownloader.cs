using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Podcatcher.Domain;
using System.IO;

namespace Podcatcher.Downloader
{
    public class FileChunkedDownloader : IChunkedDownloader
    {
        private Stream _fileStream;
        public Stream FileStream
        {
            get { return _fileStream; }
            set
            {
                if (value.CanSeek)
                {
                    _fileStream = value;
                }
                else
                {
                    throw new InvalidOperationException("FileStream must be seekable.");
                }
            }
        }

        protected async Task<byte[]> ReadBytes(Stream stream, int length)
        {
            int current = 0;
            int remaining = (int)stream.Length - (int)stream.Position;
            int _length = Math.Min(remaining, length);
            var data = new byte[_length];
            while (current < _length)
            {
                int bytesRead = await stream.ReadAsync(data, current, _length - current);
                current += bytesRead;
            }
            return data;
        }

        public async Task<IChunk> DownloadChunk(IChunkInfo chunkData)
        {
            FileStream.Position = chunkData.Start;
            var data = await ReadBytes(FileStream, chunkData.Length);

            var chunk = new Chunk(
                chunkData.Start,
                data);
            return chunk;
        }
    }
}
