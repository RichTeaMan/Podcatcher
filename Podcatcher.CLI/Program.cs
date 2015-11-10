using CommandLineParser;
using Podcatcher.Manager;
using Podcatcher.RssReader;
using Podcatcher.Search.Itunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            MethodInvoker command = null;
            try
            {
                command = ClCommandAttribute.GetCommand(typeof(Program), args);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing command:");
                Console.WriteLine(ex.Message);
            }
            if (command != null)
            {
                try
                {
                    command.Invoke();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error running command:");
                    Console.WriteLine(ex.Message);

                    var inner = ex.InnerException;
                    while (inner != null)
                    {
                        Console.WriteLine(inner);
                        Console.WriteLine();
                        inner = inner.InnerException;
                    }

                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        [ClCommand("rssInfo")]
        public static void RssInfo(
            [ClArgs("url")]
            string url
            )
        {
            var factory = new RssFactory();
            var rssTask = factory.CreateFromUrl(url);
            var rss = rssTask.Result;
            Console.WriteLine("Title: {0}", rss.channel.Title);
            Console.WriteLine("Author: {0}", rss.channel.Author);

            foreach(var item in rss.channel.Items)
            {
                Console.WriteLine(item.title);
                Console.WriteLine(item.enclosure.url);
            }
        }

        [ClCommand("download")]
        public static void Download(
            [ClArgs("url")]
            string url
            )
        {
            var factory = new RssFactory();
            var rssTask = factory.CreateFromUrl(url);
            var rss = rssTask.Result;
            Console.WriteLine("Title: {0}", rss.channel.Title);
            Console.WriteLine("Author: {0}", rss.channel.Author);

            var cast = rss.channel.Items.FirstOrDefault();
            if(cast == null)
            {
                Console.WriteLine("No file to download.");
                return;
            }

            Console.WriteLine("Downloading {0} bytes from {1}.", cast.enclosure.length, cast.enclosure.url);

            var downloader = new FileDownload(cast.enclosure.url, cast.title) { ContentLength = (int)cast.enclosure.length };
            downloader.ChunkSaved += Downloader_ChunkSaved;
            while(!downloader.Complete)
            {
                downloader.DownloadAndSaveChunk().Wait();
            }
            Console.WriteLine("Download complete.");
            downloader.GetCompleteFileStream().Wait();
            Console.WriteLine("File written.");
        }

        private static void Downloader_ChunkSaved(FileDownload sender, Podcatcher.Domain.IChunk chunk)
        {
            int remaining = sender.ContentLength - sender.GetBytesSavedCount().Result;
            Console.WriteLine("Downloaded {0} bytes. {1} remaining.", chunk.Length, remaining);
        }

        [ClCommand("search")]
        public static void Search(
            [ClArgs("term", "t")]
            string term)
        {
            var searchFactory = new ItunesSearchFactory();
            var result = searchFactory.Search(term).Result;

            Console.WriteLine("{0} results found:", result.resultCount);
            foreach(var r in result.results)
            {
                Console.WriteLine("Name: {0} Artist: {1} Casts: {2}",
                    r.collectionName,
                    r.collectionArtistName,
                    r.trackCount);
            }
        }
    }
}
