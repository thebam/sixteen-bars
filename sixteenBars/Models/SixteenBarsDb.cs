using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using sixteenBars.Library;

namespace sixteenBars.Models
{
    public class SixteenBarsDb: DbContext
    {
        public SixteenBarsDb(): base("name=DefaultConnection") { }

        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}