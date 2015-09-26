using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sixteenBars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title="Rhyme 4 Rhyme";
            ViewBag.MetaDescription = "Rhyme 4 Rhyme is a searchable database of witty, funny, and inspirational quotes from Rap/Hip-Hop music.";
            ViewBag.MetaKeywords = "rhyme, rap, hip-hop, hip hop, music, lyrics, quotes, spit, flow, search, rapper, artist, album, track, verse, song, mixtape";

            QuoteController ctrl = new QuoteController();
            Boolean blnExplicit = false;
            HttpCookie isExplicit = Request.Cookies["explicit"];
            if (isExplicit != null)
            {
                if (isExplicit.Value == "explicit")
                {
                    blnExplicit = true;
                }
            }
            var quotes = ctrl.RandomQuotes(blnExplicit, 9);

            return View(quotes);
        }

        

    }
}
