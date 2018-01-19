using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using sixteenBars.Models;
using System.Web.Mvc;
using sixteenBars.Library;

namespace sixteenBars.Controllers
{
    public class SearchAPIController : ApiController
    {
        ISixteenBarsDb _db;

        public SearchAPIController()
        {
            this._db = new SixteenBarsDb();
        }

        public SearchAPIController(ISixteenBarsDb db) {
            this._db = db;
        }

        [System.Web.Http.HttpGet]
        public JsonResult Search(String searchTerm = null, String searchType = "quote",Boolean wordLink=false,Boolean filter=false)
        {

            List<Quote> SearchResults = new List<Quote>();
            List<SearchResult> results = new List<SearchResult>();
            
            if (wordLink)
            {
                if (filter)
                        {
                            results = (from q in _db.Quotes
                                       where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true
                                       select new SearchResult
                                       {
                                           Id = q.QuoteId,
                                           ResultType = "quote",
                                           Text = q.Text,
                                           URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text,
                                           AdditionalURLS = new List<AdditionalURL>(){
                                               new AdditionalURL{
                                                   Type = "artist",
                                                   Text = q.Artist.Name,
                                                   Link = "Artists-slash-" + q.Artist.Name
                                               }
                                           }
                                       }).ToList();
                        }
                        else
                        {
                            results = (from q in _db.Quotes
                                       where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true && q.Explicit == false
                                             select new SearchResult
                                             {
                                                 Id = q.QuoteId,
                                                 ResultType = "quote",
                                                 Text = q.Text,
                                                 URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text,
                                                 AdditionalURLS = new List<AdditionalURL>(){
                                               new AdditionalURL{
                                                   Type = "artist",
                                                   Text = q.Artist.Name,
                                                   Link = "Artists-slash-" + q.Artist.Name
                                               }
                                           }
                                             }).ToList();
                        }
                        foreach (SearchResult result in results)
                        {
                            result.Text = WordLink.CreateLinks(result.Text);
                            result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                            foreach (AdditionalURL addURL in result.AdditionalURLS)
                            {
                                addURL.Link = URLClean.Clean(addURL.Link).Replace("-slash-", "/");
                            }
                        }
                    }
                    else {
                        List<SearchResult> resultsQuotes = new List<SearchResult>();
                        List<SearchResult> resultsTracks = new List<SearchResult>();
                        List<SearchResult> resultsAlbums = new List<SearchResult>();
                        List<SearchResult> resultsArtists = new List<SearchResult>();
                        if (filter)
                        {
                            
                            resultsQuotes = (from q in _db.Quotes
                                             where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true
                                             select new SearchResult
                                             {
                                                 Id = q.QuoteId,
                                                 ResultType = "quote",
                                                 Text = q.Text + " - " + q.Artist.Name,
                                                 URL = "Quotes-slash-"+q.Artist.Name+ "-slash-" +q.Text
                                             }).ToList();
                        }
                        else
                        {
                            resultsQuotes = (from q in _db.Quotes
                                             where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true && q.Explicit == false
                                             select new SearchResult
                                             {
                                                 Id = q.QuoteId,
                                                 ResultType = "quote",
                                                 Text = q.Text + " - " + q.Artist.Name,
                                                 URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text
                                             }).ToList();
                        }

                        resultsTracks = (from t in _db.Tracks
                                         where t.Title.Contains(searchTerm) && t.Enabled == true && t.Album.Enabled == true && t.Album.Artist.Enabled == true
                                         select new SearchResult
                                         {
                                             Id = t.TrackId,
                                             ResultType = "track",
                                             Text = t.Title + " - " + t.Album.Artist.Name,
                                             URL = "Tracks-slash-" + t.Album.Title+ "-slash-" + t.Title
                                         }).ToList();
                        resultsAlbums = (from a in _db.Albums
                                         where a.Title.Contains(searchTerm) && a.Enabled == true && a.Artist.Enabled == true
                                         select new SearchResult
                                         {
                                             Id = a.AlbumId,
                                             ResultType = "album",
                                             Text = a.Title + " - " + a.Artist.Name,
                                             URL = "Albums-slash-" + a.Artist.Name + "-slash-" + a.Title
                                         }).ToList();
                        resultsArtists = (from a in _db.Artists
                                          where a.Name.Contains(searchTerm) && a.Enabled == true
                                          select new SearchResult
                                          {
                                              Id = a.ArtistId,
                                              ResultType = "artist",
                                              Text = a.Name,
                                              URL = "Artists-slash-" + a.Name
                                          }).ToList();
                        //results = resultsQuotes.Concat(resultsTracks).Concat(resultsAlbums).Concat(resultsArtists).ToList();
                        foreach (var result in resultsQuotes){
                            result.URL = URLClean.Clean(result.URL).Replace("-slash-","/");
                            results.Add(result);
                        }
                        foreach (var result in resultsTracks)
                        {
                            result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                            results.Add(result);
                        }
                        foreach (var result in resultsAlbums)
                        {
                            result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                            results.Add(result);
                        }
                        foreach (var result in resultsArtists)
                        {
                            result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                            results.Add(result);
                        }
                        JsonResult jsonCombinedResult = new JsonResult();
                        jsonCombinedResult.Data = results;
                        jsonCombinedResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return jsonCombinedResult;
                    
                    
            }

            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = results;
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        }
        [System.Web.Http.HttpGet]
        public List<SearchResult> Find(String searchTerm = null, String searchType = "quote", Boolean wordLink = false, Boolean filter = false)
        {

            List<Quote> SearchResults = new List<Quote>();
            List<SearchResult> results = new List<SearchResult>();

            if (wordLink)
            {
                if (filter)
                {
                    results = (from q in _db.Quotes
                               where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true
                               select new SearchResult
                               {
                                   Id = q.QuoteId,
                                   ResultType = "quote",
                                   Text = q.Text,
                                   URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text,
                                   AdditionalURLS = new List<AdditionalURL>(){
                                               new AdditionalURL{
                                                   Type = "artist",
                                                   Text = q.Artist.Name,
                                                   Link = "Artists-slash-" + q.Artist.Name
                                               }
                                           }
                               }).ToList();
                }
                else
                {
                    results = (from q in _db.Quotes
                               where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true && q.Explicit == false
                               select new SearchResult
                               {
                                   Id = q.QuoteId,
                                   ResultType = "quote",
                                   Text = q.Text,
                                   URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text,
                                   AdditionalURLS = new List<AdditionalURL>(){
                                               new AdditionalURL{
                                                   Type = "artist",
                                                   Text = q.Artist.Name,
                                                   Link = "Artists-slash-" + q.Artist.Name
                                               }
                                           }
                               }).ToList();
                }
                foreach (SearchResult result in results)
                {
                    result.Text = WordLink.CreateLinks(result.Text);
                    result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                    foreach (AdditionalURL addURL in result.AdditionalURLS)
                    {
                        addURL.Link = URLClean.Clean(addURL.Link).Replace("-slash-", "/");
                    }
                }
            }
            else
            {
                List<SearchResult> resultsQuotes = new List<SearchResult>();
                List<SearchResult> resultsTracks = new List<SearchResult>();
                List<SearchResult> resultsAlbums = new List<SearchResult>();
                List<SearchResult> resultsArtists = new List<SearchResult>();
                if (filter)
                {

                    resultsQuotes = (from q in _db.Quotes
                                     where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true
                                     select new SearchResult
                                     {
                                         Id = q.QuoteId,
                                         ResultType = "quote",
                                         Text = q.Text + " - " + q.Artist.Name,
                                         URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text
                                     }).ToList();
                }
                else
                {
                    resultsQuotes = (from q in _db.Quotes
                                     where q.Text.Contains(searchTerm) && q.Enabled == true && q.Artist.Enabled == true && q.Track.Enabled == true && q.Track.Album.Enabled == true && q.Track.Album.Artist.Enabled == true && q.Explicit == false
                                     select new SearchResult
                                     {
                                         Id = q.QuoteId,
                                         ResultType = "quote",
                                         Text = q.Text + " - " + q.Artist.Name,
                                         URL = "Quotes-slash-" + q.Artist.Name + "-slash-" + q.Text
                                     }).ToList();
                }

                resultsTracks = (from t in _db.Tracks
                                 where t.Title.Contains(searchTerm) && t.Enabled == true && t.Album.Enabled == true && t.Album.Artist.Enabled == true
                                 select new SearchResult
                                 {
                                     Id = t.TrackId,
                                     ResultType = "track",
                                     Text = t.Title + " - " + t.Album.Artist.Name,
                                     URL = "Tracks-slash-" + t.Album.Title + "-slash-" + t.Title
                                 }).ToList();
                resultsAlbums = (from a in _db.Albums
                                 where a.Title.Contains(searchTerm) && a.Enabled == true && a.Artist.Enabled == true
                                 select new SearchResult
                                 {
                                     Id = a.AlbumId,
                                     ResultType = "album",
                                     Text = a.Title + " - " + a.Artist.Name,
                                     URL = "Albums-slash-" + a.Artist.Name + "-slash-" + a.Title
                                 }).ToList();
                resultsArtists = (from a in _db.Artists
                                  where a.Name.Contains(searchTerm) && a.Enabled == true
                                  select new SearchResult
                                  {
                                      Id = a.ArtistId,
                                      ResultType = "artist",
                                      Text = a.Name,
                                      URL = "Artists-slash-" + a.Name
                                  }).ToList();
                //results = resultsQuotes.Concat(resultsTracks).Concat(resultsAlbums).Concat(resultsArtists).ToList();
                foreach (var result in resultsQuotes)
                {
                    result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                    results.Add(result);
                }
                foreach (var result in resultsTracks)
                {
                    result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                    results.Add(result);
                }
                foreach (var result in resultsAlbums)
                {
                    result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                    results.Add(result);
                }
                foreach (var result in resultsArtists)
                {
                    result.URL = URLClean.Clean(result.URL).Replace("-slash-", "/");
                    results.Add(result);
                }
                return results;
            }
            
            return results;
        }
    }
}
