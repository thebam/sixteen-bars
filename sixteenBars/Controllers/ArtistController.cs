using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sixteenBars.Library;
using sixteenBars.Models;


namespace sixteenBars.Controllers
{
    public class ArtistController : Controller
    {
        private ISixteenBarsDb _db;
        public ArtistController() : this(new SixteenBarsDb())
        {
        }
        public ArtistController(ISixteenBarsDb db)
        {
            this._db = db;
        }


       

        //
        // GET: /Artist/

        public ActionResult Index()
        {

            var ArtistTrackAlbum = (from artist in _db.Artists
                                    join track in _db.Tracks on artist.Id equals track.Album.Artist.Id into tracks
                                    from artistTrack in tracks.DefaultIfEmpty()
                                    join quote in _db.Quotes on artist.Id equals quote.Artist.Id into quotes
                                    from artistQuote in quotes.DefaultIfEmpty()
                                    where artist.Enabled == true
                        select new ArtistIndexViewModel()
                        {
                            Id = artist.Id,
                            Name = artist.Name,
                            IsDeleteable = (artistTrack == null && artistQuote == null) ? true : false
                        }).Distinct().OrderBy(a => a.Name).ToList();

            return View(ArtistTrackAlbum);
        }

        //
        // GET: /Artist/Details/5

        public ActionResult Details(int id = 0)
        {


            //Create view model to display quotes and albums

            Artist artist = _db.Artists.Find(id);
            ArtistDetailViewModel artistVM = new ArtistDetailViewModel();
            if (artist == null)
            {
                return View(artistVM);
            }
            else {
                
                artistVM.Id = artist.Id;
                artistVM.Name = artist.Name;
                artistVM.Albums = _db.Albums.Where(a => a.Artist.Id == artist.Id).OrderBy(a => a.Title).ToList();
                artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id).OrderBy(q => q.Text).ToList();
            }
            return View(artistVM);
        }

        public ActionResult NameDetails(string artistname)
        {
            Artist artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == artistname.ToLower().Trim());

            ArtistDetailViewModel artistVM = new ArtistDetailViewModel();
            if (artist == null)
            {
                return View(artistVM);
            }
            else
            {

                artistVM.Id = artist.Id;
                artistVM.Name = artist.Name;
                artistVM.Albums = _db.Albums.Where(a => a.Artist.Id == artist.Id).OrderBy(a => a.Title).ToList();
                artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id).OrderBy(q => q.Text).ToList();
            }
            return View("details", artistVM);
        }

        //
        // GET: /Artist/Create
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Artist/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name == artist.Name);
                if (tempArtist == null)
                {
                    _db.Artists.Add(artist);
                    _db.SaveChanges();
                }
                else {
                    ViewBag.ErrorMessage = "The artist titled '" + artist.Name + "' already exists.";
                    return View(artist);
                }
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        //
        // GET: /Artist/Edit/5
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(int id = 0)
        {
            Artist artist = _db.Artists.Find(id);
            
            return View(artist);
        }

        //
        // POST: /Artist/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name == artist.Name);
                if (tempArtist == null)
                {
                    artist.DateModified = DateTime.Now;
                    _db.SetModified(artist);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    ModelState.AddModelError("", "An artist by that name already exists.");
                    return View(artist);
                }
            }
            return View(artist);
        }

        //
        // GET: /Artist/Delete/5
        [Authorize(Roles = "admin,editor")]
        public ActionResult Delete(int id = 0)
        {
            Artist artist = _db.Artists.Find(id);
            
            return View(artist);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = _db.Artists.Find(id);
            if (artist != null)
            {
                //make sure that the artist doesn't have any ablums or quotes in the database to avoid any orphaned records.
                Quote quote = _db.Quotes.SingleOrDefault(q => q.Artist.Id == id);
                if (quote == null) {
                    Album album = _db.Albums.SingleOrDefault(a => a.Artist.Id == id);
                    if (album == null)
                    {
                        _db.Artists.Remove(artist);
                        _db.SaveChanges();
                    }
                    else {
                        ViewBag.ErrorMessage = "The artist '" + artist.Name + "' can't be deleted because they have associated albums. Delete albums first.";
                        return View(artist);
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "The artist '" + artist.Name + "' can't be deleted because they have associated quotes. Delete quotes first.";
                    return View(artist);
                }
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