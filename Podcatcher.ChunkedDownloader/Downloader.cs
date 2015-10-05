﻿using Podcatcher.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace Podcatcher.ChunkedDownloader
{
    public class Downloader
    {
        public IChunkStore DestinationStore { get; private set; }

		public IReadonlyDataStore SourceDataStore{ get; private set; }

        public ushort DefaultLength { get; set; } = 500;

		public bool TransferComplete { get; private set; } = false;

        protected Downloader() { }

		public Downloader(IChunkStore destinationDataStore, IReadonlyDataStore sourceDataStore)
        {
            DestinationStore = destinationDataStore;
			SourceDataStore = sourceDataStore;
        }

		/// <summary>
		/// Gets the next empty chunk. This chunk should be retrieved from the source
		/// data. If null is returned there are no more empty chunks and the transfer should
		/// be considered complete.
		/// 
		/// This method should be checked for exceptions.
		/// </summary>
		/// <returns>The next empty chunk.</returns>
        protected async Task<IChunkData> GetNextEmptyChunk()
		{
			var emptyChunk = await DestinationStore.GetNextEmptyChunk ();
			uint length = Math.Min (emptyChunk.Length, DefaultLength);

			var chunkData = new ChunkData () { 
				Start = emptyChunk.Start,
				Length = length
			};
			return chunkData;
		}

        protected async Task StoreChunk(IChunk chunk)
        {
            await DestinationStore.StoreChunk(chunk.Start, chunk.Data.ToArray());
        }

        public async Task DownloadChunk()
        {
			try
			{
	            var chunkData = await GetNextEmptyChunk();
				if (chunkData == null) {
					// consider more robust way of checking this.
					TransferComplete = true;
				}
				else {
					var chunk = await SourceDataStore.GetChunk (chunkData);
				}
			}
			catch(Exception ex) {
				// do nothing for now. consider logging and/or aborting after x attempts.
				return;
			}
        }

    }
}
