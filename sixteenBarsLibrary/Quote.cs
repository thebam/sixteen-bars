using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Quote
    {
        public Int32 Id { get; set; }
        [Required]
        [Display(Name = "Quote")]
        public String Text { get; set; }
        public String Explanation { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Explicit { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        [Display(Name="Said By")]
        public virtual Artist Artist { get; set; }
        [Display(Name="From Song")]
        public virtual Track Track { get; set; }

        public Quote()
        {
            Enabled = true;
            Explicit = false;
            DateCreated = DateTime.Now;
        }
    }

    
}