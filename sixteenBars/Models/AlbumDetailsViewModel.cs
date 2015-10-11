using sixteenBars.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sixteenBars.Models
{
    public class AlbumDetailsViewModel
    {
        public Int32 Id { get; set; }
        public String Title { get; set; }
        public Int32 ArtistId { get; set; }
        public String ArtistName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Track> Tracks { get; set; }
    }
}