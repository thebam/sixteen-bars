namespace sixteenBars.Migrations
{
    using Library;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Models.SixteenBarsDb>
    {
        public Configuration()
        {
            //AutomaticMigrationsEnabled = false;
        }

        //protected override void Seed(Models.SixteenBarsDb context)
        //{
            
        //    context.Quotes.AddOrUpdate(q => q.Text,
        //        new Quote()
        //        {
        //            Text = "I am winning so you have to dump the gatorade",
        //            Explanation = "winners",
        //            Enabled = true,
        //            Explicit = false,
        //            DateCreated = DateTime.Now,
        //            DateModified = DateTime.Now,
        //            Artist = new Artist()
        //            {
        //                ArtistId = 1,
        //                Name = "Childish Gambino",
        //                DateModified = DateTime.Now,
        //                DateCreated = DateTime.Now,
        //                Enabled = true
        //            },
        //            Track = new Track()
        //            {
        //                Title = "IV. Sweatpants",
        //                DateModified = DateTime.Now,
        //                DateCreated = DateTime.Now,
        //                Enabled = true,
        //                Order = 4,
        //                Album = new Album()
        //                {
        //                    Title = "Because The Internet",
        //                    ReleaseDate = new DateTime(2013, 12, 3),
        //                    DateModified = DateTime.Now,
        //                    DateCreated = DateTime.Now,
        //                    Enabled = true,
        //                    ArtistId = 1
        //                },
        //                ReleaseDate = new DateTime(2013, 12, 3),
        //            }
        //        }
        //        );
        //    //  This method will be called after migrating to the latest version.

        //    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //    //  to avoid creating duplicate seed data. E.g.
        //    //
        //    //    context.People.AddOrUpdate(
        //    //      p => p.FullName,
        //    //      new Person { FullName = "Andrew Peters" },
        //    //      new Person { FullName = "Brice Lambson" },
        //    //      new Person { FullName = "Rowan Miller" }
        //    //    );
        //    //
        //}
    }
}
