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
        [DataType(DataType.MultilineText)]
        public String Text { get; set; }
        [DataType(DataType.MultilineText)]
        public String Explanation { get; set; }
        public Boolean Explicit { get; set; }
        [Required(ErrorMessage = "Select or add the artist that said this quote.")]
        public Int32 ArtistId { get; set; }
        [Display(Name = "Artist Name")]
        public String ArtistName { get; set; }
        [Display(Name="Said By")]
        public SelectList Artists { get; set; }
        [Required(ErrorMessage = "Select or add the song this quote was said in.")]
        public Int32 TrackId { get; set; }
        [Display(Name = "Song Name")]
        public String TrackName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name="Song Release Date")]
        public DateTime TrackReleaseDate { get; set; }
        [Display(Name="From Song")]
        public SelectList Tracks { get; set; }

        [Required(ErrorMessage = "Select or add the album this track is a part of.")]
        public Int32 AlbumId { get; set; }
        [Display(Name = "Album Name")]
        public String AlbumName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Album Release Date")]
        public DateTime AlbumReleaseDate { get; set; }
        [Display(Name = "From Album")]
        public SelectList Albums { get; set; }

        [Required(ErrorMessage = "Select or add the artist that made this album.")]
        public Int32 AlbumArtistId { get; set; }
        [Display(Name = "Artist Name")]
        public String AlbumArtistName { get; set; }
        [Display(Name = "Album By")]
        public SelectList AlbumArtists { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Timestamp { get; set; }
    }
}