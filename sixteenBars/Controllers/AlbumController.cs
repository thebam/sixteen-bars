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
    public class AlbumController : Controller
    {
        private ISixteenBarsDb _db;

        public AlbumController() : this(new SixteenBarsDb()) { }


        public AlbumController(ISixteenBarsDb db)
        {
            this._db = db;
        }

        public Boolean AlbumExists(String title, String artistName)
        {
            Album foundAlbum = _db.Albums.SingleOrDefault(album => album.Title.ToLower() == title.Trim().ToLower() && album.Artist.Name.ToLower() == artistName.Trim().ToLower());
            if (foundAlbum != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //
        // GET: /Album/

        public ActionResult Index()
        {
            var albumQuotes = (from album in _db.Albums
                               join quote in _db.Quotes on album.Id equals quote.Track.Album.Id into quotes
                               from albumQuote in quotes.DefaultIfEmpty()
                               select new AlbumIndexViewModel
                               {
                                   Id = album.Id,
                                   Title = album.Title,
                                   ArtistName = album.Artist.Name,
                                   ArtistId = album.Artist.Id,
                                   IsDeleteable = (albumQuote == null) ? true : false
                               }).Distinct().OrderBy(a => a.Title).ToList();

            return View(albumQuotes);
        }

        //
        // GET: /Album/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = _db.Albums.Find(id);

            return View(album);
        }

        //
        // GET: /Album/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Album/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                Artist tempArtist = null;
                Album tempAlbum = null;
                tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == album.Artist.Name.ToLower());
                if (tempAlbum == null)
                {
                    tempArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                    if (tempArtist != null)
                    {
                        album.Artist = tempArtist;

                    }
                    else
                    {
                        _db.Artists.Add(new Artist()
                        {
                            Name = album.Artist.Name
                        });
                        _db.SaveChanges();
                        album.Artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                    }


                    _db.Albums.Add(album);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "The album '" + album.Title + "' by '" + album.Artist.Name + "' already exists.";
                    return View(album);
                }
            }

            return View(album);
        }

        //
        // GET: /Album/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id = 0)
        {
            Album album = _db.Albums.Find(id);



            return View(album);
        }

        //
        // POST: /Album/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                if (!AlbumExists(album.Title, album.Artist.Name))
                {
                    Album edittedAlbum = _db.Albums.Find(album.Id);
                    edittedAlbum.Title = album.Title;
                    edittedAlbum.ReleaseDate = album.ReleaseDate;

                    Artist tempArtist = _db.Artists.FirstOrDefault(artist => artist.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                    if (tempArtist == null)
                    {
                        edittedAlbum.Artist = new Artist()
                        {
                            Name = album.Artist.Name,
                            DateModified = DateTime.Now
                        };
                    }
                    else
                    {
                        edittedAlbum.Artist = tempArtist;
                    }



                    _db.SetModified(edittedAlbum);
                    _db.SaveChanges();
                    return RedirectToAction("Index");


                }

                return View(album);
            }
            else
            {
                ViewBag.ErrorMessage = "The album '" + album.Title + "' by '" + album.Artist.Name + "' already exists.";
                return View(album);
            }
        }

        //
        // GET: /Album/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            Album album = _db.Albums.Find(id);

            return View(album);
        }

        //
        // POST: /Album/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = _db.Albums.Find(id);
            _db.Albums.Remove(album);
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