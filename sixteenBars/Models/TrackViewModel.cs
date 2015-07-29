using sixteenBars.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sixteenBars.Models
{
    public class TrackViewModel
    {
        public Int32 Id { get; set; }
        [Required]
        public String Title { get; set; }
        [Display(Name="Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name="Artist")]
        public String ArtistName { get; set; }
        [Display(Name="Album Title")]
        public String AlbumTitle { get; set; }
        public virtual Album Album { get; set; }

        
    }
}