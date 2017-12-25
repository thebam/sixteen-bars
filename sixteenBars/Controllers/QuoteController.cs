using PagedList;
using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace sixteenBars.Controllers
{
    public class QuoteController : Controller
    {
        private ISixteenBarsDb _db;

        public QuoteController() :this(new SixteenBarsDb()){ 
        
        }

        public QuoteController(ISixteenBarsDb db) {
            this._db = db;
        }

        [ChildActionOnly]
        public List<Quote> RandomQuotes(Boolean allowExplicit = false, Int32 numberOfResults = 1)
        {
            List<Quote> quotes = null;
            List<Quote> distinctArtistQuotes = new List<Quote>();
                List<Quote> eligibleQuotes = new List<Quote>();
            if (_db.Quotes.Count() > 0)
            {
                if (allowExplicit)
                {
                    //TODO - check if artist, album artist, track, and album are enabled
                    eligibleQuotes = _db.Quotes.Where(q=>q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && (q.Artist.Image != null || q.Track.Album.Image !=null) ).OrderBy(q => Guid.NewGuid()).Take(numberOfResults).ToList();

                    
                }
                else {
                    //TODO - check if artist, album artist, track, and album are enabled
                    eligibleQuotes = _db.Quotes.Where(q => q.Explicit == false && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && (q.Artist.Image != null || q.Track.Album.Image != null)).OrderBy(q => Guid.NewGuid()).ToList();
                    
                }
                distinctArtistQuotes = eligibleQuotes.GroupBy(x => x.Artist.ArtistId).Select(y => y.First()).ToList();
                quotes = distinctArtistQuotes;
                foreach (Quote quote in quotes)
                {
                    quote.FormattedText = WordLink.CreateLinks(quote.FormattedText);    
                }
            }
            return quotes;
        }


        //
        // GET: /Quote/

        public ActionResult Index(int? page)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Quotes";
            ViewBag.MetaDescription = "List of Hip-Hop quotes";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            ViewBag.MetaAuthor = "Rhyme 4 Rhyme";

            ViewBag.OGTitle = "Rap/Hip Hop Quotes";
            ViewBag.OGDescription = "List of Rap/Hip-Hop quotes from various artists.";
            ViewBag.OGURL = "http://www.rhyme4rhyme.com/quote";
            ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

            ViewBag.OGAppID = "1474377432864288";

            ViewBag.TwitterTitle = "Rap/Hip Hop Quotes";
            ViewBag.TwitterDescription = "List of Rap/Hip-Hop quotes from various artists.";
            ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            HttpCookie isExplicit = Request.Cookies["explicit"];
            List<Quote> quotes = new List<Quote>();
            if (isExplicit != null)
            {
                if (isExplicit.Value == "explicit")
                {
                    quotes = _db.Quotes.OrderByDescending(q => q.DateCreated).ToList();
                }
                else
                {
                    quotes = _db.Quotes.Where(q => q.Explicit == false).OrderByDescending(q => q.DateCreated).ToList();
                }
            }
            else {
                quotes = _db.Quotes.Where(q => q.Explicit == false).OrderByDescending(q => q.DateCreated).ToList();
            }
            if (!User.IsInRole("Admin") && !User.IsInRole("Editor"))
            {
                quotes.RemoveAll(q => q.Enabled == false || q.Track.Enabled == false || q.Track.Album.Enabled == false || q.Artist.Enabled == false || q.Track.Album.Artist.Enabled ==false);
            }
            return View(quotes.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Quote/Details/5

        public ActionResult Details(uint id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            ViewBag.MetaAuthor = "Rhyme 4 Rhyme";

            ViewBag.OGTitle = "Rap/Hip Hop Quotes";
            ViewBag.OGDescription = "List of Rap/Hip-Hop quotes from various artists.";
            ViewBag.OGURL = "http://www.rhyme4rhyme.com/quote";
            ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

            ViewBag.OGAppID = "1474377432864288";

            ViewBag.TwitterTitle = "Rap/Hip Hop Quotes";
            ViewBag.TwitterDescription = "List of Rap/Hip-Hop quotes from various artists.";
            ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

            Quote quote = _db.Quotes.Find(id);
            QuoteViewModel quoteVM = new QuoteViewModel();
            if (quote != null)
            {
                ViewBag.Title = "Rhyme 4 Rhyme : " + quote.Artist.Name + " : " + quote.Text;
                ViewBag.MetaDescription = quote.Text + " - Hip-Hop quote said by " + quote.Artist.Name + " on track " + quote.Track.Title;
                ViewBag.MetaKeywords = quote.Text + ", " + quote.Artist.Name + ", " + quote.Track.Title + ",Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";


                ViewBag.OGTitle = quote.Artist.Name + " quote";
                ViewBag.OGDescription = "\"" + quote.Text + "\" from track " + quote.Track.Title + " on album " + quote.Track.Album.Title;
                ViewBag.OGAppID = "1474377432864288";
                ViewBag.MetaAuthor = quote.Artist.Name;


                ViewBag.OGURL = "http://www.rhyme4rhyme.com/quotes/" + URLClean.Clean(quote.Artist.Name) + "/" + URLClean.Clean(quote.Text);
                ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                ViewBag.TwitterTitle = "Quote from " + quote.Artist.Name;
                ViewBag.TwitterDescription = quote.Text + " from track " + quote.Track.Title;
                ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

                ViewBag.AlbumImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";
                ViewBag.PurchaseLinks = "";

                quoteVM.Id = quote.QuoteId;
                quoteVM.Text = WordLink.CreateLinks(quote.FormattedText);
                
                quoteVM.Explanation = quote.Explanation;
                quoteVM.ArtistName = quote.Artist.Name;
                quoteVM.ArtistId = quote.Artist.ArtistId;
                quoteVM.TrackName = quote.Track.Title;
                quoteVM.TrackId = quote.Track.TrackId;
                quoteVM.AlbumName = quote.Track.Album.Title;
                quoteVM.AlbumId = quote.Track.Album.AlbumId;
                quoteVM.AlbumArtistName = quote.Track.Album.Artist.Name;
                quoteVM.AlbumArtistId = quote.Track.Album.Artist.ArtistId;
                quoteVM.Timestamp = quote.Timestamp;
                quoteVM.VideoCopyright = quote.Track.VideoCopyright;
                quoteVM.ImageCopyright = quote.Track.Album.ImageCopyright;

            }
            return View(quoteVM);
        }


        public ActionResult NameQuoteDetails(string speakername, string quotetext)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.SingleOrDefault(q => q.Artist.Name.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").Replace("/", "").Replace("#", "").Replace(" ", "_").ToLower() == speakername.ToLower().Trim() && q.Text.Replace(".", "").Replace(",", "").Replace("&", "").Replace("?", "").Replace("%", "").Replace("!", "").Replace("*", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("\\", "").Replace(";", "").Replace("/", "").Replace("#", "").Replace(" ", "_").ToLower().StartsWith(quotetext.Trim()));
            QuoteViewModel quoteVM = new QuoteViewModel();
            if (quote != null)
            {
                ViewBag.Title = "Rhyme 4 Rhyme : " + quote.Artist.Name + " : " + quote.Text;
                ViewBag.MetaDescription = quote.Text + " - Hip-Hop quote said by " + quote.Artist.Name + " on track " + quote.Track.Title;
                ViewBag.MetaKeywords = quote.Text + ", " + quote.Artist.Name + ", " + quote.Track.Title + ",Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";


                ViewBag.OGTitle = quote.Artist.Name + " quote";
                ViewBag.OGDescription = "\"" + quote.Text + "\" from track " + quote.Track.Title + " on album " + quote.Track.Album.Title;
                ViewBag.OGAppID = "1474377432864288";
                ViewBag.MetaAuthor = quote.Artist.Name;


                ViewBag.OGURL = "http://www.rhyme4rhyme.com/quotes/" + HttpUtility.UrlDecode(URLClean.Clean(quote.Artist.Name)) + "/" + HttpUtility.UrlDecode(URLClean.Clean(quote.Text));
                ViewBag.OGImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";



                ViewBag.TwitterTitle = "Quote from " + quote.Artist.Name;
                ViewBag.TwitterDescription =  quote.Text + " from track " + quote.Track.Title;
                ViewBag.TwitterImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";

                ViewBag.AlbumImage = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";
                ViewBag.PurchaseLinks = "";


                quoteVM.Id = quote.QuoteId;
                quoteVM.Text = LanguageFilter.Filter(WordLink.CreateLinks(quote.FormattedText));

                quoteVM.Explanation = quote.Explanation;
                quoteVM.ArtistName = quote.Artist.Name;
                quoteVM.ArtistId = quote.Artist.ArtistId;
                quoteVM.TrackName = quote.Track.Title;
                quoteVM.TrackId = quote.Track.TrackId;
                quoteVM.AlbumName = quote.Track.Album.Title;
                quoteVM.AlbumId = quote.Track.Album.AlbumId;
                quoteVM.AlbumArtistName = quote.Track.Album.Artist.Name;
                quoteVM.AlbumArtistId = quote.Track.Album.Artist.ArtistId;
                if (string.IsNullOrWhiteSpace(quote.Track.Video))
                {
                    
                    if (!string.IsNullOrWhiteSpace(quote.Artist.Image))
                    {
                        quoteVM.Image = quote.Artist.Image;
                    }
                    else if(!string.IsNullOrWhiteSpace(quote.Track.Album.Image))
                    {
                        quoteVM.Image = quote.Track.Album.Image;
                    }
                    else
                    {
                        quoteVM.Image = "http://www.rhyme4rhyme.com/Images/rhyme-4-rhyme-logo.png";
                    }
                }
                else
                {
                    quoteVM.Video = quote.Track.Video;
                }
                quoteVM.Timestamp = quote.Timestamp;

                
            }
            return View("details",quoteVM);
        }

        //
        // GET: /Quote/Create
        [Authorize(Roles="admin,editor")]
        public ActionResult Create()
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = new Quote();
            return View(quote);
        }

        //
        // POST: /Quote/Create

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Create(Quote quote)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Create Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            if (ModelState.IsValid)
            {
                quote.FormattedText = quote.Text;
                quote.Text = quote.Text.Replace("<br/>", " ").Replace("<br />", " ").Replace("\r", " ").Replace("\n", " ");
                quote.Text = quote.Text.Replace("  ", " ");
                if (_db.Quotes.Where(q => q.Text == quote.Text.Trim() && q.Artist.Name == quote.Artist.Name.Trim()).Count() == 0)
                {
                    
                    if (User.IsInRole("admin"))
                    {
                        quote.Enabled = true;
                    }
                    Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                    if (tempArtist == null)
                    {
                        tempArtist = new Artist();
                        tempArtist.Name = quote.Artist.Name.Trim();
                        if (User.IsInRole("admin"))
                        {
                            tempArtist.Enabled = true;
                        }
                        _db.Artists.Add(tempArtist);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "artist",
                            PreviousValues = "ADD - " + tempArtist.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                        quote.Artist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                    }
                    else
                    {
                        quote.Artist = tempArtist;
                    }

                    Artist tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    if (tempArtistAlbum == null)
                    {
                        tempArtistAlbum = new Artist();
                        tempArtistAlbum.Name = quote.Track.Album.Artist.Name.Trim();
                        if (User.IsInRole("admin"))
                        {
                            tempArtistAlbum.Enabled = true;
                        }
                        _db.Artists.Add(tempArtistAlbum);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "artist",
                            PreviousValues = "ADD - " + tempArtistAlbum.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                        tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    }

                    Album tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    if (tempAlbum == null)
                    {
                        tempAlbum = new Album();
                        tempAlbum.Title = quote.Track.Album.Title.Trim();
                        tempAlbum.Artist = tempArtistAlbum;
                        tempAlbum.ReleaseDate = quote.Track.Album.ReleaseDate;
                        if (User.IsInRole("admin"))
                        {
                            tempAlbum.Enabled = true;
                        }
                        _db.Albums.Add(tempAlbum);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "album",
                            PreviousValues = "ADD - " + tempAlbum.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                        tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    }

                    Track tempTrack = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                    if (tempTrack == null)
                    {
                        tempTrack = new Track();
                        tempTrack.Title = quote.Track.Title.Trim();
                        tempTrack.ReleaseDate = quote.Track.Album.ReleaseDate;
                        tempTrack.Album = tempAlbum;
                        if (User.IsInRole("admin"))
                        {
                            tempTrack.Enabled = true;
                        }
                        _db.Tracks.Add(tempTrack);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "track",
                            PreviousValues = "ADD - " + tempTrack.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                        quote.Track = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                    }
                    else
                    {
                        quote.Track = tempTrack;
                    }


                    _db.Quotes.Add(quote);
                    _db.SaveChanges();
                    LogUtility.Log(new ChangeLog
                    {
                        Type = "quote",
                        PreviousValues = "ADD - " + quote.ToString(),
                        UserId = WebSecurity.CurrentUserId
                    });
                    return RedirectToAction("Index");
                
            }
                else
                {
                    ViewBag.ErrorMessage = "The quote '" + quote.Text + "' said by '" + quote.Artist.Name + "' already exists.";
                    return View(quote);
                }
            }
            else {
                return View(quote);
            }
        }

        //
        // GET: /Quote/Edit/5
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            return View(quote);
        }

        //
        // POST: /Quote/Edit/5

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,editor")]
        public ActionResult Edit(Quote quote)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Edit Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            if (ModelState.IsValid)
            {
                int matchingQuotes = (from item in _db.Quotes
                           where item.Text == quote.Text.Trim() && item.Artist.Name == quote.Artist.Name.Trim() && item.QuoteId != quote.QuoteId
                           select item).Count();
                //SELECT * quotes where text == text and quoteid != quoteid
                //from that list see if any have quote.artist.name = artistname

                if (matchingQuotes == 0)
                {
                    quote.FormattedText = quote.Text;
                    quote.Text = quote.Text.Replace("<br/>", " ").Replace("<br />", " ").Replace("\r", " ").Replace("\n", " ");
                    quote.Text = quote.Text.Replace("  ", " ");
                    Artist tempArtist = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Artist.Name.Trim().ToLower());
                    if (tempArtist == null)
                    {
                        tempArtist = new Artist();
                        tempArtist.Name = quote.Artist.Name.Trim();
                        if (User.IsInRole("admin"))
                        {
                            tempArtist.Enabled = true;
                        }
                        _db.Artists.Add(tempArtist);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "artist",
                            PreviousValues = "ADD - " + tempArtist.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                    }
                    quote.ArtistId = tempArtist.ArtistId;
                    quote.Artist = tempArtist;


                    Artist tempArtistAlbum = _db.Artists.SingleOrDefault(a => a.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    if (tempArtistAlbum == null)
                    {
                        tempArtistAlbum = new Artist();
                        tempArtistAlbum.Name = quote.Track.Album.Artist.Name.Trim();
                        if (User.IsInRole("admin"))
                        {
                            tempArtistAlbum.Enabled = true;
                        }
                        _db.Artists.Add(tempArtistAlbum);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "artist",
                            PreviousValues = "ADD - " + tempArtistAlbum.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                    }
                    quote.Track.Album.ArtistId = tempArtistAlbum.ArtistId;
                    quote.Track.Album.Artist = tempArtistAlbum;


                    Album tempAlbum = _db.Albums.SingleOrDefault(a => a.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower() && a.Artist.Name.ToLower() == quote.Track.Album.Artist.Name.Trim().ToLower());
                    if (tempAlbum == null)
                    {
                        tempAlbum = new Album();
                        tempAlbum.Title = quote.Track.Album.Title.Trim();
                        tempAlbum.Artist = tempArtistAlbum;
                        tempAlbum.ReleaseDate = quote.Track.Album.ReleaseDate;
                        if (User.IsInRole("admin"))
                        {
                            tempAlbum.Enabled = true;
                        }
                        _db.Albums.Add(tempAlbum);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "album",
                            PreviousValues = "ADD - " + tempAlbum.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                    }
                    quote.Track.Album = tempAlbum;



                    Track tempTrack = _db.Tracks.SingleOrDefault(t => t.Title.ToLower() == quote.Track.Title.Trim().ToLower() && t.Album.Title.ToLower() == quote.Track.Album.Title.Trim().ToLower());
                    if (tempTrack == null)
                    {
                        tempTrack = new Track();
                        tempTrack.Title = quote.Track.Title.Trim();
                        tempTrack.ReleaseDate = quote.Track.Album.ReleaseDate;
                        tempTrack.Album = tempAlbum;
                        if (User.IsInRole("admin"))
                        {
                            tempTrack.Enabled = true;
                        }
                        _db.Tracks.Add(tempTrack);
                        _db.SaveChanges();
                        LogUtility.Log(new ChangeLog
                        {
                            Type = "track",
                            PreviousValues = "ADD - " + tempTrack.ToString(),
                            UserId = WebSecurity.CurrentUserId
                        });
                    }
                    quote.TrackId = tempTrack.TrackId;
                    quote.Track = tempTrack;

                    _db.SetModified(quote);
                    _db.SaveChanges();

                    LogUtility.Log(new ChangeLog
                    {
                        Type = "quote",
                        PreviousValues = "EDIT - " + Request.Form["previousQuote"],
                        UserId = WebSecurity.CurrentUserId
                    });

                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "The quote '" + quote.Text + "' said by '" + quote.Artist.Name + "' already exists.";
                    return View(quote);
                }

            }
            else {
                ViewBag.ErrorMessage = "The quote already exists.";
                return View(quote);
            }
        }

        //
        // GET: /Quote/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id = 0)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            QuoteViewModel quoteVM = new QuoteViewModel();

            if (quote == null)
            {
                
            } else {
                quoteVM.Id = quote.QuoteId;
                quoteVM.Text = quote.Text;
                quoteVM.ArtistName = quote.Artist.Name;
                quoteVM.TrackName = quote.Track.Title;
            }
            return View(quoteVM);
        }

        //
        // POST: /Quote/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Rhyme 4 Rhyme : Delete Quote";
            ViewBag.MetaDescription = "Hip-Hop quote";
            ViewBag.MetaKeywords = "Hip-Hop, hip hop, quote, lyric, rhyme, line, rap, music";
            Quote quote = _db.Quotes.Find(id);
            LogUtility.Log(new ChangeLog {
                Type = "quote",
                PreviousValues = "DELETE - " + quote.ToString(),
                UserId = WebSecurity.CurrentUserId
            });
            _db.Quotes.Remove(quote);
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