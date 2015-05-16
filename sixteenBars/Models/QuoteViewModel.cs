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
        [Required(ErrorMessage = "Select or add the artist that said this quote.")]
        public Int32 ArtistId { get; set; }
        public String ArtistName { get; set; }
        [Display(Name="Said By")]
        public SelectList Artists { get; set; }
        [Required(ErrorMessage = "Select or add the song this quote was said in.")]
        public Int32 TrackId { get; set; }
        public String TrackName { get; set; }
        [Display(Name="From Song")]
        public SelectList Tracks { get; set; }

        
    }
}