using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Track
    {
        public Int32 Id { get; set; }
        [Required]
        public String Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Boolean Enabled { get; set; }

        public Track() { 
            DateCreated = DateTime.Now;
            Enabled = true;
        }
    }
}
