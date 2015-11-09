using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sixteenBars.Library;
using sixteenBars.Models;
using System.Xml;

namespace sixteenBars.Controllers
{
    public class SitemapController : Controller
    {
        //
        // GET: /Sitemap/
        private ISixteenBarsDb _db;

        public SitemapController() {
            _db = new SixteenBarsDb();
        }

        public SitemapController(ISixteenBarsDb db)
        {
            this._db = db;
        }


        public ActionResult Index(String mapType)
        {
            const string url = "http://www.rhyme4rhyme.com/";

            XmlDocument xmldoc = new XmlDocument();
            XmlDeclaration decl = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", "");
            xmldoc.InsertBefore(decl, xmldoc.DocumentElement);
       	    XmlElement RootNode = xmldoc.CreateElement("urlset");
   	        RootNode.SetAttribute("xmlns","http://www.sitemaps.org/schemas/sitemap/0.9");
            xmldoc.AppendChild(RootNode);
            string sDate;


                switch (mapType) {
                    case "album":
                        List<Album> albums = _db.Albums.ToList();
                        foreach (Album album in albums)
                        {

                            XmlElement childNodeURL = xmldoc.CreateElement("url");
                            XmlElement childNodeLoc = xmldoc.CreateElement("loc");
                            XmlElement childNodeLastMod = xmldoc.CreateElement("lastmod");
                            XmlElement childNodeChangeFreq = xmldoc.CreateElement("changefreq");
                            XmlElement childNodePriority = xmldoc.CreateElement("priority");
                            childNodeLoc.InnerText = url + "Albums/" + URLClean.Clean(album.Artist.Name) + "/" + URLClean.Clean(album.Title);

                            sDate = ((DateTime)album.DateModified).ToString("yyyy-MM-dd");
                            childNodeLastMod.InnerText = sDate;
                            childNodeChangeFreq.InnerText = "monthly";
                            childNodePriority.InnerText = "0.5";

                            childNodeURL.AppendChild(childNodeLoc);
    	                    childNodeURL.AppendChild(childNodeLastMod);
                            childNodeURL.AppendChild(childNodeChangeFreq);
                            childNodeURL.AppendChild(childNodePriority);
                            RootNode.AppendChild(childNodeURL);
                        }

                break;
                    case "artist":
                        List<Artist> artists = _db.Artists.ToList();
                        foreach(Artist artist in artists){
                            XmlElement childNodeURL = xmldoc.CreateElement("url");
                            XmlElement childNodeLoc = xmldoc.CreateElement("loc");
                            XmlElement childNodeLastMod = xmldoc.CreateElement("lastmod");
                            XmlElement childNodeChangeFreq = xmldoc.CreateElement("changefreq");
                            XmlElement childNodePriority = xmldoc.CreateElement("priority");
                            childNodeLoc.InnerText = url + "Artists/" + URLClean.Clean(artist.Name);

                            sDate = ((DateTime)artist.DateModified).ToString("yyyy-MM-dd");
                            childNodeLastMod.InnerText = sDate;
                            childNodeChangeFreq.InnerText = "monthly";
                            childNodePriority.InnerText = "0.5";

                            childNodeURL.AppendChild(childNodeLoc);
    	                    childNodeURL.AppendChild(childNodeLastMod);
                            childNodeURL.AppendChild(childNodeChangeFreq);
                            childNodeURL.AppendChild(childNodePriority);
                            RootNode.AppendChild(childNodeURL);
                        }

                        break;
                 case "quote":
                List<Quote> quotes = _db.Quotes.ToList();
                foreach (Quote quote in quotes)
                        {
                            XmlElement childNodeURL = xmldoc.CreateElement("url");
                            XmlElement childNodeLoc = xmldoc.CreateElement("loc");
                            XmlElement childNodeLastMod = xmldoc.CreateElement("lastmod");
                            XmlElement childNodeChangeFreq = xmldoc.CreateElement("changefreq");
                            XmlElement childNodePriority = xmldoc.CreateElement("priority");
                            childNodeLoc.InnerText = url + "Quotes/" + URLClean.Clean(quote.Artist.Name) + "/" + URLClean.Clean(quote.Text);

                            sDate = ((DateTime)quote.DateModified).ToString("yyyy-MM-dd");
                            childNodeLastMod.InnerText = sDate;
                            childNodeChangeFreq.InnerText = "monthly";
                            childNodePriority.InnerText = "0.8";

                            childNodeURL.AppendChild(childNodeLoc);
    	                    childNodeURL.AppendChild(childNodeLastMod);
                            childNodeURL.AppendChild(childNodeChangeFreq);
                            childNodeURL.AppendChild(childNodePriority);
                            RootNode.AppendChild(childNodeURL);
                        }
	                    
                        break;
                   case "track":
                List<Track> tracks = _db.Tracks.ToList();
                foreach (Track track in tracks)
                        {
                            XmlElement childNodeURL = xmldoc.CreateElement("url");
                            XmlElement childNodeLoc = xmldoc.CreateElement("loc");
                            XmlElement childNodeLastMod = xmldoc.CreateElement("lastmod");
                            XmlElement childNodeChangeFreq = xmldoc.CreateElement("changefreq");
                            XmlElement childNodePriority = xmldoc.CreateElement("priority");
                            childNodeLoc.InnerText = url + "Tracks/" + URLClean.Clean(track.Album.Title) + "/" + URLClean.Clean(track.Title);

                            sDate = ((DateTime)track.DateModified).ToString("yyyy-MM-dd");
                            childNodeLastMod.InnerText = sDate;
                            childNodeChangeFreq.InnerText = "monthly";
                            childNodePriority.InnerText = "0.8";

                            childNodeURL.AppendChild(childNodeLoc);
    	                    childNodeURL.AppendChild(childNodeLastMod);
                            childNodeURL.AppendChild(childNodeChangeFreq);
                            childNodeURL.AppendChild(childNodePriority);
                            RootNode.AppendChild(childNodeURL);
                        }

                        break;
                }

        	    
            
            return Content(xmldoc.InnerXml.ToString(), "text/xml");

	    	
		}


            
        

    }
}
