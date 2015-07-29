using System;
using sixteenBars.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sixteenBars.Controllers;
using System.Web.Mvc;
using sixteenBars.Library;
using System.Collections.Generic;

namespace sixteenBars.Tests.Controllers
{
    [TestClass]
    public class AlbumAPIControllerTest
    {
        [TestMethod]
        public void AlbumAPI_AlbumExists()
        {
            AlbumAPIController ctrl = new AlbumAPIController(new MockSixteenBarsDb());
            Boolean result = ctrl.AlbumExists("because The Internet ","childish gambino");
            Assert.AreEqual(true, result, "Album 'Because The Internet' by 'Childish Gambino' not found");

            result = ctrl.AlbumExists("because The Internet ", "Nas");
            Assert.AreEqual(false, result, "Album 'Because The Internet' by 'Nas' should not be found");

            result = ctrl.AlbumExists("the chronic", "Dr. Dre");
            Assert.AreEqual(false, result, "Album 'The Chronic' by 'Dr. Dre' should not be found");
        }

        [TestMethod]
        public void AlbumAPI_AutoComplete() {
            AlbumAPIController ctrl = new AlbumAPIController(new MockSixteenBarsDb());
            var result = ctrl.AlbumAutoComplete("thank") as JsonResult;
            List<Album> results = result.Data as List<Album>;
            Assert.AreEqual(1, results.Count, "Thank Me Later not found");
            Assert.AreEqual("Thank Me Later", results[0].Title, "Name not Thank Me Later");

            result = ctrl.AlbumAutoComplete("the") as JsonResult;
            results = result.Data as List<Album>;
            Assert.AreEqual(2, results.Count, "Less than or greater 2 albums found");
            Assert.AreEqual("Because The Internet", results[0].Title, "Name not Because The Internet");
            Assert.AreEqual("The Blueprint 2: The Gift & The Curse", results[1].Title, "Name not The Blueprint 2: The Gift & The Curse");

            result = ctrl.AlbumAutoComplete("the","childish") as JsonResult;
            results = result.Data as List<Album>;
            Assert.AreEqual(1, results.Count, "Less than or greater 1 album found");
            Assert.AreEqual("Because The Internet", results[0].Title, "Name not Because The Internet");

            result = ctrl.AlbumAutoComplete("the", "nas") as JsonResult;
            results = result.Data as List<Album>;
            Assert.AreEqual(0, results.Count, "Album should not be found");
        }
    }
}
