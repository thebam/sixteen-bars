using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult Search(String searchTerm = null, String searchType="quote")
        {
            sixteenBars.Models.SixteenBarsDb db = new Models.SixteenBarsDb();

            List<Library.Quote> SearchResults = new List<Library.Quote>();
            switch (searchType) { 
                case "album":
                    SearchResults = (from q in db.Quotes
                                     where q.Track.Album.Title.Contains(searchTerm)
                                     select q).ToList();
                    break;
                case "artist":
                    SearchResults = (from q in db.Quotes
                                     where q.Artist.Name.Contains(searchTerm)
                                     select q).ToList();
                    break;
                case "track":
                    SearchResults = (from q in db.Quotes
                                     where q.Track.Title.Contains(searchTerm)
                                     select q).ToList();
                    break;
                default:
                    SearchResults = (from q in db.Quotes
                                     where q.Text.Contains(searchTerm)
                                     select q).ToList();
                    break;
        }
            return this.Json(SearchResults);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        
    }
}
