using sixteenBars.Library;
using System;
using System.Collections.Generic;
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
                quotes = ctrl.RandomQuotes(blnExplicit, 9);
            }




            //var ama = "http://webservices.amazon.com/onca/xml?";
            //ama += "Service=AWSECommerceService&";
            //ama += "AWSAccessKeyId=AKIAJ74EZTOBAX3MDW5A&";
            //ama += "AssociateTag=5341-2534-4501&";
            //ama += "Operation=ItemLookup&";
            //ama += "RelationshipType=Tracks&";
            //ama += "ItemId=B0013D8EQK&";
            //ama += "ItemType=ASIN&";
            //ama += "ResponseGroup=RelatedItems,Small&";
            //ama += "Version=2013-08-01";
            //ama += "&Timestamp=[YYYY-MM-DDThh:mm:ssZ]";
            //ama += "&Signature=[Request Signature]";


            //var hc = System.Xml.Linq.XDocument.Load(ama);



            return View(quotes);
        }

        

    }
}
