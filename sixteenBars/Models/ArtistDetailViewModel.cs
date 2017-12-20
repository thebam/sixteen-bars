using sixteenBars.Library;
using System;
using System.Collections.Generic;

namespace sixteenBars.Models
{
    public class ArtistDetailViewModel 
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Image { get; set; }
        public List<Album> Albums { get; set; }
        public List<Quote> Quotes { get; set; }
        
        public string ImageCopyright { get; set; }
        
        public String RealName { get; set; }
        public string Location { get; set; }
        public string Biography { get; set; }
                public DateTime BirthDate { get; set; }
    }
}
