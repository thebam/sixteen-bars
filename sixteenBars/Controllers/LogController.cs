using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class LogController : Controller
    {
        private ISixteenBarsDb _db;

        public LogController() {
            _db = new SixteenBarsDb();
        }

        public LogController(ISixteenBarsDb db) {
            _db = db;
        }

        [ChildActionOnly]
        public void Log(ChangeLog log){
            _db.ChangeLogs.Add(log);
            _db.SaveChanges();
        }

    }
}
