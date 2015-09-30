using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Podcatcher.Load;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tests.Podcatcher.Load
{
    [TestClass]
    public class TestRssDocumentFactory
    {
        [TestMethod]
        public void TestGetRssDocument()
        {
            var factory = new RssDocumentFactory();
            var documentTask = factory.GetRssDocument("http://feeds.feedburner.com/BlackListTableReads?format=xml");
            Task.WaitAll(documentTask);
            var document = documentTask.Result;

            Debug.WriteLine(document.Name);
            Debug.WriteLine(document.Description);
            Debug.WriteLine(document.Link);
            Debug.WriteLine(document.PictureUrl);
            foreach(var i in document.Items)
            {
                Debug.WriteLine(i.Title);
                Debug.WriteLine(i.Description);
                Debug.WriteLine(i.Published);
            }
            Debug.WriteLine("End of rss");
        }

        [TestMethod]
        public void TestGetRssDocumentWithFile()
        {
            var factory = new RssDocumentFactory();
            using (var fs = new System.IO.FileStream(@"C:\Users\thoma\Desktop\BlackListTableReads.xml", System.IO.FileMode.Open))
            {
                var document = factory.GetRssDocument(fs);
            }
        }
    }
}
