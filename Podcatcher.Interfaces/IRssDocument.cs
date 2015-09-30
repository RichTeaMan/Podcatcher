using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Interfaces
{
    public interface IRssDocument
    {
        string Name { get; }
        string Description { get; }
        string Link { get; }
        string PictureUrl { get; }
        //string PictureTitle { get; }
        //string PictureLink { get; }

        IEnumerable<IRssItem> Items { get; }
    }
}
