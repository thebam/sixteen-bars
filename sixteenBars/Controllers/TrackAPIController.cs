using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class TrackAPIController : ApiController
    {
        private ISixteenBarsDb _db;
        public TrackAPIController() {
            _db = new SixteenBarsDb();
        }

        public TrackAPIController(ISixteenBarsDb db) {
            _db = db;
        }

        [System.Web.Http.HttpPost]
        public bool TrackExists([FromBody]String trackName)
        {
            Track trackFound = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == trackName.Trim().ToLower());
            if (trackFound == null) {
                return false;
            }else{
                return true;
            }
        }

        [System.Web.Http.HttpGet]
        public JsonResult TrackAutoComplete(String title)
        {
            List<Track> tracks = _db.Tracks.Where(t => t.Title.ToLower().Contains(title.Trim().ToLower())).OrderBy(t => t.Title).ToList();
            JsonResult result = new JsonResult();
            result.Data = tracks;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
