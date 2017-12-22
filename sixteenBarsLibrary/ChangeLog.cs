using System;
using System.Web.Mvc;
namespace sixteenBars.Library
{
    public class ChangeLog
    {
        public Int32 Id { get; set; }
        public Int32 UserId { get; set; }
        public String Type { get; set; }
        [AllowHtml]
        public String PreviousValues { get; set; }
        public DateTime DateCreated { get; set; }


        public ChangeLog() {
            DateCreated = DateTime.Now;
        }
    }
}