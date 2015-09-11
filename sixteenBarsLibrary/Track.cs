using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Track
    {
        public Int32 Id { get; set; }
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
        public virtual Album Album { get; set; }

        public Track() { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Enabled = true;
        }
    }
}
