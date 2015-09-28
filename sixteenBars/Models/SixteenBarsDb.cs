using System.Data.Entity;
using sixteenBars.Library;
using System.Data;

namespace sixteenBars.Models
{
    public class SixteenBarsDb: DbContext, ISixteenBarsDb
    {
        public SixteenBarsDb(): base("name=DefaultConnection") { }

        public IDbSet<Quote> Quotes { get; set; }
        public IDbSet<Artist> Artists { get; set; }
        public IDbSet<Track> Tracks { get; set; }
        public IDbSet<Album> Albums { get; set; }
        public IDbSet<ChangeLog> ChangeLogs { get; set; }
        public IDbSet<UserProfile> UserProfiles { get; set; }
        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

    }
}