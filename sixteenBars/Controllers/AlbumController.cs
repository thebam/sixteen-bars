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
        private SixteenBarsDb db = new SixteenBarsDb();

        //
        // GET: /Album/

        public ActionResult Index()
        {
            //Select * from Albums if Quote attached set isdeleteable to false
            var albumQuotes = (from a in db.Albums
                              from q in db.Quotes.Where(q=>q.Enabled == true && q.Track.Album.Id == a.Id).DefaultIfEmpty()
                              select new AlbumIndexViewModel
                              {
                                  Id = a.Id,
                                  Title = a.Title,
                                  ArtistName = a.Artist.Name,
                                  IsDeleteable = (q == null)? true: false
                              }).ToList();

            return View(albumQuotes);
        }

        //
        // GET: /Album/Details/5

        public ActionResult Details(int id = 0)
        {
            Album album = db.Albums.Find(id);
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
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(album);
        }

        //
        // GET: /Album/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Album album = db.Albums.Find(id);
            AlbumViewModel albumVM = new AlbumViewModel();
            if (album == null)
            {
                return HttpNotFound();
            }
            albumVM.Id = album.Id;
            albumVM.Title = album.Title;
            albumVM.ReleaseDate = album.ReleaseDate;
            albumVM.ArtistId = album.Artist.Id;

            var artists = db.Artists.ToList();
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
                Album album = db.Albums.Find(albumVM.Id);
                album.Title = albumVM.Title;
                album.ReleaseDate = albumVM.ReleaseDate;
                album.DateModified = DateTime.Now;
                if (albumVM.ArtistId == -1)
                {
                    album.Artist = new Artist() { Name = albumVM.ArtistName, DateModified = DateTime.Now };
                }
                else {
                    album.Artist = db.Artists.Find(albumVM.ArtistId);
                }

                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(albumVM);
        }

        //
        // GET: /Album/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Album album = db.Albums.Find(id);
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
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}