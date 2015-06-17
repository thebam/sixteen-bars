using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sixteenBars.Tests.Model
{
    public class MockSixteenBarsDb : ISixteenBarsDb
    {

        public MockSixteenBarsDb()
        {
            this.Artists = new MockArtistDbSet()
            {
                new Artist
                {
                    Id = 1,
                    Name = "Jay-Z",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 2,
                    Name = "Kanye West",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 3,
                    Name = "Childish Gambino",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 4,
                    Name = "Drake",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 5,
                    Name = "Lil' Kim",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 6,
                    Name = "The Game",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 7,
                    Name = "Lil Wayne",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 8,
                    Name = "Curren$y",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 9,
                    Name = "50 Cent",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    Id = 10,
                    Name = "Dr. Dre",
                    DateModified = DateTime.Now                    
                }
            };
            this.Albums = new MockAlbumDbSet() { 
                new Album{
                    Id=1,
                    Title="Because the Internet",
                    ReleaseDate=new DateTime(2013,12,3),
                    Artist = new Artist(){
                        Id=3,
                        Name="Childish Gambino",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                },
                new Album{
                    Id=2,
                    Title="Thank Me Later",
                    ReleaseDate=new DateTime(2010,6,14),
                    Artist = new Artist(){
                        Id=4,
                        Name="Drake",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                },
                 new Album{
                    Id=3, 
                    Title="Yeezus",
                    ReleaseDate=new DateTime(2013,6,18),
                    Artist = new Artist(){
                        Id=2,
                        Name="Kanye West",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                }
            };
            this.Quotes = new MockQuoteDbSet() { 
                new Quote(){
                    Id=1,
                    Text="I am winning so you have to dump the gatorade",
                    Explanation = "winners",
                    Explicit = false,
                    Artist = new Artist(){
                        Id=3,
                        Name="Childish Gambino",
                        DateModified =DateTime.Now
                    },
                    Track = new Track(){
                        Id=1,
                        Title="sweatpants",
                        Album= new Album(){
                            Id=1,
                            Title="Because the Internet",
                            ReleaseDate=new DateTime(2013,12,3),
                            Artist = new Artist(){
                                Id=3,
                                Name="Childish Gambino",
                                DateModified =DateTime.Now
                            },
                            DateModified=DateTime.Now
                        },
                        ReleaseDate=new DateTime(2013,12,3),
                    }
                },
                new Quote(){
                    Id=2,
                    Text="Con Edison flow I'm connected to a higher power",
                    Explanation = "electric",
                    Explicit = false,
                    Artist = new Artist(){
                        Id=1,
                        Name="Jay-Z",
                        DateModified =DateTime.Now
                    },
                    Track = new Track(){
                        Id=2,
                        Title="Light Up",
                        Album= new Album(){
                            Id=2,
                            Title="Thank Me Later",
                            ReleaseDate=new DateTime(2010,6,14),
                            Artist = new Artist(){
                                Id=4,
                                Name="Drake",
                                DateModified =DateTime.Now
                            },
                            DateModified=DateTime.Now
                        },
                        ReleaseDate=new DateTime(2010,6,14),
                    }
                }
            };
            this.Tracks = new MockTrackDbSet() {
                new Track()
                {
                    Id = 2,
                    Title = "Light Up",
                    Album = new Album()
                    {
                        Id = 2,
                        Title = "Thank Me Later",
                        ReleaseDate = new DateTime(2010, 6, 14),
                        Artist = new Artist()
                        {
                            Id = 4,
                            Name = "Drake",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2010, 6, 14),
                },
                new Track()
                {
                    Id = 1,
                    Title = "sweatpants",
                    Album = new Album()
                    {
                        Id = 1,
                        Title = "Because the Internet",
                        ReleaseDate = new DateTime(2013, 12, 3),
                        Artist = new Artist()
                        {
                            Id = 3,
                            Name = "Childish Gambino",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2013, 12, 3),
                }
            };
        }

        public IDbSet<Album> Albums { get; private set; }
        public IDbSet<Artist> Artists { get; private set; }
        public IDbSet<Quote> Quotes { get; private set; }
        public IDbSet<Track> Tracks { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
        public void SetModified(object entity)
        {

        }

    }

}
