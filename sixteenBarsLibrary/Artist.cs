using System;

namespace sixteenBars.Library
{
    public class Artist
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Boolean Enabled { get; set; }

        public Artist()
        { 
            DateCreated = DateTime.Now;
            Enabled = true;
        }
    }
}
