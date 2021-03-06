﻿using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Artist
    {
        public Int32 Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Enabled { get; set; }

        public Artist()
        { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            Enabled = true;
        }
    }
}
