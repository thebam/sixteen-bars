using sixteenBars.Library;
using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Models
{
    public class TrackIndexViewModel
    {
        public Int32 Id { get; set; }
        public String Title { get; set; }
        public Int32 AlbumId { get; set; }
        [Display(Name="From Album")]
        public String AlbumTitle { get; set; }
        public Int32 ArtistId { get; set; }
        [Display(Name="Artist")]
        public String ArtistName { get; set; }
        public Boolean IsDeleteable { get; set; }
        public Boolean Enabled { get; set; }
        public Album Album { get; set; }
        public TrackIndexViewModel()
        {
            IsDeleteable = false;
        }

    }
}