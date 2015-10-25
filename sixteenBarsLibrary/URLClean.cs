using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sixteenBars.Library
{
    static public class URLClean
    {
        public static String Clean(String queryvalue) {
            return queryvalue.Replace(".", "").Replace(",", "");
        }
    }
}