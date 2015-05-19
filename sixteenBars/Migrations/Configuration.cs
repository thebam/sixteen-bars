namespace sixteenBars.Migrations
{
    using sixteenBars.Library;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<sixteenBars.Models.SixteenBarsDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(sixteenBars.Models.SixteenBarsDb context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //

            context.Artists.AddOrUpdate(
                a=>a.Name,
                    new Artist() { Id = 3, Name = "Childish Gambino", DateModified = DateTime.Now }
                );
            context.SaveChanges();
            context.Quotes.AddOrUpdate(
              q => q.Text,
              new Quote()
              {
                  Id = 1,
                  Text = "Con Edison flow, I'm connected to a higher power.",
                  Explanation = "Play on words and Dual meaning. \"Con Edison\" is a utility company that supplies energy to the New York city area. The direct meaning is \"I rap so well it is like I am drawing energy from the powergrid.\" The other meaning uses the colloquial usage of \"higher power.\" \"My rap ability is God-given.\"",
                  DateModified = DateTime.Now,
                  Explicit = false,
                  Artist = new Artist() { Id = 1, Name = "Jay-Z", DateModified = DateTime.Now },
                  Track = new Track()
                  {
                      Id = 1,
                      Title = "Light Up",
                      DateModified = DateTime.Now,
                      ReleaseDate = new DateTime(2010, 6, 15),
                      Album = new Album()
                      {
                          Id = 1,
                          Title = "Thank Me Later",
                          ReleaseDate = new DateTime(2010, 6, 15),
                          DateModified = DateTime.Now,
                          Artist = new Artist()
                          {
                              Id = 2,
                              Name = "Drake",
                              DateModified = DateTime.Now
                          }
                      }
                  }
              },
              new Quote()
              {
                  Id = 2,
                  Text = "I'm winning so they have to dump the Gatorade.",
                  Explanation = "Play on words. Gatorade is dumped on the head of the coach for a winning athletic team. The meaning is \"I should be recognized as a winner in my profession and/or in life.\"",
                  DateModified = DateTime.Now,
                  Explicit = false,
                  Artist = context.Artists.Single(a => a.Name == "Childish Gambino"),
                  Track = new Track()
                  {
                      Id = 2,
                      Title = "IV. Sweatpants",
                      DateModified = DateTime.Now,
                      ReleaseDate = new DateTime(2014, 3, 11),
                      Album = new Album() { Id = 2, ReleaseDate = new DateTime(2014, 3, 11), DateModified = DateTime.Now, Title = "Because The Internet", Artist = context.Artists.Single(a => a.Name == "Childish Gambino") }
                  }
              }
            );
        }
    }
}
