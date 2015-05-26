using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Album
    {
        public Int32 Id { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public virtual Artist Artist { get; set; }
        [Display(Name="Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Enabled { get; set; }

        public Album()
        { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Enabled = true;
        }
    }
}
