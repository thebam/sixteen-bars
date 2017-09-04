using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using sixteenBars.Library;
using sixteenBars.Models;
using PagedList;
using System.Web;
using WebMatrix.WebData;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

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
                               join quote in _db.Quotes on album.AlbumId equals quote.Track.Album.AlbumId into quotes
                               from albumQuote in quotes.DefaultIfEmpty()
                               select new AlbumIndexViewModel
                               {
                                   Id = album.AlbumId,
                                   Title = album.Title,
                                   ArtistName = album.Artist.Name,
                                   ArtistId = album.Artist.ArtistId,
                                   Enabled = album.Enabled,
                                   IsDeleteable = (albumQuote == null) ? true : false
                               }).Distinct().OrderBy(a => a.Title).ToList();

            //TODO - remove enabled == false if not admin or editor
            if (!User.IsInRole("Admin") && !User.IsInRole("Editor"))
            {
                albumQuotes.RemoveAll(a => a.Enabled == false);
            }
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
                if (album.AlbumId > 0)
                {
                    ViewBag.Title = "Rhyme 4 Rhyme : " + album.Title + " : " + album.Artist.Name;
                    ViewBag.MetaDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                    ViewBag.MetaKeywords = album.Title + ", " + album.Artist.Name + ", Hip-Hop, hip hop, album, record, rap, music";



                    ViewBag.OGTitle = album.Title + " : " + album.Artist.Name;
                    ViewBag.OGDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                    ViewBag.OGAppID = "1474377432864288";
                    ViewBag.MetaAuthor = album.Artist.Name;


                    ViewBag.OGURL = "http://www.rhyme4rhyme.com/albums/" + URLClean.Clean(album.Artist.Name) + "/" + URLClean.Clean(album.Title);
                    ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                    ViewBag.TwitterTitle = "Rhyme 4 Rhyme : " + album.Title + " : " + album.Artist.Name;
                    ViewBag.TwitterDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                    ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";


                    ViewBag.AlbumImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";
                    ViewBag.PurchaseLinks = "";

                    albumViewModel.Id = album.AlbumId;
                    albumViewModel.Title = album.Title;
                    albumViewModel.ArtistId = album.Artist.ArtistId;
                    albumViewModel.ArtistName = album.Artist.Name;
                    albumViewModel.ReleaseDate = (DateTime)album.ReleaseDate;
                    try
                    {
                        albumViewModel.Tracks = _db.Tracks.Where(t => t.Album.AlbumId == album.AlbumId).OrderBy(t => t.Title).ToList();
                    }
                    catch (Exception ex)
                    {
                    }

                    AmazonAPIController amz = new AmazonAPIController();
                    List<AmazonProduct> amzList = new List<AmazonProduct>();
                    amzList = amz.GetProducts(album.Title, album.Artist.Name, "album");


                    if (amzList != null)
                    {
                        if (amzList.Count > 0)
                        {
                            ViewBag.AlbumImage = amzList[0].ImageURL;
                            ViewBag.TwitterImage = amzList[0].ImageURL;
                            ViewBag.OGImage = amzList[0].ImageURL;

                            foreach (AmazonProduct product in amzList)
                            {
                                ViewBag.PurchaseLinks += "<p><a href=\"" + product.URL + "\" target=\"_blank\">" + product.Title + "<br /><img src=\"http://www.rhyme4rhyme.com/Images/buy2._V192207737_.gif\" alt=\"buy from amazon.com\" /></a></p>";
                            }
                        }
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
            if (album != null)
            {



                ViewBag.OGTitle = album.Title + " : " + album.Artist.Name;
                ViewBag.OGDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                ViewBag.OGAppID = "1474377432864288";
                ViewBag.MetaAuthor = album.Artist.Name;


                ViewBag.OGURL = "http://www.rhyme4rhyme.com/albums/" + URLClean.Clean(album.Artist.Name) + "/" + URLClean.Clean(album.Title);
                ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                ViewBag.TwitterTitle = "Rhyme 4 Rhyme : " + album.Title + " : " + album.Artist.Name;
                ViewBag.TwitterDescription = album.Title + ", a Hip-Hop album by " + album.Artist.Name;
                ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";
                ViewBag.AlbumImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";
                ViewBag.PurchaseLinks = "";
                albumViewModel.Id = album.AlbumId;
                albumViewModel.Title = album.Title;
                albumViewModel.ArtistId = album.Artist.ArtistId;
                albumViewModel.ArtistName = album.Artist.Name;
                albumViewModel.ReleaseDate = (DateTime)album.ReleaseDate;
                try
                {
                    albumViewModel.Tracks = _db.Tracks.Where(t => t.Album.AlbumId == album.AlbumId).OrderBy(t => t.Title).ToList();
                }
                catch (Exception ex)
                {
                }

                AmazonAPIController amz = new AmazonAPIController();
                List<AmazonProduct> amzList = new List<AmazonProduct>();
                amzList = amz.GetProducts(album.Title, album.Artist.Name, "album");


                if (amzList != null)
                {
                    if (amzList.Count > 0)
                    {
                        ViewBag.AlbumImage = amzList[0].ImageURL;
                        ViewBag.TwitterImage = amzList[0].ImageURL;
                        ViewBag.OGImage = amzList[0].ImageURL;

                        foreach (AmazonProduct product in amzList)
                        {
                            ViewBag.PurchaseLinks += "<p><a href=\"" + product.URL + "\" target=\"_blank\">" + product.Title + "<br /><img src=\"http://www.rhyme4rhyme.com/Images/buy2._V192207737_.gif\" alt=\"buy from amazon.com\" /></a></p>";
                        }
                    }
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
                Artist selectedArtist = null;
                if (_db.Albums.Where(a => a.Title.ToLower() == album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == album.Artist.Name.ToLower()).Count() == 0)
                {
                    selectedArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                    if (selectedArtist != null)
                    {
                        album.Artist = selectedArtist;
                    }
                    else
                    {
                        selectedArtist = new Artist();
                        selectedArtist.Name = album.Artist.Name.Trim();
                        if (User.IsInRole("Admin"))
                        {
                            selectedArtist.Enabled = true;
                        }
                        _db.Artists.Add(selectedArtist);
                        //TODO is this needed
                        _db.SaveChanges();
                        LogUtility.Log(
                            new ChangeLog
                            {
                                Type = "artist",
                                PreviousValues = "ADD - " + selectedArtist.ToString(),
                                UserId = WebSecurity.CurrentUserId
                            });
                        album.Artist = selectedArtist;
                    }

                    if (User.IsInRole("Admin"))
                    {
                        album.Enabled = true;
                    }
                    _db.Albums.Add(album);
                    _db.SaveChanges();

                    LogUtility.Log(
                        new ChangeLog
                        {
                            Type = "album",
                            PreviousValues = "ADD - " + album.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
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
        public ActionResult Edit([Bind(Include = "Id,Enabled,Title,Artist,ReleaseDate")] Album album)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Album";
            ViewBag.MetaDescription = "Hip-Hop album";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, album, record, rap, music";
            if (ModelState.IsValid)
            {
                string temp = Request.Form["previousAlbum"];
                if (_db.Albums.Where(a => a.Title.ToLower() == album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == album.Artist.Name.Trim().ToLower() && a.AlbumId != album.AlbumId).Count() > 0)
                {
                    ModelState.AddModelError("", "The album already exists.");
                    return View(album);
                }
                Artist tempArtist = _db.Artists.FirstOrDefault(artist => artist.Name.ToLower() == album.Artist.Name.Trim().ToLower());
                if (tempArtist == null)
                {
                    tempArtist = new Artist();
                    tempArtist.Name = album.Artist.Name;
                    tempArtist.DateModified = DateTime.Now;
                    if (User.IsInRole("Admin")) {
                        tempArtist.Enabled = true;
                    }
                    _db.Artists.Add(tempArtist);
                    _db.SaveChanges();
                    LogUtility.Log(new ChangeLog {
                        Type = "artist",
                        PreviousValues = album.Artist.ToString(),
                        UserId = WebSecurity.CurrentUserId
                    });
                }
                //TO DO - artist will not update
                album.Artist = tempArtist;
                _db.SetModified(album);
                _db.SaveChanges();
                LogUtility.Log(new ChangeLog {
                    Type="album",
                    PreviousValues = "EDIT - " + Request["previousAlbum"],
                    UserId = WebSecurity.CurrentUserId
                });

                

                return RedirectToAction("Index");


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
            LogUtility.Log(
                new ChangeLog
                {
                    Type = "album",
                    PreviousValues = "DELETE - " + album.ToString(),
                    UserId = WebSecurity.CurrentUserId
                });
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