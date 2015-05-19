using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace sixteenBars.Models
{
    public class QuoteViewModel
    {
        public Int32 Id { get; set; }
        [Required(ErrorMessage="Quote is required.")]
        [Display(Name = "Quote")]
        public String Text { get; set; }
        public String Explanation { get; set; }
        public Boolean Explicit { get; set; }
        [Required(ErrorMessage = "Select or add the artist that said this quote.")]
        public Int32 ArtistId { get; set; }
        public String ArtistName { get; set; }
        [Display(Name="Said By")]
        public SelectList Artists { get; set; }
        [Required(ErrorMessage = "Select or add the song this quote was said in.")]
        public Int32 TrackId { get; set; }
        public String TrackName { get; set; }
        [DataType(DataType.Date)]
        
        [Display(Name="Track Release Date")]
        public DateTime TrackReleaseDate { get; set; }
        [Display(Name="From Song")]
        public SelectList Tracks { get; set; }

        [Required(ErrorMessage = "Select or add the album this track is a part of.")]
        public Int32 AlbumId { get; set; }
        public String AlbumName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Album Release Date")]
        public DateTime AlbumReleaseDate { get; set; }
        [Display(Name = "From Album")]
        public SelectList Albums { get; set; }

        [Required(ErrorMessage = "Select or add the artist that made this album.")]
        public Int32 AlbumArtistId { get; set; }
        public String AlbumArtistName { get; set; }
        [Display(Name = "Album By")]
        public SelectList AlbumArtists { get; set; }
        
    }
}