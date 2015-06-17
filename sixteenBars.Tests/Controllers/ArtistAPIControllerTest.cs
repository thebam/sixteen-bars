using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sixteenBars.Controllers;
using sixteenBars.Tests.Model;
using System.Web.Mvc;
using System.Collections.Generic;
using sixteenBars.Library;

namespace sixteenBars.Tests.Controllers
{
    [TestClass]
    public class ArtistAPIControllerTest
    {
        [TestMethod]
        public void ArtistAPI_ArtistExists()
        {
            ArtistAPIController ctrl = new ArtistAPIController(new MockSixteenBarsDb());
            Boolean result = ctrl.ArtistExists("Dr. dre ");
            Assert.AreEqual(true, result, "Dr. Dre should be found.");

            result = ctrl.ArtistExists("Busta Rhymes");
            Assert.AreEqual(false, result, "Busta Rhymes should not be found.");
        }

        [TestMethod]
        public void ArtistAPI_AutoComplete() {
            var context = new MockSixteenBarsDb();
            ArtistAPIController ctrl = new ArtistAPIController(context);

            var result = ctrl.AutoCompleteName("lil'") as JsonResult;
            List<Artist> results = result.Data as List<Artist>;
            Assert.AreEqual(1, results.Count, "Lil' Kim not found");
            Assert.AreEqual("Lil' Kim", results[0].Name, "Name not Lil' Kim");

            result = ctrl.AutoCompleteName("dr.") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(1, results.Count, "Dr. Dre not found");
            Assert.AreEqual("Dr. Dre", results[0].Name, "Name not Dr. Dre");

            result = ctrl.AutoCompleteName("ren$") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(1, results.Count, "Curren$y not found");
            Assert.AreEqual("Curren$y", results[0].Name, "Name not Curren$y");

            result = ctrl.AutoCompleteName("ay-") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(1, results.Count, "Jay-Z not found");
            Assert.AreEqual("Jay-Z", results[0].Name, "Name not Jay-Z");

            result = ctrl.AutoCompleteName("The game") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(1, results.Count, "The Game not found");
            Assert.AreEqual("The Game", results[0].Name, "Name not The Game");

            result = ctrl.AutoCompleteName(":") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(0, results.Count, ": should not return any results");

            result = ctrl.AutoCompleteName(";") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(0, results.Count, "; should not return any results");

            result = ctrl.AutoCompleteName("\\") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(0, results.Count, "\\ should not return any results");

            result = ctrl.AutoCompleteName("\"") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(0, results.Count, "\" should not return any results");


            result = ctrl.AutoCompleteName("gam") as JsonResult;
            results = result.Data as List<Artist>;
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("Childish Gambino", results[0].Name, "Name not Childish Gambino");
            Assert.AreEqual("The Game", results[1].Name, "Name not The Game");

        }
    }
}
