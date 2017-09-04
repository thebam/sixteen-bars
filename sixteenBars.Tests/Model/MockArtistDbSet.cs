using sixteenBars.Library;
using System.Linq;

namespace sixteenBars.Tests.Model
{
    public class MockArtistDbSet : MockDbSet<Artist>
    {
        public override Artist Find(params object[] keyValues)
        {
            return this.SingleOrDefault(a => a.ArtistId == (int)keyValues.Single());
        }
    }

}
