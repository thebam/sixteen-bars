using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Models
{
    public class AlbumIndexViewModel
    {
        public Int32 Id { get; set; }
        [Display(Name="Album Title")]
        public String Title { get; set; }
        [Display(Name = "Artist Name")]
        public String ArtistName { get; set; }
        public Int32 ArtistId { get; set; }
        public Boolean IsDeleteable { get; set; }
        public AlbumIndexViewModel() {
            IsDeleteable = false;
        }
    }
}