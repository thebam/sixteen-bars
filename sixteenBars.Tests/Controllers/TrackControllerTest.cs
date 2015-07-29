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
                AlbumTitle = "Trap Lord",
                ArtistName = "A$AP Ferg",    
                ReleaseDate = new DateTime(2013, 8, 20)
            };


            ctrl.Create(newTrack);

            Track foundTrack = mockDb.Tracks.SingleOrDefault(t => t.Title == newTrack.Title);
            Assert.AreEqual(newTrack.Title, foundTrack.Title, "Title not Shabba");
            Assert.AreEqual(newTrack.AlbumTitle, foundTrack.Album.Title, "Name not Trap Lord");
            Assert.AreEqual(newTrack.ArtistName, foundTrack.Album.Artist.Name, "Name not A$AP Ferg");

            newTrack = new TrackViewModel()
            {
                Title = "She Don't Want A Man",
                ArtistName = "Curren$y",
                AlbumTitle = "Weekend At Burnie's",
                ReleaseDate = new DateTime(2011,6,28)
            };


            ctrl.Create(newTrack);

            foundTrack = mockDb.Tracks.SingleOrDefault(t => t.Title == newTrack.Title);
            Int32 cntArtists = mockDb.Artists.Where(a => a.Name == newTrack.ArtistName.Trim()).Count();
            Assert.AreEqual(newTrack.Title, foundTrack.Title, "Title not She Don't Want A Man");
            Assert.AreEqual(newTrack.AlbumTitle, foundTrack.Album.Title, "Name not Weekend At Burnie's");
            Assert.AreEqual(newTrack.ArtistName, foundTrack.Album.Artist.Name, "Name not Curren$y");
            Assert.AreEqual(1, cntArtists, "Artist Currenc$y was duplicated.");


            newTrack = new TrackViewModel()
            {
                Title = "I.Crawl",
                ArtistName = "Childish Gambino",
                AlbumTitle = "Because The Internet",
                ReleaseDate = new DateTime(2014, 3, 1)
            };
            ctrl.Create(newTrack);

            foundTrack = mockDb.Tracks.SingleOrDefault(t => t.Title == newTrack.Title);
            cntArtists = mockDb.Artists.Where(a => a.Name == newTrack.ArtistName.Trim()).Count();
            Int32 cntAlbums = mockDb.Albums.Where(a => a.Title == newTrack.AlbumTitle.Trim()).Count();
            Assert.AreEqual(newTrack.Title, foundTrack.Title, "Title not I.Crawl");
            Assert.AreEqual(newTrack.AlbumTitle, foundTrack.Album.Title, "Name not Because The Internet");
            Assert.AreEqual(newTrack.ArtistName, foundTrack.Album.Artist.Name, "Name not Childish Gambino");
            Assert.AreEqual(1, cntArtists, "Artist Childish Gambino was duplicated.");
            Assert.AreEqual(1, cntAlbums, "Album Because The Internet was duplicated.");
        }
        [TestMethod]
        public void Track_Edit() {

            MockSixteenBarsDb db = new MockSixteenBarsDb();

            TrackController ctrl = new TrackController(db);
            
            
            Track editted = db.Tracks.Find(1);
            Assert.AreEqual(editted.Id, 1, "Track Id not 1");
            Assert.AreEqual(editted.Title, "IV. Sweatpants", "Track title not 'IV. Sweatpants'");


            String newTitle= "Push It",newArtist="Salt-n-Pepa",newAlbumTitle="Hot, Cool & Vicious";
            DateTime newDate = new DateTime(1992,12,8);

            editted.Title = newTitle;
            editted.Album.Artist.Name = newArtist;
            editted.Album.Title = newAlbumTitle;
            editted.ReleaseDate = newDate;
            ctrl.Edit(editted);

            Track actual = db.Tracks.Find(1);

            Assert.AreEqual(actual.Id, 1, "Track Id not 1");
            Assert.AreEqual(actual.Title, newTitle, "Track title not 'Push It'");
            Assert.AreEqual(actual.Album.Artist.Name, newArtist, "Track artist not 'Salt-n-Pepa'");
            Assert.AreEqual(actual.Album.Title, newAlbumTitle, "Track album title not 'Hot, Cool & Vicious'");
            Assert.AreEqual(actual.ReleaseDate, newDate, "Track release date not '12/8/1992'");



            

            editted = db.Tracks.Find(2);
            Assert.AreEqual(editted.Id, 2, "Track Id not 2");
            Assert.AreEqual(editted.Title, "Light Up", "Track title not 'Light Up'");


            newTitle = "How to be the Man"; newArtist = "Riff Raff"; newAlbumTitle = "Neon Icon";
            newDate = new DateTime(2014, 5, 1);

            
            editted.Album.Artist.Name = newArtist;
            editted.Album.Title = newAlbumTitle;
            editted.ReleaseDate = newDate;
            ctrl.Edit(editted);

            actual = db.Tracks.Find(2);

            Assert.AreEqual(actual.Id, 2, "Track Id not 2");
            Assert.AreEqual(actual.Title, editted.Title, "Track title not 'Light Up'");
            Assert.AreEqual(actual.Album.Artist.Name, newArtist, "Track artist not 'Riff Raff'");
            Assert.AreEqual(actual.Album.Title, newAlbumTitle, "Track album title not 'Neon Icon'");
            Assert.AreEqual(actual.ReleaseDate, newDate, "Track release date not '5/1/2014'");


            //Id = 5,
            //        Title = "Bandz A Make Her Dance",
            //        Album = new Album()
            //        {
            //            Id = 6,
            //            Title = "Stay Trippy",
            //            ReleaseDate = new DateTime(2013, 8, 27),
            //            Artist = new Artist()
            //            {
            //                Id = 14,
            //                Name = "Juicy J",
            editted = db.Tracks.Find(5);
            Assert.AreEqual(editted.Id, 5, "Track Id not 5");
            Assert.AreEqual(editted.Title, "Bandz A Make Her Dance", "Track title not 'Bandz A Make Her Dance'");


            newTitle = "How to be the Man"; newArtist = "Riff Raff"; newAlbumTitle = "Neon Icon";
            newDate = new DateTime(2014, 5, 1);


            editted.Album.Artist.Name = newArtist;
            ctrl.Edit(editted);

            actual = db.Tracks.Find(5);

            Assert.AreEqual(actual.Id, 5, "Track Id not 5");
            Assert.AreEqual(actual.Title, editted.Title, "Track title not 'Bandz A Make Her Dance'");
            Assert.AreEqual(actual.Album.Artist.Name, newArtist, "Track artist not 'Riff Raff'");
            Assert.AreEqual(actual.Album.Title, editted.Album.Title, "Track album title not 'Stay Trippy'");
            Assert.AreEqual(actual.ReleaseDate, editted.ReleaseDate, "Track release date not '8/27/2013'");

             //Id = 4,
             //       Title = "King Sh*t",
             //       Album = new Album()
             //       {
             //           Id = 5,
             //           Title = "I am",
             //           ReleaseDate = new DateTime(2013, 11, 19),
             //           Artist = new Artist()
             //           {
             //               Id = 12,
             //               Name = "Yo Gotti",
             //               DateModified = DateTime.Now
             //           },
             //           DateModified = DateTime.Now
             //       },
             //       ReleaseDate = new DateTime(2013, 11, 19)

            editted = db.Tracks.Find(4);
            Assert.AreEqual(editted.Id, 4, "Track Id not 4");
            Assert.AreEqual(editted.Title, "King Sh*t", "Track title not 'King Sh*t'");


            newTitle = "How to be the Man"; newArtist = "Riff Raff"; newAlbumTitle = "Neon Icon";
            newDate = new DateTime(2014, 5, 1);


            editted.Album.Title = newAlbumTitle;
            ctrl.Edit(editted);

            actual = db.Tracks.Find(4);

            Assert.AreEqual(actual.Id, 4, "Track Id not 4");
            Assert.AreEqual(actual.Title, editted.Title, "Track title not 'King Sh*t'");
            Assert.AreEqual(actual.Album.Artist.Name, editted.Album.Artist.Name, "Track artist not 'Yo Gotti'");
            Assert.AreEqual(actual.Album.Title, newAlbumTitle, "Track album title not 'Neon Icon'");
            Assert.AreEqual(actual.ReleaseDate, editted.ReleaseDate, "Track release date not '8/27/2013'");

        }
    }
}
