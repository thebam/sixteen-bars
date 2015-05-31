using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sixteenBars.Models
{
    public class ArtistIndexViewModel
    {
        public Int32 Id { get; set; }
        [Display(Name="Artist Name")]
        public String Name { get; set; }
        public Boolean IsDeleteable { get; set; }

        public ArtistIndexViewModel() {
            IsDeleteable = false;
        }
    }
}