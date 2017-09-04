using sixteenBars.Library;
using System.Linq;

namespace sixteenBars.Tests.Model
{
    public class MockQuoteDbSet : MockDbSet<Quote>
    {
        public override Quote Find(params object[] keyValues)
        {
            return this.SingleOrDefault(q => q.QuoteId == (int)keyValues.Single());
        }
    }

}
