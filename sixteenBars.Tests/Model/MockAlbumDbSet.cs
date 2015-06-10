using sixteenBars.Library;
using System.Linq;

namespace sixteenBars.Tests.Model
{
    public class MockAlbumDbSet : MockDbSet<Album>
    {
        public override Album Find(params object[] keyValues)
        {
            return this.SingleOrDefault(a => a.Id == (int)keyValues.Single());
        }
    }

}
