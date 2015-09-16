using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using sixteenBars.Models;
using System.Web.Mvc;
using sixteenBars.Library;

namespace sixteenBars.Controllers
{
    public class SearchAPIController : ApiController
    {
        ISixteenBarsDb _db;

        public SearchAPIController()
        {
            this._db = new SixteenBarsDb();
        }

        public SearchAPIController(ISixteenBarsDb db) {
            this._db = db;
        }

        [System.Web.Http.HttpGet]
        public JsonResult Search(String searchTerm = null, String searchType = "quote")
        {
            List<Library.Quote> SearchResults = new List<Library.Quote>();
            switch (searchType)
            {
                case "album":
                    SearchResults = (from q in _db.Quotes
                                     where q.Track.Album.Title.Contains(searchTerm)
                                     select q).ToList();
                    break;
                case "artist":
                    SearchResults = (from q in _db.Quotes
                                     where q.Artist.Name.Contains(searchTerm)
                                     select q).ToList();
                    break;
                case "track":
                    SearchResults = (from q in _db.Quotes
                                     where q.Track.Title.Contains(searchTerm)
                                     select q).ToList();
                    break;
                default:
                    SearchResults = (from q in _db.Quotes
                                     where q.Text.Contains(searchTerm)
                                     select q).ToList();

                    foreach (Quote quote in SearchResults)
                    {
                        quote.Text = WordLink.CreateLinks(quote.Text);
                        quote.Text = LanguageFilter.Filter(quote.Text);
                    }

                    break;
            }

            JsonResult result = new JsonResult();
            result.Data = SearchResults;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        } 
    }
}
