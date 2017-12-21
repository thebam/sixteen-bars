using System;
using System.ComponentModel.DataAnnotations;

namespace sixteenBars.Library
{
    public class Artist
    {
        [Key]
        public Int32 ArtistId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public DateTime DateModified { get; set; }
        [Required]
        public Boolean Enabled { get; set; }
        public string Image { get; set; }
        [Display(Name ="Image Copyright")]
        public string ImageCopyright { get; set; }
        [Display(Name ="Real Name")]
        public String RealName { get; set; }
        public string Location { get; set; }
        public string Biography { get; set; }
        [Display(Name ="Date of Birth")]
        public DateTime BirthDate { get; set; }

        public Artist()
        { 
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
            BirthDate = DateTime.Now;
            Enabled = false;
        }

        public override string ToString()
        {
            return "Id: " + this.ArtistId.ToString() + ",Name: " + this.Name + ",RealName: " + this.RealName + ",BirthDate: " + this.BirthDate + ",Image: " + this.Image + ",ImageCopyright: " + this.ImageCopyright + ",Location: " + this.Location + ",Biography: " + this.Biography + ", DateCreated: " + this.DateCreated.ToString() + ",DateModified: " + this.DateModified.ToString() + ",Enabled: " + this.Enabled;
        }
    }
}
