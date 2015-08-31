using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sixteenBars.Tests.Model;
using sixteenBars.Controllers;
using System.Web.Mvc;
using sixteenBars.Library;
using System.Collections.Generic;

namespace sixteenBars.Tests.Controllers
{
    [TestClass]
    public class TrackAPIControllerTest
    {
        [TestMethod]
        public void TrackAPI_TrackExists()
        {
            TrackAPIController ctrl = new TrackAPIController(new MockSixteenBarsDb());
            Boolean result = ctrl.TrackExists("Light Up", "Thank Me Later", "Drake", new DateTime(2010, 6, 14));
            Assert.AreEqual(true, result, "Track 'Light Up' exists");

            result = ctrl.TrackExists("Curls", "Doomsday", "MF DOOM", new DateTime(2010, 6, 14));
            Assert.AreEqual(false, result, "Track 'Curls' doesn't exists");
        }

        [TestMethod]
        public void TrackAPI_AutoComplete() {
            TrackAPIController ctrl = new TrackAPIController(new MockSixteenBarsDb());
            var result = ctrl.TrackAutoComplete("wet") as JsonResult;
            List<Track> results = result.Data as List<Track>;
            Assert.AreEqual(1, results.Count, "Wetter Than Tsunami not found");
            Assert.AreEqual("Wetter Than Tsunami", results[0].Title, "Name not Wetter Than Tsunami");

            result = ctrl.TrackAutoComplete("thing") as JsonResult;
            results = result.Data as List<Track>;
            Assert.AreEqual(0, results.Count, "Incorrect track found");
           
        }
    }
}
