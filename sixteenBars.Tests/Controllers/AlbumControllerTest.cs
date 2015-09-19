using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sixteenBars.Library;
using sixteenBars.Controllers;
using sixteenBars.Models;
using sixteenBars.Tests.Model;
using System.Web.Mvc;
using System.Collections.Generic;

namespace sixteenBars.Tests.Controllers
{
    [TestClass]
    public class AlbumControllerTest
    {
        [TestMethod]
        public void AlbumExists() {
            AlbumController ctrl = new AlbumController(new MockSixteenBarsDb());
            var result = ctrl.AlbumExists(" Because the internet", "childish Gambino ");
            Assert.AreEqual(true, result, "Because the Internet from Childish Gambino should exists.");

            result = ctrl.AlbumExists(" Because the internet", "Jay-z ");
            Assert.AreEqual(false, result, "Because the Internet from Jay-Z should not exists.");
        }

        [TestMethod]
        public void Album_Index()
        {
            AlbumController ctrl = new AlbumController(new MockSixteenBarsDb());
            var result = ctrl.Index() as ViewResult;
            var results = (List<AlbumIndexViewModel>)result.ViewData.Model;
            Assert.AreEqual(4, results.Count, "More or less than 4 albums returned.");

            Assert.AreEqual("Because The Internet", results[0].Title, "Album title not 'Because The Internet'.");
            Assert.AreEqual(false, results[0].IsDeleteable, "Album 'Because the Internet' is not deleteable.");
            Assert.AreEqual("Thank Me Later", results[1].Title, "Album title not 'Thank Me Later'.");
            Assert.AreEqual(false, results[1].IsDeleteable, "Album 'Thank Me Later' is not deleteable.");
            Assert.AreEqual("The Blueprint 2: The Gift & The Curse", results[2].Title, "Album title not 'The Blueprint 2: The Gift & The Curse'.");
            Assert.AreEqual(true, results[2].IsDeleteable, "Album 'The Blueprint 2: The Gift & The Curse' is deleteable.");
            Assert.AreEqual("Yeezus", results[3].Title, "Album title not 'Yeezus'.");
            Assert.AreEqual(true, results[3].IsDeleteable, "Album 'Yeezus' is deleteable.");
        }

        [TestMethod]
        public void Album_Create() {
            ISixteenBarsDb mockDb = new MockSixteenBarsDb();
            AlbumController ctrl = new AlbumController(mockDb);
            Album newAlbum = new Album()
            {
                Id = 5,
                Title = "Doggystyle",
                ReleaseDate = new DateTime(1993, 11, 23),
                Artist = new Artist()
                {
                    Id = 11,
                    Name = "Snoop Doggy Dogg"
                }
            };
            ctrl.Create(newAlbum);
            Assert.AreEqual(newAlbum.Title, mockDb.Albums.Find(5).Title, "Doggystyle not added.");
            Assert.AreEqual(newAlbum.ReleaseDate, mockDb.Albums.Find(5).ReleaseDate, "Doggystyle release date not 11/23/93.");
            Assert.AreEqual(newAlbum.Artist.Name, mockDb.Albums.Find(5).Artist.Name, "Snoop Doggy Dogg not added as artist for Doggystyle.");

            newAlbum = new Album()
            {
                Id = 6,
                Title = ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./",
                ReleaseDate = new DateTime(1993, 11, 23),
                Artist = new Artist()
                {
                    Id = 12,
                    Name = ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./"
                }
            };
            ctrl.Create(newAlbum);
            Assert.AreEqual(newAlbum.Title, mockDb.Albums.Find(6).Title, ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./ not added.");
            Assert.AreEqual(newAlbum.ReleaseDate, mockDb.Albums.Find(6).ReleaseDate, ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./ release date not 11/23/93.");
            Assert.AreEqual(newAlbum.Artist.Name, mockDb.Albums.Find(6).Artist.Name, ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./ not added as artist for .~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./.");

            newAlbum = new Album()
            {
                Id = 7,
                Title = "The Blueprint",
                ReleaseDate = new DateTime(2001, 9, 11),
                Artist = new Artist()
                {
                    Id = 1,
                    Name = "Jay-Z"
                }
            };
            ctrl.Create(newAlbum);
            Assert.AreEqual(newAlbum.Title, mockDb.Albums.Find(7).Title, "The Blueprint not added.");
            Assert.AreEqual(newAlbum.ReleaseDate, mockDb.Albums.Find(7).ReleaseDate, "The Blueprint release date not 9/11/01.");
            Assert.AreEqual("Jay-Z", mockDb.Albums.Find(7).Artist.Name, "Jay-Z not added as artist for The Blueprint.");


            newAlbum = new Album()
            {
                Id = 8,
                Title = "Because the Internet",
                ReleaseDate = new DateTime(2013, 12, 3),
                Artist = new Artist()
                {
                    Id = 3,
                    Name = "Childish Gambino"
                }
             };
            ctrl.Create(newAlbum);
            Assert.IsNull(mockDb.Albums.Find(8), "Because the Internet was created twice");

            newAlbum = new Album()
            {
                Id = 9,
                Title = "Because the Internet",
                ReleaseDate = new DateTime(2013, 12, 3),
                Artist = new Artist()
                {
                    Id = 12,
                    Name = "Childish Gambino"
                }
            };
            ctrl.Create(newAlbum);
            Assert.IsNull(mockDb.Albums.Find(9), "Because the Internet was created twice");
            Assert.IsNull(mockDb.Artists.Find(12), "Childish Gambino was created twice");

            newAlbum = new Album()
            {
                Id = 9,
                Title = "The Blueprint",
                ReleaseDate = new DateTime(2015, 5, 4),
                Artist = new Artist()
                {
                    Id = 12,
                    Name = "Jay-Z Imposter"
                }
            };
            ctrl.Create(newAlbum);
            Assert.AreEqual(newAlbum.Title, mockDb.Albums.Find(9).Title, "The Blueprint from different artist not added.");
            Assert.AreEqual(newAlbum.ReleaseDate, mockDb.Albums.Find(9).ReleaseDate, "The Blueprint from different artist release date not 5/4/15.");
            Assert.AreEqual("Jay-Z Imposter", mockDb.Albums.Find(9).Artist.Name, "The Jay-Z Imposter not added as artist for The other Blueprint album.");



        }

        [TestMethod]
        public void Album_Edit()
        {
            ISixteenBarsDb mockDb = new MockSixteenBarsDb();
            AlbumController ctrl = new AlbumController(mockDb);
            AlbumViewModel editedAlbum = new AlbumViewModel(){
                Id=1,
                Title = "Because of the interwebs",
                ReleaseDate = new DateTime(2015, 5, 4),
                ArtistId = 10
            };
            ctrl.Edit(1);
            Assert.AreEqual(editedAlbum.Title, mockDb.Albums.Find(1).Title, "Title not changed to 'Because of the interwebs'.");
            Assert.AreEqual(editedAlbum.ReleaseDate, mockDb.Albums.Find(1).ReleaseDate, "Release date not changed to '5/4/15'.");
            Assert.AreEqual(editedAlbum.ArtistId, mockDb.Albums.Find(1).Artist.Id, "Artist not changed to 'Dr. Dre'.");

            editedAlbum = new AlbumViewModel()
            {
                Id = 1,
                Title = ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./",
                ReleaseDate = new DateTime(1993, 11, 23),
                ArtistName = ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./"
            };
            ctrl.Edit(1);
            Assert.AreEqual(editedAlbum.Title, mockDb.Albums.Find(1).Title, "Title not changed to .~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./");
            Assert.AreEqual(editedAlbum.ReleaseDate, mockDb.Albums.Find(1).ReleaseDate, "Release date not changed to 11/23/93.");
            Assert.AreEqual(editedAlbum.ArtistName, mockDb.Albums.Find(1).Artist.Name, "Artist not chnaged to .~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./");

            editedAlbum = new AlbumViewModel()
            {
                Id = 1,
                Title = "The Blueprint2",
                ReleaseDate = new DateTime(2002, 9, 11),
                ArtistId = 9
            };
            ctrl.Edit(1);
            Assert.AreEqual(editedAlbum.Title, mockDb.Albums.Find(1).Title, "Title not changed to The Blueprint2.");
            Assert.AreEqual(editedAlbum.ReleaseDate, mockDb.Albums.Find(1).ReleaseDate, "Release date not changed to 9/11/02.");
            Assert.AreEqual("50 Cent", mockDb.Albums.Find(1).Artist.Name, "Artist not changed to 50 Cent");


            //editedAlbum = new AlbumViewModel()
            //{
            //    Id = 1,
            //    Title = "Because the Internet",
            //    ReleaseDate = new DateTime(2013, 12, 3),
            //    ArtistName = "Childish Gambino"
            //};
            //ctrl.Edit(editedAlbum);
            //Assert.AreEqual(1,mockDb.Artists.f, "Because the Internet was created twice");

            
            
        }
    }

}
