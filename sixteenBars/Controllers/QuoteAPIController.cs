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
        public List<QuoteAPIViewModel> GetQuotes()
        {
            var quotes = (from quote in _db.Quotes
                          where quote.Enabled == true && quote.Track.Enabled == true && quote.Track.Album.Enabled == true && quote.Track.Album.Artist.Enabled == true && quote.Artist.Enabled == true
                          select new QuoteAPIViewModel
                          {
                              QuoteId = quote.QuoteId,
                              Text = quote.Text,
                              Explanation = quote.Explanation,
                              Explicit = quote.Explicit,
                              ArtistId = quote.Artist.ArtistId,
                              ArtistName = quote.Artist.Name,
                              TrackId = quote.Track.TrackId,
                              TrackName = quote.Track.Title,
                              AlbumId = quote.Track.Album.AlbumId,
                              AlbumName = quote.Track.Album.Title,
                              AlbumArtistId = quote.Track.Album.Artist.ArtistId,
                              AlbumArtistName = quote.Track.Album.Artist.Name,
                              ArtistImage = quote.Artist.Image,
                              AlbumImage = quote.Track.Album.Image,
                                Video = quote.Track.Video
    }).ToList<QuoteAPIViewModel>();
            return quotes;
        }
    }
}