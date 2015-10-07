using System;

namespace Tests.Podcatcher.FileDownloader
{
	class Program
	{
		public static void Main(string[] args)
		{
			var downloader = global::Podcatcher.ChunkedDownloader.DownloaderFactory.GetDownloader (
				                 "https://soundcloud.com/denzelisthegreatest/4-antwone-fisher/download",
				                 "Antone-Fisher.mp3");
			int chunks = 0;

			try {
				while (!downloader.TransferComplete) {
					Console.WriteLine ("Getting chunk...");
					var dChunk = downloader.DownloadChunk ();
					dChunk.Wait();
					chunks++;
					Console.WriteLine("Downloaded {0} chunks.", chunks);
				}
				Console.WriteLine ("Complete!", chunks);
			} catch (AggregateException ex) {
				Console.WriteLine ("{0} errors occurred:", ex.InnerExceptions.Count);
				int count = 1;
				foreach (var iex in ex.InnerExceptions) {
					Console.WriteLine("Error {0}:", count);
					Console.WriteLine(iex);
					count++;
				}
			}
			catch (Exception ex) {
				Console.WriteLine ("An error occurred:");
				Console.WriteLine(ex);				
			}
		}
	}
}
