using sixteenBars.Library;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sixteenBars.Models
{
    public class AlbumViewModel
    {
        public Int32 Id { get; set; }
        [Required]
        public String Title { get; set; }
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public Int32 ArtistId { get; set; }
        public String ArtistName { get; set; }
        [Display(Name = "Artist")]
        public SelectList Artists { get; set; }
    }
}