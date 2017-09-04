using sixteenBars.Library;
using sixteenBars.Models;

namespace sixteenBars.Controllers
{
    public class LogUtility
    {
        public static void Log(ChangeLog log){
            ISixteenBarsDb _db = new SixteenBarsDb();
            _db.ChangeLogs.Add(log);
            _db.SaveChanges();
        }
    }
}
