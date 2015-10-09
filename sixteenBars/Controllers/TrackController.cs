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
    public class TrackController : Controller
    {
        private ISixteenBarsDb _db;

        public TrackController() : this(new SixteenBarsDb()) { }

        public TrackController(ISixteenBarsDb db) {
            this._db = db;
        }

        //
        // GET: /Track/

        public ActionResult Index(int? page)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Tracks/Songs";
            ViewBag.MetaDescription = "List of Hip-Hop tracks/songs";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
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


            return View(tracks.ToPagedList(pageNumber, pageSize));
        }

        
        public ActionResult Details(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            Track track = _db.Tracks.Find(id);
            if (track != null)
            {

                ViewBag.Title = "Rhyme 4 Rhyme : " + track.Title +" : " +track.Album.Title;
                ViewBag.MetaDescription = track.Title + " a Hip-Hop song from " + track.Album.Artist.Name + " off of the album " + track.Album.Title;
                ViewBag.MetaKeywords = track.Title +", "+ track.Album.Artist.Name +", "+ track.Album.Title + ", hip-hop, hip hop, artist, rapper, rap, music, quote, album";
            }
            return View(track);
        }


        public ActionResult TitleTitleDetails(string albumtitle, string tracktitle)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            Track track = _db.Tracks.SingleOrDefault(t => t.Album.Title.ToLower() == albumtitle.ToLower().Trim() && t.Title.ToLower() == tracktitle.ToLower().Trim());
            if (track != null)
            {

                ViewBag.Title = "Rhyme 4 Rhyme : " + track.Title + " : " + track.Album.Title;
                ViewBag.MetaDescription = track.Title + " a Hip-Hop song from " + track.Album.Artist.Name + " off of the album " + track.Album.Title;
                ViewBag.MetaKeywords = track.Title + ", " + track.Album.Artist.Name + ", " + track.Album.Title + ", hip-hop, hip hop, artist, rapper, rap, music, quote, album";
            }
            return View("details", track);
        }


        //
        // GET: /Track/Create
        [Authorize(Roles="Admin,editor")]
        public ActionResult Create()
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            return View();
        }

        //
        // POST: /Track/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,editor")]
        public ActionResult Create(TrackViewModel trackVM)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            if (ModelState.IsValid)
            {
                Artist artist = _db.Artists.FirstOrDefault(a => a.Name.ToLower() == trackVM.ArtistName.Trim().ToLower());
                if (artist == null)
                {
                    _db.Artists.Add(new Artist() { Name = trackVM.ArtistName.Trim(), DateModified = DateTime.Now });
                    _db.SaveChanges();
                    artist = _db.Artists.FirstOrDefault(a => a.Name.ToLower() == trackVM.ArtistName.Trim().ToLower());
                }

                Album album = _db.Albums.FirstOrDefault(a => a.Title.ToLower() == trackVM.AlbumTitle.Trim().ToLower());
                if (album == null)
                {
                    _db.Albums.Add(new Album() { 
                        Title = trackVM.AlbumTitle.Trim(), 
                        Artist = artist,
                        ReleaseDate = trackVM.ReleaseDate,
                        DateModified = DateTime.Now 
                    });
                    _db.SaveChanges();
                    album = _db.Albums.FirstOrDefault(a => a.Title.ToLower() == trackVM.AlbumTitle.Trim().ToLower());
                }

                Track track = new Track();
                track.Title = trackVM.Title;
                track.ReleaseDate = trackVM.ReleaseDate;
                track.Album = album;

                TrackAPIController api = new TrackAPIController(_db);

                if (!api.TrackExists(track.Title, track.Album.Title, track.Album.Artist.Name, (DateTime)track.ReleaseDate))
                {
                    _db.Tracks.Add(track);
                    _db.SaveChanges();
                }
                else {

                   
                    ViewBag.ErrorMessage = "The track titled '" + track.Title + "' on the album titled '" + track.Album.Title + "' already exists.";
                    return View(trackVM);
                }

                return RedirectToAction("Index");
            }

            return View(trackVM);
        }

        //
        // GET: /Track/Edit/5
        [Authorize(Roles = "Admin,editor")]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
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
        [Authorize(Roles = "Admin,editor")]
        public ActionResult Edit(Track track)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            if (ModelState.IsValid)
            {
                TrackAPIController api = new TrackAPIController(_db);
                if (!api.TrackExists(track.Title, track.Album.Title, track.Album.Artist.Name,(DateTime)track.ReleaseDate)){
                    Track edittedTrack = _db.Tracks.Find(track.Id);
                    Track previousTrack = edittedTrack;
                    edittedTrack.Title = track.Title;
                    edittedTrack.ReleaseDate = track.ReleaseDate;
                    Album tempAlbum = _db.Albums.FirstOrDefault(album => album.Title.ToLower() == track.Album.Title.Trim().ToLower() && album.Artist.Name.ToLower() == track.Album.Artist.Name.Trim().ToLower());
                    Artist tempArtist = _db.Artists.FirstOrDefault(artist => artist.Name.ToLower() == track.Album.Artist.Name.Trim().ToLower());
                    if (tempAlbum == null)
                    {
                        if (tempArtist == null)
                        {
                            edittedTrack.Album = new Album()
                            {
                                Title = track.Album.Title.Trim(),
                                ReleaseDate = track.ReleaseDate,
                                DateModified = DateTime.Now,
                                Artist = new Artist()
                                {
                                    Name = track.Album.Artist.Name.Trim(),
                                    DateModified = DateTime.Now
                                }
                            };
                        }
                        else
                        {
                            edittedTrack.Album = new Album()
                            {
                                Title = track.Album.Title.Trim(),
                                ReleaseDate = track.ReleaseDate,
                                DateModified = DateTime.Now,
                                Artist = tempArtist
                            };
                        }
                    }
                    else {
                        edittedTrack.Album = tempAlbum;
                    }


                    _db.SetModified(edittedTrack);
                    _db.SaveChanges();

                    try
                    {
                        ChangeLog log = new ChangeLog();
                        log.Type = "track";
                        log.PreviousValues = new JavaScriptSerializer().Serialize(previousTrack);
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
                else
                {
                    ViewBag.ErrorMessage = "The track titled '" + track.Title + "' on the album titled '" + track.Album.Title + "' already exists.";
                    return View(track);
                }
               
            }
            return View(track);
        }

        //
        // GET: /Track/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            Track track = _db.Tracks.Find(id);
            
            return View(track);
        }

        //
        // POST: /Track/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Track/Song";
            ViewBag.MetaDescription = "Hip-Hop track/song";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, track, song, rap, music";
            Track track = _db.Tracks.Find(id);
            Track previousTrack = track;
            _db.Tracks.Remove(track);
            _db.SaveChanges();


            try
            {
                ChangeLog log = new ChangeLog();
                log.Type = "track";
                log.PreviousValues = new JavaScriptSerializer().Serialize(previousTrack);
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