using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sixteenBars.Library;
using sixteenBars.Models;
using PagedList;
using WebMatrix.WebData;
using System.Web.Script.Serialization;

namespace sixteenBars.Controllers
{
    public class QuoteController : Controller
    {
        private ISixteenBarsDb _db;

        public QuoteController() :this(new SixteenBarsDb()){ 
        
        }

        public QuoteController(ISixteenBarsDb db) {
            this._db = db;
        }

        [ChildActionOnly]
        public List<Quote> RandomQuotes(Boolean allowExplicit = false, Int32 numberOfResults = 1)
        {
            List<Quote> quotes = null;
            if (_db.Quotes.Count() > 0)
            {

                if (allowExplicit)
                {
                    quotes = _db.Quotes.OrderBy(q => Guid.NewGuid()).Take(numberOfResults).ToList();
                }
                else {
                    quotes = _db.Quotes.Where(q => q.Explicit == false).OrderBy(q => Guid.NewGuid()).Take(numberOfResults).ToList();
                }


                
                
                foreach (Quote quote in quotes)
                {
                    quote.Text = WordLink.CreateLinks(quote.Text);
                    
                }

            }


            return quotes;
        }


        //
        // GET: /Quote/

        public ActionResult Index(int? page)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Quotes";
            ViewBag.MetaDescription = "List of Hip-Hop quotes";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            HttpCookie isExplicit = Request.Cookies["explicit"];
            List<Quote> quotes = new List<Quote>();
            if (isExplicit != null)
            {
                if (isExplicit.Value == "explicit")
                {
                    quotes = _db.Quotes.OrderByDescending(q => q.DateCreated).ToList();
                }
                else
                {
                    quotes = _db.Quotes.Where(q => q.Explicit == false).OrderByDescending(q => q.DateCreated).ToList();
                }
            }
            else {
                quotes = _db.Quotes.Where(q => q.Explicit == false).OrderByDescending(q => q.DateCreated).ToList();
            }
            

            return View(quotes.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Quote/Details/5

        public ActionResult Details(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            QuoteViewModel quoteVM = new QuoteViewModel();
            if (quote != null)
            {
                ViewBag.Title = "Rhyme 4 Rhyme : " + quote.Artist.Name + " : " + quote.Text;
                ViewBag.MetaDescription = quote.Text + " - Hip-Hop quote said by " + quote.Artist.Name + " on track " + quote.Track.Title;
                ViewBag.MetaKeywords = quote.Text +", " +quote.Artist.Name+ ", "+ quote.Track.Title +",Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
                ViewBag.OGTitle = "Quote from Hip-Hop Artist " + quote.Artist.Name;
                ViewBag.OGDescription = "\"" +quote.Text + "\" from track " + quote.Track.Title;
                ViewBag.OGAppID = "1474377432864288";
                quoteVM.Id = quote.Id;
                quoteVM.Text = WordLink.CreateLinks(quote.Text);

                quoteVM.Explanation = quote.Explanation;
                quoteVM.ArtistName = quote.Artist.Name;
                quoteVM.ArtistId = quote.Artist.Id;
                quoteVM.TrackName = quote.Track.Title;
                quoteVM.TrackId = quote.Track.Id;
                quoteVM.AlbumName = quote.Track.Album.Title;
                quoteVM.AlbumId = quote.Track.Album.Id;
                quoteVM.AlbumArtistName = quote.Track.Album.Artist.Name;
                quoteVM.AlbumArtistId = quote.Track.Album.Artist.Id;
            }
            return View(quoteVM);
        }


        public ActionResult NameQuoteDetails(string speakername, string quotetext)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.SingleOrDefault(q => q.Artist.Name.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").ToLower() == speakername.ToLower().Trim() && q.Text.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").ToLower().StartsWith(quotetext.Trim()));
            QuoteViewModel quoteVM = new QuoteViewModel();
            if (quote != null)
            {
                ViewBag.Title = "Rhyme 4 Rhyme : " + quote.Artist.Name + " : " + quote.Text;
                ViewBag.MetaDescription = quote.Text + " - Hip-Hop quote said by " + quote.Artist.Name + " on track " + quote.Track.Title;
                ViewBag.MetaKeywords = quote.Text + ", " + quote.Artist.Name + ", " + quote.Track.Title + ",Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";


                ViewBag.OGTitle = "Quote from Hip-Hop Artist " + quote.Artist.Name;
                ViewBag.OGDescription = "\"" + quote.Text + "\" from track " + quote.Track.Title;
                ViewBag.OGAppID = "1474377432864288";


                quoteVM.Id = quote.Id;
                quoteVM.Text = LanguageFilter.Filter(WordLink.CreateLinks(quote.Text));

                quoteVM.Explanation = quote.Explanation;
                quoteVM.ArtistName = quote.Artist.Name;
                quoteVM.ArtistId = quote.Artist.Id;
                quoteVM.TrackName = quote.Track.Title;
                quoteVM.TrackId = quote.Track.Id;
                quoteVM.AlbumName = quote.Track.Album.Title;
                quoteVM.AlbumId = quote.Track.Album.Id;
                quoteVM.AlbumArtistName = quote.Track.Album.Artist.Name;
                quoteVM.AlbumArtistId = quote.Track.Album.Artist.Id;
            }
            return View("details",quoteVM);
        }

        //
        // GET: /Quote/Create
        [Authorize(Roles="admin,editor")]
        public ActionResult Create()
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = new Quote();

            return View(quote);
        }

        //
        // POST: /Quote/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create(Quote quote)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            if (ModelState.IsValid)
            {
                Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                    if (tempArtist == null)
                    {
                        _db.Artists.Add(new Artist()
                        {
                            Name = quote.Artist.Name.Trim()
                        });
                        _db.SaveChanges();
                        quote.Artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                    }
                    else {
                        quote.Artist = tempArtist;
                    }

                    Artist tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    if (tempArtistAlbum == null)
                    {
                        _db.Artists.Add(new Artist()
                        {
                            Name = quote.Track.Album.Artist.Name.Trim()
                        });
                        _db.SaveChanges();
                        tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    }

                    Album tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    if (tempAlbum == null)
                    {
                        _db.Albums.Add(new Album()
                        {
                            Title = quote.Track.Album.Title.Trim(),
                            Artist = tempArtistAlbum,
                            ReleaseDate = quote.Track.Album.ReleaseDate
                        });
                        _db.SaveChanges();
                        tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    }
                    


                    Track tempTrack = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                    if (tempTrack == null)
                    {
                        _db.Tracks.Add(new Track()
                        {
                            Title = quote.Track.Title.Trim(),
                            ReleaseDate = quote.Track.Album.ReleaseDate,
                            Album = tempAlbum
                        });
                        _db.SaveChanges();
                        quote.Track = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                    }
                    else
                    {
                        quote.Track = tempTrack;
                    }
                

                _db.Quotes.Add(quote);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quote);
        }

        //
        // GET: /Quote/Edit/5
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            return View(quote);
        }

        //
        // POST: /Quote/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(Quote quote)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote previousQuote = _db.Quotes.Find(quote.Id);
            if (ModelState.IsValid)
            {
                Quote edittedQuote = _db.Quotes.Find(quote.Id);
                edittedQuote.Text = quote.Text;
                edittedQuote.Explanation = quote.Explanation;
                edittedQuote.Explicit = quote.Explicit;
                Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                if (tempArtist == null)
                {
                    _db.Artists.Add(new Artist()
                    {
                        Name = quote.Artist.Name.Trim()
                    });
                    _db.SaveChanges();
                    edittedQuote.Artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                }
                else
                {
                    edittedQuote.Artist = tempArtist;
                }

                Artist tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                if (tempArtistAlbum == null)
                {
                    _db.Artists.Add(new Artist()
                    {
                        Name = quote.Track.Album.Artist.Name.Trim()
                    });
                    _db.SaveChanges();
                    tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                }

                Album tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                if (tempAlbum == null)
                {
                    _db.Albums.Add(new Album()
                    {
                        Title = quote.Track.Album.Title.Trim(),
                        Artist = tempArtistAlbum,
                        ReleaseDate = quote.Track.Album.ReleaseDate
                    });
                    _db.SaveChanges();
                    tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                }


                Track tempTrack = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                if (tempTrack == null)
                {
                    _db.Tracks.Add(new Track()
                    {
                        Title = quote.Track.Title.Trim(),
                        ReleaseDate = quote.Track.Album.ReleaseDate,
                        Album = tempAlbum
                    });
                    _db.SaveChanges();
                    edittedQuote.Track = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                }
                else
                {
                    edittedQuote.Track = tempTrack;
                }
                _db.SetModified(edittedQuote);
                _db.SaveChanges();

                try
                {
                    ChangeLog log = new ChangeLog();
                    log.Type = "quote";
                    log.PreviousValues = new JavaScriptSerializer().Serialize(previousQuote);
                    log.UserId = WebSecurity.CurrentUserId;

                    LogController ctrl = new LogController();
                    ctrl.Log(log);
                }
                catch (Exception ex) { 
                    //TO DO handle exception - email change?
                }

                return RedirectToAction("Index");
            }
            else {
                ViewBag.ErrorMessage = "The quote already exists.";
                return View(quote);
            }
        }

        //
        // GET: /Quote/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            QuoteViewModel quoteVM = new QuoteViewModel();

            if (quote == null)
            {
                
            } else {
                quoteVM.Id = quote.Id;
                quoteVM.Text = quote.Text;
                quoteVM.ArtistName = quote.Artist.Name;
                quoteVM.TrackName = quote.Track.Title;
            }
            return View(quoteVM);
        }

        //
        // POST: /Quote/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            Quote previousQuote = quote;
            _db.Quotes.Remove(quote);
            _db.SaveChanges();


            try
            {
                ChangeLog log = new ChangeLog();
                log.Type = "quote";
                log.PreviousValues = new JavaScriptSerializer().Serialize(previousQuote);
                log.UserId = WebSecurity.CurrentUserId;

                LogController ctrl = new LogController();
                ctrl.Log(log);
            }
            catch (Exception ex)
            {
                //TO DO handle exception - email change?
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (_db is IDisposable)
            {
                ((IDisposable)_db).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}