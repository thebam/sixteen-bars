using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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


        public Boolean ArtistExists(String artistName) {
            Artist artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == artistName.Trim().ToLower());
            if (artist != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        public JsonResult AutoCompleteName(String artistName)
        {
            List<Artist> artists = _db.Artists.Where(a => a.Name.ToLower().Contains(artistName.Trim().ToLower())).OrderBy(a=>a.Name).ToList();
            
            JsonResult result = new JsonResult();
            result.Data = artists;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

 
    }
}
