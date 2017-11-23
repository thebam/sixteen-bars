using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class QuoteAPIController : ApiController
    {

        ISixteenBarsDb _db;

        public QuoteAPIController()
        {
            this._db = new SixteenBarsDb();
        }

        public QuoteAPIController(ISixteenBarsDb db)
        {
            this._db = db;
        }

        [System.Web.Http.HttpPost]
        public Boolean QuoteExists([FromBody]String text)
        {
            Quote quote = _db.Quotes.SingleOrDefault(q => q.Text.ToLower() == text.Trim().ToLower());
            if (quote != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [System.Web.Http.HttpGet]
        public JsonResult AutoCompleteQuote(String text)
        {
            List<Quote> quotes = _db.Quotes.Where(q => q.Text.ToLower().Contains(text.Trim().ToLower())).OrderBy(q => q.Text).ToList();

            JsonResult result = new JsonResult();
            result.Data = quotes;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        [System.Web.Http.HttpGet]
        public JsonResult GetQuotes()
        {
            List<Quote> quotes = _db.Quotes.Where(q => q.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true && q.Artist.Enabled == true).OrderBy(q=>q.Artist.Name).OrderBy(q => q.Text).ToList();
            JsonResult result = new JsonResult();
            result.Data = quotes;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }
    }
}