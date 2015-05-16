using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sixteenBars.Library
{
    public class Quote
    {
        public Int32 Id { get; set; }
        [Required]
        [Display(Name = "Quote")]
        public String Text { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        [Display(Name="Said By")]
        public virtual Artist Artist { get; set; }
        [Display(Name="From Song")]
        public virtual Track Track { get; set; }

        public Quote()
        {
            Enabled = true;
            DateCreated = DateTime.Now;
        }
    }

    
}