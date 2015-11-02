using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sixteenBars.Models
{
    public class SearchResult
    {
        public Int32 Id { get; set; }
        public String ResultType { get; set; }
        public String Text { get; set; }
        public String URL { get; set; }
    }
}