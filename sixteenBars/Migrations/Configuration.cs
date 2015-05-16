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
            context.Quotes.AddOrUpdate(
              q => q.Text,
              new Quote() { Id=1, Text = "Con Edison flow, I'm connected to a higher power.", Artist = new Artist() { Id=1, Name = "Jay-Z" }, Track = new Track() { Id=1,Title="Light Up"} },
              new Quote() { Id=2, Text = "I'm winning so they have to dump the Gatorade.", Artist = new Artist() { Id=2, Name = "Childish Gambino" }, Track = new Track() {Id=2, Title = "Sweatpants" } }
            );
            //
        }
    }
}
