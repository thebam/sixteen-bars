using sixteenBars.Library;
using sixteenBars.Models;
using System;
using System.Data.Entity;

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
                    ArtistId = 1,
                    Name = "Jay-Z",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 2,
                    Name = "Kanye West",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 3,
                    Name = "Childish Gambino",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 4,
                    Name = "Drake",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 5,
                    Name = "Lil' Kim",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 6,
                    Name = "The Game",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 7,
                    Name = "Lil Wayne",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 8,
                    Name = "Curren$y",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 9,
                    Name = "50 Cent",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 10,
                    Name = "Dr. Dre",
                    DateModified = DateTime.Now                    
                },
                new Artist
                {
                    ArtistId = 13,
                    Name = "Mystikal",
                    DateModified = DateTime.Now,
                    Enabled=false
                }
            };
            this.Albums = new MockAlbumDbSet() { 
                new Album{
                    AlbumId=1,
                    Title="Because The Internet",
                    ReleaseDate=new DateTime(2013,12,3),
                    Artist = new Artist(){
                        ArtistId=3,
                        Name="Childish Gambino",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                },
                new Album{
                    AlbumId=2,
                    Title="Thank Me Later",
                    ReleaseDate=new DateTime(2010,6,14),
                    Artist = new Artist(){
                        ArtistId=4,
                        Name="Drake",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                },
                 new Album{
                    AlbumId=3, 
                    Title="Yeezus",
                    ReleaseDate=new DateTime(2013,6,18),
                    Artist = new Artist(){
                        ArtistId=2,
                        Name="Kanye West",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                },
                new Album{
                    AlbumId=4, 
                    Title="The Blueprint 2: The Gift & The Curse",
                    ReleaseDate=new DateTime(2002,11,12),
                    Artist = new Artist(){
                        ArtistId=1,
                        Name="Jay-Z",
                        DateModified =DateTime.Now
                    },
                    DateModified=DateTime.Now
                }
            };
            this.Quotes = new MockQuoteDbSet() { 
                new Quote(){
                    QuoteId=1,
                    Text="I am winning so you have to dump the gatorade",
                    Explanation = "winners",
                    Explicit = false,
                    Artist = new Artist(){
                        ArtistId=3,
                        Name="Childish Gambino",
                        DateModified =DateTime.Now
                    },
                    Track = new Track(){
                        TrackId=1,
                        Title="IV. Sweatpants",
                        Album= new Album(){
                            AlbumId=1,
                            Title="Because The Internet",
                            ReleaseDate=new DateTime(2013,12,3),
                            Artist = new Artist(){
                                ArtistId=3,
                                Name="Childish Gambino",
                                DateModified =DateTime.Now
                            },
                            DateModified=DateTime.Now
                        },
                        ReleaseDate=new DateTime(2013,12,3),
                    }
                },
                new Quote(){
                    QuoteId=2,
                    Text="Con Edison flow I'm connected to a higher power",
                    Explanation = "electric",
                    Explicit = false,
                    Artist = new Artist(){
                        ArtistId=1,
                        Name="Jay-Z",
                        DateModified =DateTime.Now
                    },
                    Track = new Track(){
                        TrackId=2,
                        Title="Light Up",
                        Album= new Album(){
                            AlbumId=2,
                            Title="Thank Me Later",
                            ReleaseDate=new DateTime(2010,6,14),
                            Artist = new Artist(){
                                ArtistId=4,
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
                    TrackId = 2,
                    Title = "Light Up",
                    Album = new Album()
                    {
                        AlbumId = 2,
                        Title = "Thank Me Later",
                        ReleaseDate = new DateTime(2010, 6, 14),
                        Artist = new Artist()
                        {
                            ArtistId = 4,
                            Name = "Drake",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2010, 6, 14)
                },
                new Track()
                {
                    TrackId = 1,
                    Title = "IV. Sweatpants",
                    Album = new Album()
                    {
                        AlbumId = 1,
                        Title = "Because The Internet",
                        ReleaseDate = new DateTime(2013, 12, 3),
                        Artist = new Artist()
                        {
                            ArtistId = 3,
                            Name = "Childish Gambino",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2013, 12, 3)
                },
                new Track()
                {
                    TrackId = 3,
                    Title = "Wetter Than Tsunami",
                    Album = new Album()
                    {
                        AlbumId = 4,
                        Title = "Neon Icon",
                        ReleaseDate = new DateTime(2014, 6, 24),
                        Artist = new Artist()
                        {
                            ArtistId = 11,
                            Name = "Riff Raff",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2014, 6, 24)
                },
                new Track()
                {
                    TrackId = 4,
                    Title = "King Sh*t",
                    Album = new Album()
                    {
                        AlbumId = 5,
                        Title = "I am",
                        ReleaseDate = new DateTime(2013, 11, 19),
                        Artist = new Artist()
                        {
                            ArtistId = 12,
                            Name = "Yo Gotti",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2013, 11, 19)
                },
                new Track()
                {
                    TrackId = 5,
                    Title = "Bandz A Make Her Dance",
                    Album = new Album()
                    {
                        AlbumId = 6,
                        Title = "Stay Trippy",
                        ReleaseDate = new DateTime(2013, 8, 27),
                        Artist = new Artist()
                        {
                            ArtistId = 14,
                            Name = "Juicy J",
                            DateModified = DateTime.Now
                        },
                        DateModified = DateTime.Now
                    },
                    ReleaseDate = new DateTime(2013, 8, 27)
                }
            };
        }

        public IDbSet<Album> Albums { get; private set; }
        public IDbSet<Artist> Artists { get; private set; }
        public IDbSet<Quote> Quotes { get; private set; }
        public IDbSet<Track> Tracks { get; private set; }
        public IDbSet<ChangeLog> ChangeLogs { get; private set; }

        public int SaveChanges()
        {
            return 0;
        }
        public void SetModified(object entity)
        {

        }

    }

}
