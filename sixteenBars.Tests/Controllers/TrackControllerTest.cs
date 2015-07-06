using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sixteenBars.Tests.Model;
using sixteenBars.Controllers;
using sixteenBars.Models;
using sixteenBars.Library;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace sixteenBars.Tests.Controllers
{
    [TestClass]
    public class TrackControllerTest
    {
        [TestMethod]
        public void Track_Index() {
            ISixteenBarsDb mockDb = new MockSixteenBarsDb();
            TrackController ctrl = new TrackController(mockDb);

            var result = ctrl.Index() as ViewResult;
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<TrackIndexViewModel>));
            var tracks = (List<TrackIndexViewModel>)result.ViewData.Model;
            Assert.AreEqual(5, tracks.Count, "More than or less than 5 tracks.");

            Assert.AreEqual("Bandz A Make Her Dance", tracks[0].Title, "Track title not 'Bandz A Make Her Dance'");
            Assert.AreEqual(true, tracks[0].IsDeleteable, "Track title not 'Bandz A Make Her Dance' is not deleteable");
            Assert.AreEqual("IV. Sweatpants", tracks[1].Title, "Track title not 'IV. Sweatpants'");
            Assert.AreEqual(false, tracks[1].IsDeleteable, "Track title not 'IV. Sweatpants' is not deleteable");
            Assert.AreEqual("King Sh*t", tracks[2].Title, "Track title not 'King Sh*t'");
            Assert.AreEqual(true, tracks[2].IsDeleteable, "Track title not 'King Sh*t' is deleteable");
            Assert.AreEqual("Light Up", tracks[3].Title, "Track title not 'Light Up'");
            Assert.AreEqual(false, tracks[3].IsDeleteable, "Track title not 'Light Up' is not deleteable");
            Assert.AreEqual("Wetter Than Tsunami", tracks[4].Title, "Track title not 'Wetter Than Tsunami'");
            Assert.AreEqual(true, tracks[4].IsDeleteable, "Track title not 'Wetter Than Tsunami' is deleteable");
        }
        [TestMethod]
        public void Track_Create()
        {
            MockSixteenBarsDb mockDb = new MockSixteenBarsDb();
            TrackController ctrl = new TrackController(mockDb);

            TrackViewModel newTrack = new TrackViewModel()
            {
                Title = "Shabba",
                AlbumName = "Trap Lord",
                ArtistName = "A$AP Ferg",    
                ReleaseDate = new DateTime(2013, 8, 20)
            };


            ctrl.Create(newTrack);

            Track foundTrack = mockDb.Tracks.SingleOrDefault(t => t.Title == newTrack.Title);
            Assert.AreEqual(newTrack.Title, foundTrack.Title, "Title not Shabba");
            Assert.AreEqual(newTrack.AlbumName, foundTrack.Album.Title, "Name not Trap Lord");
            Assert.AreEqual(newTrack.ArtistName, foundTrack.Album.Artist.Name, "Name not A$AP Ferg");

            newTrack = new TrackViewModel()
            {
                Title = "She Don't Want A Man",
                ArtistName = "Curren$y",
                AlbumName = "Weekend At Burnie's",
                ReleaseDate = new DateTime(2011,6,28)
            };


            ctrl.Create(newTrack);

            foundTrack = mockDb.Tracks.SingleOrDefault(t => t.Title == newTrack.Title);
            Int32 cntArtists = mockDb.Artists.Where(a => a.Name == newTrack.ArtistName.Trim()).Count();
            Assert.AreEqual(newTrack.Title, foundTrack.Title, "Title not She Don't Want A Man");
            Assert.AreEqual(newTrack.AlbumName, foundTrack.Album.Title, "Name not Weekend At Burnie's");
            Assert.AreEqual(newTrack.ArtistName, foundTrack.Album.Artist.Name, "Name not Curren$y");
            Assert.AreEqual(1, cntArtists, "Artist Currenc$y was duplicated.");


            newTrack = new TrackViewModel()
            {
                Title = "I.Crawl",
                ArtistName = "Childish Gambino",
                AlbumName = "Because The Internet",
                ReleaseDate = new DateTime(2014, 3, 1)
            };
            ctrl.Create(newTrack);

            foundTrack = mockDb.Tracks.SingleOrDefault(t => t.Title == newTrack.Title);
            cntArtists = mockDb.Artists.Where(a => a.Name == newTrack.ArtistName.Trim()).Count();
            Int32 cntAlbums = mockDb.Albums.Where(a => a.Title == newTrack.AlbumName.Trim()).Count();
            Assert.AreEqual(newTrack.Title, foundTrack.Title, "Title not I.Crawl");
            Assert.AreEqual(newTrack.AlbumName, foundTrack.Album.Title, "Name not Because The Internet");
            Assert.AreEqual(newTrack.ArtistName, foundTrack.Album.Artist.Name, "Name not Childish Gambino");
            Assert.AreEqual(1, cntArtists, "Artist Childish Gambino was duplicated.");
            Assert.AreEqual(1, cntAlbums, "Album Because The Internet was duplicated.");
        }
    }
}
