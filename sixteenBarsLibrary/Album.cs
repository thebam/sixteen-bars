using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sixteenBars.Library
{
    public class Album
    {
        [Key]
        public Int32 AlbumId { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }
        [Display(Name="Release Date")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yyyy}",ApplyFormatInEditMode=true)]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        public virtual List<Track> Tracks { get; set; }
        public Album()
        { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Enabled = false;
        }

        public override string ToString()
        {
            return "Id: "+this.AlbumId.ToString()+",Title: "+ this.Title+",{ Artist: "+this.Artist.ToString()+"},ReleaseDate: "+this.ReleaseDate.ToString()+", DateCreated: "+this.DateCreated.ToString()+",DateModified: "+this.DateModified.ToString()+",Enabled: " + this.Enabled;
        }
    }
}
