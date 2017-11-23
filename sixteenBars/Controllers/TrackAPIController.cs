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

        [System.Web.Http.HttpGet]
        public bool TrackExists(String trackTitle, String albumTitle, String artistName, DateTime releaseDate)
        {
            Track trackFound = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == trackTitle.ToLower().Trim() && t.Album.Title.ToLower() == albumTitle.ToLower().Trim() && t.Album.Artist.Name.ToLower() == artistName.ToLower().Trim() && t.ReleaseDate == releaseDate);
            
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

        [System.Web.Http.HttpGet]
        public JsonResult GetTracks()
        {
            List<Track> tracks = _db.Tracks.Where(t => t.Enabled == true && t.Album.Enabled == true && t.Album.Artist.Enabled == true).OrderBy(t => t.Album.Title).OrderBy(t=>t.Order).ToList();
            JsonResult result = new JsonResult();
            result.Data = tracks;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
