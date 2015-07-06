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
    public class TrackController : Controller
    {
        private ISixteenBarsDb _db;

        public TrackController() : this(new SixteenBarsDb()) { }

        public TrackController(ISixteenBarsDb db) {
            this._db = db;
        }

        //
        // GET: /Track/

        public ActionResult Index()
        {


            var tracks = (from track in _db.Tracks
                          join quote in _db.Quotes on track.Id equals quote.Track.Id into quotes
                          from trackQuotes in quotes.DefaultIfEmpty()
                          where track.Enabled == true
                          select new TrackIndexViewModel()
                          {
                              Id = track.Id,
                              Title = track.Title,
                              AlbumId = track.Album.Id,
                              AlbumTitle = track.Album.Title,
                              ArtistId = track.Album.Artist.Id,
                              ArtistName = track.Album.Artist.Name,
                              IsDeleteable = (trackQuotes == null) ? true:false
                          }).Distinct().OrderBy(t => t.Title).ToList();


            return View(tracks);
        }

        
        public ActionResult Details(int id = 0)
        {
            Track track = _db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        //
        // GET: /Track/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Track/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrackViewModel trackVM)
        {
            if (ModelState.IsValid)
            {
                Artist artist = _db.Artists.FirstOrDefault(a => a.Name.ToLower() == trackVM.ArtistName.Trim().ToLower());
                if (artist == null)
                {
                    _db.Artists.Add(new Artist() { Name = trackVM.ArtistName.Trim(), DateModified = DateTime.Now });
                    _db.SaveChanges();
                    artist = _db.Artists.FirstOrDefault(a => a.Name.ToLower() == trackVM.ArtistName.Trim().ToLower());
                }

                Album album = _db.Albums.FirstOrDefault(a => a.Title.ToLower() == trackVM.AlbumName.Trim().ToLower());
                if (album == null)
                {
                    _db.Albums.Add(new Album() { 
                        Title = trackVM.AlbumName.Trim(), 
                        Artist = artist,
                        ReleaseDate = trackVM.ReleaseDate,
                        DateModified = DateTime.Now 
                    });
                    _db.SaveChanges();
                    album = _db.Albums.FirstOrDefault(a => a.Title.ToLower() == trackVM.AlbumName.Trim().ToLower());
                }

                Track track = new Track();
                track.Title = trackVM.Title;
                track.ReleaseDate = trackVM.ReleaseDate;
                track.Album = album;

                _db.Tracks.Add(track);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trackVM);
        }

        //
        // GET: /Track/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Track track = _db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        //
        // POST: /Track/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Track track)
        {
            if (ModelState.IsValid)
            {
                _db.SetModified(track);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(track);
        }

        //
        // GET: /Track/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Track track = _db.Tracks.Find(id);
            if (track == null)
            {
                return HttpNotFound();
            }
            return View(track);
        }

        //
        // POST: /Track/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Track track = _db.Tracks.Find(id);
            _db.Tracks.Remove(track);
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