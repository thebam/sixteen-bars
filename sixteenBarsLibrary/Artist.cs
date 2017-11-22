using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Artist
    {
        [Key]
        public Int32 ArtistId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        public string Image { get; set; }
        public Artist()
        { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Enabled = false;
        }

        public override string ToString()
        {
            return "Id: " + this.ArtistId.ToString() + ",Name: " + this.Name + ", DateCreated: " + this.DateCreated.ToString() + ",DateModified: " + this.DateModified.ToString() + ",Enabled: " + this.Enabled;
        }
    }
}
