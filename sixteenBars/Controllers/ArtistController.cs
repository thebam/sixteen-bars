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
    public class ArtistController : Controller
    {
        private SixteenBarsDb db = new SixteenBarsDb();

        //
        // GET: /Artist/

        public ActionResult Index()
        {

            var ArtistTrackAlbum = (from artist in db.Artists
                                    join track in db.Tracks on artist.Id equals track.Album.Artist.Id into tracks
                                    from artistTrack in tracks.DefaultIfEmpty()
                                    join quote in db.Quotes on artist.Id equals quote.Artist.Id into quotes
                                    from artistQuote in quotes.DefaultIfEmpty()
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
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        //
        // GET: /Artist/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Artist/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        //
        // GET: /Artist/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        //
        // POST: /Artist/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        //
        // GET: /Artist/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        //
        // POST: /Artist/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            if (artist != null)
            {
                //make sure that the artist doesn't have any ablums or quotes in the database to avoid any orphaned records.
                Quote quote = db.Quotes.SingleOrDefault(q => q.Artist.Id == id);
                if (quote == null) {
                    Album album = db.Albums.SingleOrDefault(a => a.Artist.Id == id);
                    if (album == null) {
                        db.Artists.Remove(artist);
                        db.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}