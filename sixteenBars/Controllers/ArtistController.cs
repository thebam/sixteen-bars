using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using sixteenBars.Library;
using sixteenBars.Models;
using PagedList;
using System.Web.Script.Serialization;
using WebMatrix.WebData;
using System.Web;


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

        public ActionResult Index(int? page)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Artists";
            ViewBag.MetaDescription = "List of Hip-Hop artists and rappers";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
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

            return View(ArtistTrackAlbum.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Artist/Details/5

        public ActionResult Details(int id = 0)
        {

            ViewBag.Title = "Rhyme 4 Rhyme : Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            //Create view model to display quotes and albums
            HttpCookie isExplicit = Request.Cookies["explicit"];
            Artist artist = _db.Artists.Find(id);
            ArtistDetailViewModel artistVM = new ArtistDetailViewModel();
            if (artist == null)
            {
                return View(artistVM);
            }
            else {

                ViewBag.Title = "Rhyme 4 Rhyme : " + artist.Name;
                ViewBag.MetaDescription = "Quotes and albums from Hip-Hop artist " + artist.Name;
                ViewBag.MetaKeywords = artist.Name + ",hip-hop, hip hop, artist, rapper, rap, music, quote, album";

                artistVM.Id = artist.Id;
                artistVM.Name = artist.Name;
                artistVM.Albums = _db.Albums.Where(a
                    => a.Artist.Id == artist.Id).OrderBy(a => a.Title).ToList();
                if (isExplicit != null)
                {
                    if (isExplicit.Value == "explicit")
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id).OrderBy(q => q.Text).ToList();
                    }
                    else
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id && q.Explicit == false).OrderBy(q => q.Text).ToList();
                    }
                }
                else {
                    artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id && q.Explicit == false).OrderBy(q => q.Text).ToList();
                }
            }
            return View(artistVM);
        }

        public ActionResult NameDetails(string artistname)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            HttpCookie isExplicit = Request.Cookies["explicit"];
            
            artistname = URLClean.Clean(artistname);

            Artist artist = _db.Artists.SingleOrDefault(a => a.Name.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").ToLower() == artistname.ToLower().Trim());

            ArtistDetailViewModel artistVM = new ArtistDetailViewModel();
            if (artist == null)
            {
                return View(artistVM);
            }
            else
            {
                ViewBag.Title = "Rhyme 4 Rhyme : " + artist.Name;
                ViewBag.MetaDescription = "Quotes and albums from Hip-Hop artist " + artist.Name;
                ViewBag.MetaKeywords = artist.Name + ",hip-hop, hip hop, artist, rapper, rap, music,quote,album";

                artistVM.Id = artist.Id;
                artistVM.Name = artist.Name;
                artistVM.Albums = _db.Albums.Where(a => a.Artist.Id == artist.Id).OrderBy(a => a.Title).ToList();
                if (isExplicit != null)
                {
                    if (isExplicit.Value == "explicit")
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id).OrderBy(q => q.Text).ToList();
                    }
                    else
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id && q.Explicit == false).OrderBy(q => q.Text).ToList();
                    }
                }
                else {
                    artistVM.Quotes = _db.Quotes.Where(q => q.Artist.Id == artist.Id && q.Explicit == false).OrderBy(q => q.Text).ToList();
                }
            }
            return View("details", artistVM);
        }

        //
        // GET: /Artist/Create
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create()
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            return View();
        }

        //
        // POST: /Artist/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create(Artist artist)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
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
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
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
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            if (ModelState.IsValid)
            {
                Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name == artist.Name);
                Artist previousArtist = null;
                if (tempArtist == null)
                {
                    previousArtist = artist;
                    artist.DateModified = DateTime.Now;
                    _db.SetModified(artist);
                    _db.SaveChanges();


                    try
                    {
                        ChangeLog log = new ChangeLog();
                        log.Type = "artist";
                        log.PreviousValues = new JavaScriptSerializer().Serialize(previousArtist);
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
                else {
                    ModelState.AddModelError("", "An artist by that name already exists.");
                    return View(artist);
                }
            }
            return View(artist);
        }

        //
        // GET: /Artist/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            Artist artist = _db.Artists.Find(id);
            
            return View(artist);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Artist";
            ViewBag.MetaDescription = "Hip-Hop artist or rapper";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            Artist artist = _db.Artists.Find(id);
            Artist previousArtist=null;
            if (artist != null)
            {
                //make sure that the artist doesn't have any ablums or quotes in the database to avoid any orphaned records.
                Quote quote = _db.Quotes.SingleOrDefault(q => q.Artist.Id == id);
                if (quote == null) {
                    Album album = _db.Albums.SingleOrDefault(a => a.Artist.Id == id);
                    if (album == null)
                    {
                        previousArtist = artist;
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

            try
            {
                ChangeLog log = new ChangeLog();
                log.Type = "artist";
                log.PreviousValues = new JavaScriptSerializer().Serialize(previousArtist);
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