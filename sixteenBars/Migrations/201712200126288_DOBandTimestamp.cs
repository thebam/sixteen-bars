namespace sixteenBars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DOBandTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Artists", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quotes", "Timestamp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "Timestamp");
            DropColumn("dbo.Artists", "BirthDate");
        }
    }
}
