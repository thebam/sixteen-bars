using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sixteenBars.Library
{
    public class Track
    {
        [Key]
        public Int32 TrackId { get; set; }
        [Required]
        public String Title { get; set; }
        [Display(Name="Release Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        [Required]
        public int AlbumId { get; set; }
        [ForeignKey("AlbumId")]
        public virtual Album Album { get; set; }
        public int Order { get; set; }
        public string Video { get; set; }
        [Display(Name ="Video Copyright")]
        public string VideoCopyright { get; set; }
        public Track() { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Enabled = false;
        }

        public override string ToString()
        {
            return "Id: " + this.TrackId.ToString() + ",Title: " + this.Title + ",{ Album: " + this.Album.ToString() + "},Video: " + this.Video + ",VideoCopyright: " + this.VideoCopyright + ",ReleaseDate: " + this.ReleaseDate.ToString() + ", DateCreated: " + this.DateCreated.ToString() + ",DateModified: " + this.DateModified.ToString() + ",Enabled: " + this.Enabled;
        }
    }
}
