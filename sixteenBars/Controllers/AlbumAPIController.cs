using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
namespace sixteenBars.Controllers
{
    public class AlbumAPIController : ApiController
    {
        private ISixteenBarsDb _db;

        public AlbumAPIController() {
            _db = new SixteenBarsDb();
        }

        public AlbumAPIController(ISixteenBarsDb db) {
            _db = db;
        }

        [System.Web.Http.HttpGet]
        public Boolean AlbumExists(String title, String artist = null)
        {
            Album album = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == title.Trim().ToLower() && a.Artist.Name.ToLower() == artist.Trim().ToLower());
            if (album == null)
            {
                return false;
            }
            else {
                return true;
            }
        }

        [System.Web.Http.HttpGet]
        public JsonResult AlbumAutoComplete(String title,String artist = null)
        {
            List<Album> albums;
            if (artist == null)
            {
                albums = _db.Albums.Where(a => a.Title.ToLower().Contains(title.Trim().ToLower())).OrderBy(a => a.Title).ToList();
            }
            else {
                albums = _db.Albums.Where(a => a.Title.ToLower().Contains(title.Trim().ToLower()) && a.Artist.Name.ToLower().Contains(artist.Trim().ToLower())).OrderBy(a => a.Title).ToList();
            }
            JsonResult results = new JsonResult();
            results.Data = albums;
            results.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return results;
        }

        [System.Web.Http.HttpGet]
        public JsonResult GetAlbums()
        {
            List<Album> albums = _db.Albums.Where(a => a.Enabled == true && a.Artist.Enabled==true).OrderBy(a=>a.Artist.Name).OrderBy(a => a.Title).ToList();
            JsonResult result = new JsonResult();
            result.Data = albums;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}
