using System;

namespace Podcatcher.ChunkedDownloader
{
	public static class DownloaderFactory
	{

		public static Downloader GetDownloader(string url, string filename) {
			var httpStore = new HttpDataStore(url);
			var fileStore = new FileDataStore(filename);

			var downloader = new Downloader (fileStore, httpStore);
			return downloader;
		}
	}
}

