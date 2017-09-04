using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sixteenBars.Controllers;
using sixteenBars.Tests.Model;
using sixteenBars.Library;
using sixteenBars.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace sixteenBars.Tests.Controllers
{
    [TestClass]
    public class ArtistControllerTest
    {
        [TestMethod]
        public void Artist_Index()
        {
            var context = new MockSixteenBarsDb();
            ArtistController ctrl = new ArtistController(context);
            var result = ctrl.Index(1) as ViewResult;

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(List<ArtistIndexViewModel>));
            var artists = (List<ArtistIndexViewModel>)result.ViewData.Model;
            Assert.AreEqual(10, artists.Count);
            Assert.AreEqual("Curren$y", artists[2].Name, "Name not Curren$y");
            Assert.AreEqual(true, artists[2].IsDeleteable, "Curren$y is deletable");
            Assert.AreEqual("Dr. Dre", artists[3].Name, "Name not Dr. Dre");
            Assert.AreEqual(true, artists[3].IsDeleteable, "Kanye West is deletable");
            Assert.AreEqual("50 Cent", artists[0].Name, "Name not 50 Cent");
            Assert.AreEqual(true, artists[0].IsDeleteable, "50 Cent is deletable");
            Assert.AreEqual("Drake", artists[4].Name, "Name not Drake");
            Assert.AreEqual(false, artists[4].IsDeleteable, "Drake not deletable");
            Assert.AreEqual("Jay-Z", artists[5].Name, "Name not Jay-Z");
            Assert.AreEqual(false, artists[5].IsDeleteable, "Jay-Z not deletable");
            Assert.AreEqual("Kanye West", artists[6].Name, "Name not Kanye West");
            Assert.AreEqual(true, artists[6].IsDeleteable, "Kanye West is deletable");
        }

        //AutoCompleteName
        //[TestMethod]
        //public void Artist_AutoCompleteName_special_chars()
        //{
        //    var context = new MockSixteenBarsDb();
        //    ArtistController ctrl = new ArtistController(context);

        //    var result = ctrl.AutoCompleteName("lil'") as JsonResult;
        //    List<Artist> results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count, "Lil' Kim not found");
        //    Assert.AreEqual("Lil' Kim", results[0].Name, "Name not Lil' Kim");

        //    result = ctrl.AutoCompleteName("dr.") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count, "Dr. Dre not found");
        //    Assert.AreEqual("Dr. Dre", results[0].Name, "Name not Dr. Dre");

        //    result = ctrl.AutoCompleteName("ren$") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count, "Curren$y not found");
        //    Assert.AreEqual("Curren$y", results[0].Name, "Name not Curren$y");

        //    result = ctrl.AutoCompleteName("ay-") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count, "Jay-Z not found");
        //    Assert.AreEqual("Jay-Z", results[0].Name, "Name not Jay-Z");

        //    result = ctrl.AutoCompleteName("The game") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count, "The Game not found");
        //    Assert.AreEqual("The Game", results[0].Name, "Name not The Game");

        //    result = ctrl.AutoCompleteName(":") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(0, results.Count, ": should not return any results");

        //    result = ctrl.AutoCompleteName(";") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(0, results.Count, "; should not return any results");

        //    result = ctrl.AutoCompleteName("\\") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(0, results.Count, "\\ should not return any results");

        //    result = ctrl.AutoCompleteName("\"") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(0, results.Count, "\" should not return any results");
        //}

        //[TestMethod]
        //public void Artist_AutoCompleteName_general()
        //{
        //    var context = new MockSixteenBarsDb();
        //    ArtistController ctrl = new ArtistController(context);
        //    var result = ctrl.AutoCompleteName("gam") as JsonResult;
        //    List<Artist> results = result.Data as List<Artist>;

        //    Assert.AreEqual(2, results.Count);
        //    Assert.AreEqual("Childish Gambino", results[0].Name, "Name not Childish Gambino");
        //    Assert.AreEqual("The Game", results[1].Name, "Name not The Game");


        //    result = ctrl.AutoCompleteName("Celine Dion") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(0, results.Count, "Celine Dion should not return any results");

        //    result = ctrl.AutoCompleteName("dra ") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count);
        //    Assert.AreEqual("Drake", results[0].Name, "Name not Drake");

        //    result = ctrl.AutoCompleteName(" Kanye West ") as JsonResult;
        //    results = result.Data as List<Artist>;
        //    Assert.AreEqual(1, results.Count);
        //    Assert.AreEqual("Kanye West", results[0].Name, "Name not Kanye West");
        //}

        ////ArtistExists
        //[TestMethod]
        //public void Artist_Exists() {
        //    var context = new MockSixteenBarsDb();
        //    ArtistController ctrl = new ArtistController(context);

        //    Boolean result = ctrl.ArtistExists("lil' kim");
        //    Assert.AreEqual(true, result, "Lil' Kim not found");

        //    result = ctrl.ArtistExists("dr. dre ");
        //    Assert.AreEqual(true, result, "Dr. Dre not found");

        //    result = ctrl.ArtistExists("Curren$y");
        //    Assert.AreEqual(true, result, "Curren$y not found");

        //    result = ctrl.ArtistExists("jay-z");
        //    Assert.AreEqual(true, result, "Jay-Z not found");

        //    result = ctrl.ArtistExists("mos def");
        //    Assert.AreEqual(false, result, "Mos Def should not be found");
            
        //}

        //Details
        [TestMethod]
        public void Artist_Details() {
            ArtistController ctrl = new ArtistController(new MockSixteenBarsDb());
            var result = ctrl.Details(3) as ViewResult;
            var artist = (ArtistDetailViewModel)result.ViewData.Model;
            Assert.AreEqual("Childish Gambino", artist.Name, "Name not Childish Gambino");
            Assert.AreEqual(1, artist.Albums.Count, "More or less than 1 album found");
            Assert.AreEqual("Because The Internet", artist.Albums[0].Title, "Name of album not Because the Internet");
            Assert.AreEqual(1, artist.Quotes.Count, "More or less than 1 quote found");
            Assert.AreEqual("I am winning so you have to dump the gatorade", artist.Quotes[0].Text, "Quote text was not 'I am winning so you have to dump the gatorade'");
        }
        //Create
        [TestMethod]
        public void Artist_Create()
        {
            MockSixteenBarsDb mockDb = new MockSixteenBarsDb();
            ArtistController ctrl = new ArtistController(mockDb);

            Artist newArtist = new Artist()
            {
                ArtistId = 11,
                Name = "Will.I.Am"
            };
            ctrl.Create(newArtist);

            Artist foundArtist = mockDb.Artists.Find(newArtist.ArtistId);
            Assert.AreEqual(newArtist.ArtistId, foundArtist.ArtistId, "Id not 11");
            Assert.AreEqual(newArtist.Name, foundArtist.Name, "Name not Will.I.Am");

            newArtist = new Artist()
            {
                ArtistId = 12,
                Name = ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./"
            };
            ctrl.Create(newArtist);

            foundArtist = mockDb.Artists.Find(newArtist.ArtistId);
            Assert.AreEqual(newArtist.ArtistId, foundArtist.ArtistId, "Id not 12");
            Assert.AreEqual(newArtist.Name, foundArtist.Name, ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./");

            
        }
        //Edit
        [TestMethod]
        public void Artist_Edit()
        {
            MockSixteenBarsDb mockDb = new MockSixteenBarsDb();
            ArtistController ctrl = new ArtistController(mockDb);

            Artist foundArtist = mockDb.Artists.Find(1);
            foundArtist.Name = "Hova";
            ctrl.Edit(foundArtist);
            Artist updatedArtist = mockDb.Artists.Find(1);
            Assert.AreEqual(updatedArtist.ArtistId, foundArtist.ArtistId, "Id not 1");
            Assert.AreEqual(updatedArtist.Name, foundArtist.Name, "Name not Hova");

            foundArtist = mockDb.Artists.Find(1);
            foundArtist.Name = ".~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./";
            ctrl.Edit(foundArtist);
            updatedArtist = mockDb.Artists.Find(1);
            Assert.AreEqual(updatedArtist.ArtistId, foundArtist.ArtistId, "Id not 1");
            Assert.AreEqual(updatedArtist.Name, foundArtist.Name, "Name not .~`!@#$%^&*()_+-={}|:\"<>?[]\\;',./");
        }

        //Delete
        [TestMethod]
        public void Artist_Delete() {
            MockSixteenBarsDb MockDb = new MockSixteenBarsDb();
            ArtistController ctrl = new ArtistController(MockDb);

            //should not be able to delete this artist b/c isdeletable is false
            ctrl.DeleteConfirmed(1);
            Artist foundArtist = MockDb.Artists.Find(1);
            Assert.IsNotNull(foundArtist, "Artist was deleted.");

            //should be able to delete this artist
            ctrl.DeleteConfirmed(8);
            foundArtist = MockDb.Artists.Find(8);
            Assert.IsNull(foundArtist, "Artist was not deleted.");
        }
    }
}
