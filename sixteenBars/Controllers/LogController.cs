using sixteenBars.Library;
using sixteenBars.Models;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class LogUtility
    {
        [ValidateInput(false)]
        public static void Log(ChangeLog log){
            ISixteenBarsDb _db = new SixteenBarsDb();
            _db.ChangeLogs.Add(log);
            _db.SaveChanges();
        }
    }
}
