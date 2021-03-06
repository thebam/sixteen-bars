﻿using sixteenBars.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sixteenBars.Models
{
    public class ArtistDetailViewModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public List<Album> Albums { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
