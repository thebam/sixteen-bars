using sixteenBars.Library;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace sixteenBars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title="Rhyme 4 Rhyme";
            ViewBag.MetaDescription = "Rhyme 4 Rhyme is a searchable database of witty, funny, and inspirational quotes from Rap/Hip-Hop music.";
            ViewBag.MetaKeywords = "rhyme, rap, hip-hop, hip hop, music, lyrics, quotes, spit, flow, search, rapper, artist, album, track, verse, song, mixtape";


            List<Quote> quotes = new List<Quote>();
            if (Request.QueryString["word"] == null)
            {

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
                quotes = ctrl.RandomQuotes(blnExplicit, 15);
            }

            return View(quotes);
        }


        
    }
}
