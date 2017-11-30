using System;

namespace sixteenBars.Models
{
    public class QuoteAPIViewModel
    {
        public Int32 QuoteId { get; set; }
        public String Text { get; set; }
        public String Explanation { get; set; }
        public Boolean Explicit { get; set; }
        public Int32 ArtistId { get; set; }
        public String ArtistName { get; set; }
        public Int32 TrackId { get; set; }
        public String TrackName { get; set; }
        public Int32 AlbumId { get; set; }
        public String AlbumName { get; set; }
        public Int32 AlbumArtistId { get; set; }
        public String AlbumArtistName { get; set; }
        public string ArtistImage { get; set; }
        public string AlbumImage { get; set; }
        public string Video { get; set; }
    }
}