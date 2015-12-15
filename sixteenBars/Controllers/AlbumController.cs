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
using System.Web.Script.Serialization;
using WebMatrix.WebData;

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

        public ActionResult Index(int? page)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Albums";
            ViewBag.MetaDescription = "List of Hip-Hop albums";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
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

         
            return View(albumQuotes.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Album/Details/5

        public ActionResult Details(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

            Album album = _db.Albums.Find(id);
            AlbumDetailsViewModel albumViewModel = new AlbumDetailsViewModel();
            if (album != null)
            {
                if (album.Id > 0)
                {
                    ViewBag.Title = "Rhyme 4 Rhyme : " + album.Title + " : " + album.Artist.Name;
                    ViewBag.MetaDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                    ViewBag.MetaKeywords = album.Title + ", " + album.Artist.Name + ", Hip-Hop, hip hop, album, record, rap, music";

                    
                    albumViewModel.Id = album.Id;
                    albumViewModel.Title = album.Title;
                    albumViewModel.ArtistId = album.Artist.Id;
                    albumViewModel.ArtistName = album.Artist.Name;
                    albumViewModel.ReleaseDate = (DateTime)album.ReleaseDate;
                    try
                    {
                        albumViewModel.Tracks = _db.Tracks.Where(t => t.Album.Id == album.Id).OrderBy(t => t.Title).ToList();
                    }
                    catch (Exception ex) { 
                    }

                }
            }
            return View(albumViewModel);
        }

        public ActionResult NameTitleDetails(string artistname, string albumtitle)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

            Album album = _db.Albums.SingleOrDefault(a => a.Artist.Name.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").Replace("/", "").Replace("#", "").Replace(" ", "_").ToLower() == artistname.ToLower().Trim() && a.Title.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").Replace("/", "").Replace("#", "").Replace(" ", "_").ToLower() == albumtitle.ToLower().Trim());
            AlbumDetailsViewModel albumViewModel = new AlbumDetailsViewModel();
            if (album!=null)
            {
                ViewBag.Title = "Rhyme 4 Rhyme : " + album.Title + " : " + album.Artist.Name;
                ViewBag.MetaDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                ViewBag.MetaKeywords = album.Title + ", " + album.Artist.Name + ", Hip-Hop, hip hop, album, record, rap, music";

                albumViewModel.Id = album.Id;
                albumViewModel.Title = album.Title;
                albumViewModel.ArtistId = album.Artist.Id;
                albumViewModel.ArtistName = album.Artist.Name;
                albumViewModel.ReleaseDate = (DateTime)album.ReleaseDate;
                try
                {
                    albumViewModel.Tracks = _db.Tracks.Where(t => t.Album.Id == album.Id).OrderBy(t => t.Title).ToList();
                }
                catch (Exception ex)
                {
                }
            }
            return View("details", albumViewModel);
        }

        //
        // GET: /Album/Create
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create()
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

            return View();
        }

        //
        // POST: /Album/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create(Album album)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

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
                            Name = album.Artist.Name.Trim()
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
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

            Album album = _db.Albums.Find(id);



            return View(album);
        }

        //
        // POST: /Album/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(Album album)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";
            Album previousAlbum = null;
            if (ModelState.IsValid)
            {
                if (!AlbumExists(album.Title, album.Artist.Name))
                {
                    Album edittedAlbum = _db.Albums.Find(album.Id);
                    previousAlbum = edittedAlbum;
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

                    try
                    {
                        ChangeLog log = new ChangeLog();
                        log.Type = "album";
                        log.PreviousValues = new JavaScriptSerializer().Serialize(previousAlbum);
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
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

            Album album = _db.Albums.Find(id);

            return View(album);
        }

        //
        // POST: /Album/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";

            Album album = _db.Albums.Find(id);
            Album previousAlbum = album;
            _db.Albums.Remove(album);
            _db.SaveChanges();


            try
            {
                ChangeLog log = new ChangeLog();
                log.Type = "album";
                log.PreviousValues = new JavaScriptSerializer().Serialize(previousAlbum);
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