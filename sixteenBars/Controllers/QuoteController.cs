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
    public class QuoteController : Controller
    {
        private SixteenBarsDb db = new SixteenBarsDb();

        //
        // GET: /Quote/

        public ActionResult Index()
        {
            return View(db.Quotes.ToList());
        }

        //
        // GET: /Quote/Details/5

        public ActionResult Details(int id = 0)
        {
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        //
        // GET: /Quote/Create

        public ActionResult Create()
        {
            QuoteViewModel quoteVM = new QuoteViewModel();
            var artists = db.Artists.ToList();
            artists.Add(new Artist() { Name = "Add New Artist", Id = -1 });
            quoteVM.Artists = new SelectList(artists, "Id", "Name");
            var tracks = db.Tracks.ToList();
            tracks.Add(new Track() { Title = "Add New Song", Id = -1 });
            quoteVM.Tracks = new SelectList(tracks, "Id", "Title");
            return View(quoteVM);
        }

        //
        // POST: /Quote/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuoteViewModel quoteVM)
        {
            if (ModelState.IsValid)
            {
                Quote quote = new Quote();
                quote.Text = quoteVM.Text;
                if (quoteVM.ArtistId != -1)
                {
                    quote.Artist = db.Artists.Find(quoteVM.ArtistId);
                }
                else {

                    Artist artist = db.Artists.FirstOrDefault(a => a.Name == quoteVM.ArtistName);
                    if (artist == null)
                    {
                        quote.Artist = new Artist() { Name = quoteVM.ArtistName };
                    }
                    else {
                        quote.Artist = artist;
                    }
                }
                if (quoteVM.TrackId != -1)
                {
                    quote.Track = db.Tracks.Find(quoteVM.TrackId);
                }
                else {
                    Track track = db.Tracks.FirstOrDefault(t => t.Title == quoteVM.TrackName);
                    if (track == null)
                    {
                        quote.Track = new Track() { Title = quoteVM.TrackName };
                    }
                    else
                    {
                        quote.Track = track;
                    }
                }
                db.Quotes.Add(quote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quoteVM);
        }

        //
        // GET: /Quote/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Quote quote = db.Quotes.Find(id);
            QuoteViewModel quoteVM = new QuoteViewModel();
            if (quote == null)
            {
                return HttpNotFound();
            }
            else {
                
                quoteVM.Text = quote.Text; 
                quoteVM.TrackId = quote.Track.Id;
                quoteVM.ArtistId = quote.Artist.Id;
                var artists = db.Artists.ToList();
                artists.Add(new Artist() { Name = "Add New Artist", Id = -1 });
                quoteVM.Artists = new SelectList(artists, "Id", "Name",quote.Artist.Id);

                var tracks = db.Tracks.ToList();
                tracks.Add(new Track() { Title = "Add New Song", Id = -1 });
                quoteVM.Tracks = new SelectList(tracks, "Id", "Title",quote.Track.Id);

                
            
            }
            return View(quoteVM);
        }

        //
        // POST: /Quote/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuoteViewModel quoteVM)
        {
            if (ModelState.IsValid)
            {

                Quote quote = db.Quotes.FirstOrDefault(q => q.Id == quoteVM.Id);
                quote.Text = quoteVM.Text;
                if (quoteVM.ArtistId != -1)
                {
                    quote.Artist = db.Artists.Find(quoteVM.ArtistId);
                }
                else
                {

                    Artist artist = db.Artists.FirstOrDefault(a => a.Name == quoteVM.ArtistName);
                    if (artist == null)
                    {
                        quote.Artist = new Artist() { Name = quoteVM.ArtistName };
                    }
                    else
                    {
                        quote.Artist = artist;
                    }
                }
                
                if (quoteVM.TrackId != -1)
                {
                    quote.Track = db.Tracks.Find(quoteVM.TrackId);
                }
                else
                {
                    Track track = db.Tracks.FirstOrDefault(t => t.Title == quoteVM.TrackName);
                    if (track == null)
                    {
                        quote.Track = new Track() { Title = quoteVM.TrackName };
                    }
                    else
                    {
                        quote.Track = track;
                    }
                }

                
               
                
                db.Entry(quote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quoteVM);
        }

        //
        // GET: /Quote/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Quote quote = db.Quotes.Find(id);
            if (quote == null)
            {
                return HttpNotFound();
            }
            return View(quote);
        }

        //
        // POST: /Quote/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quote quote = db.Quotes.Find(id);
            db.Quotes.Remove(quote);
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