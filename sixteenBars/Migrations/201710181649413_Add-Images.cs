namespace sixteenBars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImages : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Image", c => c.String());
            AddColumn("dbo.Artists", "Image", c => c.String());
            AddColumn("dbo.Tracks", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "Order");
            DropColumn("dbo.Artists", "Image");
            DropColumn("dbo.Albums", "Image");
        }
    }
}
