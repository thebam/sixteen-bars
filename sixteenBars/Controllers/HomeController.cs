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
                quotes = ctrl.RandomQuotes(blnExplicit, 9);
            }


            string MY_AWS_ACCESS_KEY_ID = "AKIAJGE6S6HIJ6EKMROA";
            string MY_AWS_SECRET_KEY = "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";
            string DESTINATION          = "ecs.amazonaws.com";
            string AssociateTag = "rhy4rhy-20";
        
            

            SignedRequestHelper helper = new SignedRequestHelper(MY_AWS_ACCESS_KEY_ID, MY_AWS_SECRET_KEY, DESTINATION);
            String requestString = "Service=AWSECommerceService"
                    + "&Version=2009-03-31"
                    + "&Operation=ItemSearch"
                    + "&SearchIndex=Music"
                    + "&ResponseGroup=ItemAttributes,Offers,Images"
                    + "&Keywords=because the internet"
                    + "&AssociateTag=" + AssociateTag
                    ;
            String requestUrl = helper.Sign(requestString);
            String title = FetchTitle(requestUrl);
            //WebRequest request = HttpWebRequest.Create(ama);
            //WebResponse response = request.GetResponse();
            //XmlDocument doc = new XmlDocument();
            //doc.Load(response.GetResponseStream());

            //XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", NAMESPACE);
            //if (errorMessageNodes != null && errorMessageNodes.Count > 0)
            //{
            //    String message = errorMessageNodes.Item(0).InnerText;
               
            //}

            //XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(0);
            //string title = titleNode.InnerText;




            return View(quotes);
        }


        private static string FetchTitle(string url)
        {
            string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";
            try
            {
                WebRequest request = HttpWebRequest.Create(url);
                WebResponse response = request.GetResponse();
                XmlDocument doc = new XmlDocument();
                doc.Load(response.GetResponseStream());

                XmlNodeList errorMessageNodes = doc.GetElementsByTagName("Message", NAMESPACE);
                if (errorMessageNodes != null && errorMessageNodes.Count > 0)
                {
                    String message = errorMessageNodes.Item(0).InnerText;
                    return "Error: " + message + " (but signature worked)";
                }

                XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(0);
                string title = titleNode.InnerText;
                return title;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Caught Exception: " + e.Message);
                System.Console.WriteLine("Stack Trace: " + e.StackTrace);
            }

            return null;
        }
    }
}
