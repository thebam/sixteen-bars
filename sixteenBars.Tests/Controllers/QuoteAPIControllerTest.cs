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
    public class QuoteAPIControllerTest
    {
        [TestMethod]
        public void QuoteAPI_QuoteExists()
        {
            QuoteAPIController ctrl = new QuoteAPIController(new MockSixteenBarsDb());
            Boolean result = ctrl.QuoteExists("Con Edison flow I'm connected to a higher power");
            Assert.AreEqual(true, result, "Quote Con Edison flow I'm connected to a higher power not found");

            result = ctrl.QuoteExists("Mama said knock you out");
            Assert.AreEqual(false, result, "Quote Mama said knock you out found");
        }
    }
}
