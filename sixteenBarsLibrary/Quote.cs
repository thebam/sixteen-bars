using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sixteenBars.Library
{
    public class Quote
    {
        [Key]
        public Int32 QuoteId { get; set; }
        [Required]
        [Display(Name = "Quote")]
        public String Text { get; set; }
        public String FormattedText { get; set; }
        public String Explanation { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Explicit { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        [Required]
        public int ArtistId { get; set; }
        [Required]
        [Display(Name="Said By")]
        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }
        [Required]
        public int TrackId { get; set; }
        [Display(Name="From Song")]
        [ForeignKey("TrackId")]
        public virtual Track Track { get; set; }
        public string Timestamp { get; set; }
        public Quote()
        {
            Enabled = false;
            Explicit = false;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }

        public override string ToString()
        {
            return "Id: " + this.QuoteId.ToString() + ",Text: " + this.Text + ",Explanation: "+ this.Explanation+",{ Artist: " + this.Artist.ToString() + "},{Track: "+this.Track.ToString()+"},Explicit: " + this.Explicit.ToString() + ", DateCreated: " + this.DateCreated.ToString() + ",DateModified: " + this.DateModified.ToString() + ",Enabled: " + this.Enabled + ",Timestamp: " + this.Timestamp;
        }
    }

    
}