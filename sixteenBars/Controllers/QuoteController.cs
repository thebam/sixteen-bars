using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sixteenBars.Library;
using sixteenBars.Models;

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
        public ActionResult RandomQuotes(Boolean allowExplicit, Int32 numberOfResults=1) {
            List<Quote> quotes = null;
            if (_db.Quotes.Count() > 0)
            { 
                //Random rand = new Random();
                //int toSkip = rand.Next(0, _db.Quotes.Where(q => q.Explicit == allowExplicit).Count());
                //quotes = _db.Quotes.Where(q => q.Explicit == allowExplicit).OrderBy(q => q.Id).Skip(toSkip).Take(numberOfResults).ToList();


                quotes = _db.Quotes.Where(q => q.Explicit == allowExplicit).OrderBy(q => Guid.NewGuid()).Take(numberOfResults).ToList();


            }


            return PartialView("_RandomQuotes", quotes);
        }


        //
        // GET: /Quote/

        public ActionResult Index()
        {
            return View(_db.Quotes.ToList());
        }

        //
        // GET: /Quote/Details/5

        public ActionResult Details(int id = 0)
        {
            Quote quote = _db.Quotes.Find(id);
            QuoteViewModel quoteVM = new QuoteViewModel();
            if (quote != null)
            {
                quoteVM.Id = quote.Id;
                quoteVM.Text = quote.Text;
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

        //
        // GET: /Quote/Create
        [Authorize(Roles="admin")]
        public ActionResult Create()
        {
            Quote quote = new Quote();

            return View(quote);
        }

        //
        // POST: /Quote/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Quote quote)
        {
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
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Quote quote = _db.Quotes.Find(id);
            return View(quote);
        }

        //
        // POST: /Quote/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Quote quote)
        {
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
            Quote quote = _db.Quotes.Find(id);
            _db.Quotes.Remove(quote);
            _db.SaveChanges();
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