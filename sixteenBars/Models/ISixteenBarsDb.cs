using System.Data.Entity;
using sixteenBars.Library;

namespace sixteenBars.Models
{
    public interface ISixteenBarsDb
    {
         IDbSet<Quote> Quotes { get;  }
         IDbSet<Artist> Artists { get;  }
         IDbSet<Track> Tracks { get;  }
         IDbSet<Album> Albums { get;  }
         int SaveChanges();
         void SetModified(object entity);
    }
}