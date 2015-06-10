using sixteenBars.Library;
using System.Linq;

namespace sixteenBars.Tests.Model
{
    public class MockTrackDbSet : MockDbSet<Track>
    {
        public override Track Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.Id == (int)keyValues.Single());
        }
    }

}
