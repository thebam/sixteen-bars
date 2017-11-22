using System;
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
            ViewBag.MetaDescription = "A list of Hip-Hop artists and rappers linked to more details about their quotes.";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, artist, rapper, rap, music";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var ArtistTrackAlbum = (from artist in _db.Artists
                                    join track in _db.Tracks on artist.ArtistId equals track.Album.Artist.ArtistId into tracks
                                    from artistTrack in tracks.DefaultIfEmpty()
                                    join quote in _db.Quotes on artist.ArtistId equals quote.Artist.ArtistId into quotes
                                    from artistQuote in quotes.DefaultIfEmpty()
                                    
                        select new ArtistIndexViewModel()
                        {
                            Id = artist.ArtistId,
                            Name = artist.Name,
                            Enabled = artist.Enabled,
                            IsDeleteable = (artistTrack == null && artistQuote == null) ? true : false
                        }).Distinct().OrderBy(a => a.Name).ToList();

            if (!User.IsInRole("Admin") && !User.IsInRole("Editor")) {
                ArtistTrackAlbum.RemoveAll(a => a.Enabled == false);
            }

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
            Artist artist = _db.Artists.FirstOrDefault(a=>a.ArtistId == id);
            ArtistDetailViewModel artistVM = new ArtistDetailViewModel();
            if (artist == null)
            {
                return View(artistVM);
            }
            else {

                ViewBag.Title = artist.Name + " : Quotes and Albums";
                ViewBag.MetaDescription = "Explore quotes and albums from hip-hop artist " + artist.Name;
                ViewBag.MetaKeywords = artist.Name + ", artist, rapper, quote,album";

                ViewBag.OGTitle = artist.Name + " : Quotes and Albums";
                ViewBag.OGDescription = "Explore quotes and albums from hip-hop artist " + artist.Name;
                ViewBag.OGAppID = "1474377432864288";
                ViewBag.MetaAuthor = "Rhyme 4 Rhyme";


                ViewBag.OGURL = "http://www.rhyme4rhyme.com/artists/" + URLClean.Clean(artist.Name);
                ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                ViewBag.TwitterTitle = artist.Name + " : Quotes and Albums";
                ViewBag.TwitterDescription = "Explore quotes and albums from hip-hop artist " + artist.Name;
                ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                artistVM.Id = artist.ArtistId;
                artistVM.Name = artist.Name;
                artistVM.Image = artist.Image;
                artistVM.Albums = _db.Albums.Where(a
                    => a.Artist.ArtistId == artist.ArtistId).OrderBy(a => a.Title).ToList();
                if (isExplicit != null)
                {
                    if (isExplicit.Value == "explicit")
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.ArtistId == artist.ArtistId && q.Enabled == true).OrderBy(q => q.Text).ToList();
                    }
                    else
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.ArtistId == artist.ArtistId && q.Explicit == false && q.Enabled == true).OrderBy(q => q.Text).ToList();
                    }
                }
                else {
                    artistVM.Quotes = _db.Quotes.Where(q => q.Artist.ArtistId == artist.ArtistId && q.Explicit == false && q.Enabled == true).OrderBy(q => q.Text).ToList();
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

            Artist artist = _db.Artists.SingleOrDefault(a => a.Name.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").Replace("/", "").Replace("#", "").Replace(" ", "_").ToLower() == artistname.ToLower().Trim());

            ArtistDetailViewModel artistVM = new ArtistDetailViewModel();
            if (artist == null)
            {
                return View(artistVM);
            }
            else
            {
                if (artist.Enabled == true || (artist.Enabled == false && User.IsInRole("Admin") || User.IsInRole("Editor")))
                {
                    ViewBag.Title = artist.Name + " : Quotes and Albums";
                    ViewBag.MetaDescription = "Explore quotes and albums from hip-hop artist " + artist.Name;
                    ViewBag.MetaKeywords = artist.Name + ", artist, rapper, quote,album";

                    ViewBag.OGTitle = artist.Name + " : Quotes and Albums";
                    ViewBag.OGDescription = "Explore quotes and albums from hip-hop artist " + artist.Name;
                    ViewBag.OGAppID = "1474377432864288";
                    ViewBag.MetaAuthor = "Rhyme 4 Rhyme";


                    ViewBag.OGURL = "http://www.rhyme4rhyme.com/artists/" + URLClean.Clean(artist.Name);
                    ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                    ViewBag.TwitterTitle = artist.Name + " : Quotes and Albums";
                    ViewBag.TwitterDescription = "Explore quotes and albums from hip-hop artist " + artist.Name;
                    ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

                    artistVM.Id = artist.ArtistId;
                    artistVM.Name = artist.Name;
                    artistVM.Image = artist.Image;
                    artistVM.Albums = _db.Albums.Where(a => a.Artist.ArtistId == artist.ArtistId).OrderBy(a => a.Title).ToList();
                    if (isExplicit != null)
                    {
                        if (isExplicit.Value == "explicit")
                        {
                            artistVM.Quotes = _db.Quotes.Where(q => q.Artist.ArtistId == artist.ArtistId && q.Enabled == true).OrderBy(q => q.Text).ToList();
                        }
                        else
                        {
                            artistVM.Quotes = _db.Quotes.Where(q => q.Artist.ArtistId == artist.ArtistId && q.Explicit == false && q.Enabled == true).OrderBy(q => q.Text).ToList();
                        }
                    }
                    else
                    {
                        artistVM.Quotes = _db.Quotes.Where(q => q.Artist.ArtistId == artist.ArtistId && q.Explicit == false && q.Enabled == true).OrderBy(q => q.Text).ToList();
                    }
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
                if (_db.Artists.Where(a => a.Name == artist.Name).Count()==0)
                {
                    if (User.IsInRole("Admin")) {
                        artist.Enabled = true;
                    }
                    artist.Name = artist.Name.Trim();
                    _db.Artists.Add(artist);
                    _db.SaveChanges();

                    try
                    {
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "artist",
                            PreviousValues = "ADD - " + artist.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                    }
                    catch (Exception ex) {

                    }
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
                if (_db.Artists.Where(a => a.Name == artist.Name && a.ArtistId != artist.ArtistId).Count()==0)
                {
                    artist.Name = artist.Name.Trim();
                    artist.DateModified = DateTime.Now;
                    _db.SetModified(artist);
                    _db.SaveChanges();
                    try
                    {
                        LogUtility.Log(new ChangeLog {
                            Type = "artist",
                            PreviousValues = "EDIT - " + Request["previousArtist"],
                            UserId = WebSecurity.CurrentUserId
                        });
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
            if (artist != null)
            {
                //make sure that the artist doesn't have any ablums or quotes in the database to avoid any orphaned records.
                Quote quote = _db.Quotes.SingleOrDefault(q => q.Artist.ArtistId == id);
                if (quote == null) {
                    Album album = _db.Albums.SingleOrDefault(a => a.Artist.ArtistId == id);
                    if (album == null)
                    {
                        try
                        {
                            LogUtility.Log(new ChangeLog
                            {
                                Type = "artist",
                                PreviousValues = "DELETE - " + artist.ToString(),
                                UserId = WebSecurity.CurrentUserId
                            });       
                        }
                        catch (Exception ex)
                        {
                            //TO DO handle exception - email change?
                        }
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