using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class ArtistAPIController : ApiController
    {

        ISixteenBarsDb _db;

        public ArtistAPIController() {
            this._db = new SixteenBarsDb();
        }

        public ArtistAPIController(ISixteenBarsDb db) {
            this._db = db;
        }

        [System.Web.Http.HttpPost]
        public Boolean ArtistExists([FromBody]String name) {
            Artist artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == name.Trim().ToLower());
            if (artist != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        [System.Web.Http.HttpGet]
        public JsonResult AutoCompleteName(String name)
        {
            List<Artist> artists = _db.Artists.Where(a => a.Name.ToLower().Contains(name.Trim().ToLower())).OrderBy(a=>a.Name).ToList();
            
            JsonResult result = new JsonResult();
            result.Data = artists;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [System.Web.Http.HttpGet]
        public List<Artist> GetArtists() {
            return _db.Artists.Where(a=>a.Enabled==true).OrderBy(a=>a.Name).ToList();
        }
    }
}
