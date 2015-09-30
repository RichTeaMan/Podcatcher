using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Podcatcher.Interfaces
{
    public interface IRssItem
    {
        //      <item>
		//	<guid isPermaLink = "false" > tag:soundcloud,2010:tracks/223191770</guid>
		//	<title>Bonus: John August and Craig Mazin</title>
		//	<pubDate>Thu, 10 Sep 2015 07:20:42 +0000</pubDate>
		//	<link>https://soundcloud.com/black-list-table-reads/bonus-john-august-and-craig-mazin</link>
		//	<itunes:duration>01:03:25</itunes:duration>
		//	<itunes:author>Black List Table Reads</itunes:author>
		//	<itunes:explicit>no</itunes:explicit>
		//	<itunes:summary>Screenwriters John August (Big Fish, Dark Shadows) and Craig Mazin(Hangover 3, Identity Thief) of the Scriptnotes podcast, a podcast about screenwriting and things that are interesting to screenwriters join Franklin Leonard for a in depth conversation on this week’s Black List Table Reads.They’ll talk about what lead them to start their podcast which has now hit over 200 episodes, the truth about paper teams, and when they realized when they were good as writers.</itunes:summary>
		//	<itunes:subtitle>Screenwriters John August (Big Fish, Dark Shadows…</itunes:subtitle>
		//	<description>Screenwriters John August (Big Fish, Dark Shadows) and Craig Mazin (Hangover 3, Identity Thief) of the Scriptnotes podcast, a podcast about screenwriting and things that are interesting to screenwriters join Franklin Leonard for a in depth conversation on this week’s Black List Table Reads. They’ll talk about what lead them to start their podcast which has now hit over 200 episodes, the truth about paper teams, and when they realized when they were good as writers.</description>
		//	<enclosure type="audio/mpeg" url="http://feeds.soundcloud.com/stream/223191770-black-list-table-reads-bonus-john-august-and-craig-mazin.mp3" length="53172801" />
		//	<itunes:image href="http://i1.sndcdn.com/avatars-000138976562-xg6gg9-original.jpg" />
		//	<dc:creator xmlns:dc="http://purl.org/dc/elements/1.1/">Wolfpop</dc:creator>
		//	<media:content url="http://feeds.soundcloud.com/stream/223191770-black-list-table-reads-bonus-john-august-and-craig-mazin.mp3" fileSize="53172801" type="audio/mpeg" />
		//</item>

        string Title { get; }
        DateTime? Published { get; }
        string Link { get; }
        string Description { get; }
        string ImageUrl { get; }
        string ContentUrl { get; }
        uint ContentFileSize { get; }
    }
}
