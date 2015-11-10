using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Search.Itunes
{

    public class SearchObject
    {
        public int resultCount { get; set; }
        public Result[] results { get; set; }
    }

}
