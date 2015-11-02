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

            List<Library.Quote> SearchResults = new List<Library.Quote>();
            List<SearchResult> results = new List<SearchResult>();
            switch (searchType)
            {
                case "album":
                    SearchResults = (from q in _db.Quotes
                                     where q.Track.Album.Title.Contains(searchTerm) 
                                     select q).ToList();
                    break;
                case "artist":
                    SearchResults = (from q in _db.Quotes
                                     where q.Artist.Name.Contains(searchTerm) 
                                     select q).ToList();
                    break;
                case "track":
                    SearchResults = (from q in _db.Quotes
                                     where q.Track.Title.Contains(searchTerm) 
                                     select q).ToList();
                    break;
                default:
                    if (wordLink)
                    {
                        if (filter)
                        {
                            SearchResults = (from q in _db.Quotes
                                             where q.Text.Contains(searchTerm) && q.Enabled == true
                                             select q).ToList();
                        }
                        else
                        {
                            SearchResults = (from q in _db.Quotes
                                             where q.Text.Contains(searchTerm) && q.Enabled == true && q.Explicit == false
                                             select q).ToList();
                        }
                        foreach (Quote result in SearchResults)
                        {
                            result.Text = WordLink.CreateLinks(result.Text);
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
                                             where q.Text.Contains(searchTerm) && q.Enabled == true
                                             select new SearchResult
                                             {
                                                 Id = q.Id,
                                                 ResultType = "quote",
                                                 Text = q.Text + " - " + q.Artist.Name,
                                                 URL = "Quotes/"+q.Artist.Name+ "/" +q.Text
                                             }).ToList();
                        }
                        else
                        {
                            resultsQuotes = (from q in _db.Quotes
                                             where q.Text.Contains(searchTerm) && q.Enabled == true && q.Explicit == false
                                             select new SearchResult
                                             {
                                                 Id = q.Id,
                                                 ResultType = "quote",
                                                 Text = q.Text + " - " + q.Artist.Name,
                                                 URL = "Quotes/" + q.Artist.Name + "/" + q.Text
                                             }).ToList();
                        }

                        resultsTracks = (from t in _db.Tracks
                                         where t.Title.Contains(searchTerm) && t.Enabled == true
                                         select new SearchResult
                                         {
                                             Id = t.Id,
                                             ResultType = "track",
                                             Text = t.Title + " - " + t.Album.Artist.Name,
                                             URL = "Tracks/" + t.Album.Title+ "/" + t.Title
                                         }).ToList();
                        resultsAlbums = (from a in _db.Albums
                                         where a.Title.Contains(searchTerm) && a.Enabled == true
                                         select new SearchResult
                                         {
                                             Id = a.Id,
                                             ResultType = "album",
                                             Text = a.Title + " - " + a.Artist.Name,
                                             URL = "Albums/" + a.Artist.Name + "/" + a.Title
                                         }).ToList();
                        resultsArtists = (from a in _db.Artists
                                          where a.Name.Contains(searchTerm) && a.Enabled == true
                                          select new SearchResult
                                          {
                                              Id = a.Id,
                                              ResultType = "artist",
                                              Text = a.Name,
                                              URL = "Artists/" + a.Name
                                          }).ToList();
                        //results = resultsQuotes.Concat(resultsTracks).Concat(resultsAlbums).Concat(resultsArtists).ToList();
                        foreach (var result in resultsQuotes){
                            result.URL = URLClean.Clean(result.URL);
                            results.Add(result);
                        }
                        foreach (var result in resultsTracks)
                        {
                            result.URL = URLClean.Clean(result.URL);
                            results.Add(result);
                        }
                        foreach (var result in resultsAlbums)
                        {
                            result.URL = URLClean.Clean(result.URL);
                            results.Add(result);
                        }
                        foreach (var result in resultsArtists)
                        {
                            result.URL = URLClean.Clean(result.URL);
                            results.Add(result);
                        }
                        JsonResult jsonCombinedResult = new JsonResult();
                        jsonCombinedResult.Data = results;
                        jsonCombinedResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        return jsonCombinedResult;
                    }
                    break;
                    
            }

            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = SearchResults;
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return jsonResult;
        } 
    }
}
