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

        public AlbumController() : this(new SixteenBarsDb()){}
        

        public AlbumController(ISixteenBarsDb db) {
            this._db = db;
        }

        public Boolean AlbumExists(String title, String artistName)
        {
            Album foundAlbum = _db.Albums.SingleOrDefault(album => album.Title.ToLower() == title.Trim().ToLower() && album.Artist.Name.ToLower() == artistName.Trim().ToLower());
            if (foundAlbum != null)
            {
                return true;
            }else{
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
                                  IsDeleteable = (albumQuote == null) ? true : false
                              }).Distinct().OrderBy(a=>a.Title).ToList();

            return View(albumQuotes);
        }

        //
        // GET: /Album/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = _db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        //
        // GET: /Album/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Album/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                Artist tempArtist;
                Album tempAlbum=null;
                if (album.Artist.Name == null) {
                    tempArtist = _db.Artists.Find(album.Artist.Id);
                }else{ 
                    tempArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                }


                if (tempArtist != null)
                {
                    album.Artist = tempArtist;
                    tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == album.Artist.Name.ToLower());
                }
                else
                {
                    if (album.Artist.Name != null)
                    {
                        tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == album.Artist.Name.ToLower());
                        if (tempAlbum == null)
                        {
                            _db.Artists.Add(new Artist()
                            {
                                Name = album.Artist.Name
                            });
                            _db.SaveChanges();
                            album.Artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                        }
                    }
                    else
                    {
                        throw new Exception("No artist found with the provided id");
                    }
                }


                
                


                
                if (tempAlbum == null)
                {
                    _db.Albums.Add(album);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(album);
        }

        //
        // GET: /Album/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = _db.Albums.Find(id);
            AlbumViewModel albumVM = new AlbumViewModel();
            if (album == null)
            {
                return HttpNotFound();
            }
            albumVM.Id = album.Id;
            albumVM.Title = album.Title;
            albumVM.ReleaseDate = album.ReleaseDate;
            albumVM.ArtistId = album.Artist.Id;

            var artists = _db.Artists.ToList();
            artists.Add(new Artist() { Name = "Add New Artist", Id = -1 });
            albumVM.Artists = new SelectList(artists, "Id", "Name", album.Artist.Id);


            return View(albumVM);
        }

        //
        // POST: /Album/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlbumViewModel albumVM)
        {
            if (ModelState.IsValid)
            {

                    Album album = _db.Albums.Find(albumVM.Id);
                    album.Title = albumVM.Title;
                    album.ReleaseDate = albumVM.ReleaseDate;
                    album.DateModified = DateTime.Now;
                    if (albumVM.ArtistName !=null)
                    {
                        Artist tempArtist = _db.Artists.SingleOrDefault(artist => artist.Name.ToLower() == albumVM.ArtistName.Trim().ToLower());
                        if (tempArtist == null)
                        {
                            album.Artist = new Artist() { Name = albumVM.ArtistName, DateModified = DateTime.Now };
                        }
                        else {
                            album.Artist = tempArtist;
                        }
                    }
                    else {
                        album.Artist = _db.Artists.Find(albumVM.ArtistId);
                    }
                    if (!AlbumExists(album.Title, album.Artist.Name))
                    { 
                        _db.SetModified(album);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                
            }
            return View(albumVM);
        }

        //
        // GET: /Album/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Album album = _db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
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