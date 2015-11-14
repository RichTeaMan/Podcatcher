﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Podcatcher.Search.Itunes
{

    public class Result
    {
        public string wrapperType { get; set; }
        public string kind { get; set; }
        public int artistId { get; set; }
        public int collectionId { get; set; }
        public int trackId { get; set; }
        public string artistName { get; set; }
        public string collectionName { get; set; }
        public string trackName { get; set; }
        public string collectionCensoredName { get; set; }
        public string trackCensoredName { get; set; }
        public string artistViewUrl { get; set; }
        public string collectionViewUrl { get; set; }
        public string feedUrl { get; set; }
        public string trackViewUrl { get; set; }
        public string artworkUrl30 { get; set; }
        public string artworkUrl60 { get; set; }
        public string artworkUrl100 { get; set; }
        public float collectionPrice { get; set; }
        public float trackPrice { get; set; }
        public float trackRentalPrice { get; set; }
        public float collectionHdPrice { get; set; }
        public float trackHdPrice { get; set; }
        public float trackHdRentalPrice { get; set; }
        public DateTime releaseDate { get; set; }
        public string collectionExplicitness { get; set; }
        public string trackExplicitness { get; set; }
        public int trackCount { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string primaryGenreName { get; set; }
        public string contentAdvisoryRating { get; set; }
        public string radioStationUrl { get; set; }
        public string artworkUrl600 { get; set; }
        public string[] genreIds { get; set; }
        public string[] genres { get; set; }
        public int collectionArtistId { get; set; }
        public string collectionArtistName { get; set; }
        public string previewUrl { get; set; }
        public int discCount { get; set; }
        public int discNumber { get; set; }
        public int trackNumber { get; set; }
        public int trackTimeMillis { get; set; }
        public bool isStreamable { get; set; }
    }
}
