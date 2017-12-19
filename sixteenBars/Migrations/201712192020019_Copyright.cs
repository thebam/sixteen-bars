namespace sixteenBars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Copyright : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "ImageCopyright", c => c.String());
            AddColumn("dbo.Artists", "ImageCopyright", c => c.String());
            AddColumn("dbo.Artists", "RealName", c => c.String());
            AddColumn("dbo.Artists", "Location", c => c.String());
            AddColumn("dbo.Artists", "Biography", c => c.String());
            AddColumn("dbo.Tracks", "VideoCopyright", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "VideoCopyright");
            DropColumn("dbo.Artists", "Biography");
            DropColumn("dbo.Artists", "Location");
            DropColumn("dbo.Artists", "RealName");
            DropColumn("dbo.Artists", "ImageCopyright");
            DropColumn("dbo.Albums", "ImageCopyright");
        }
    }
}
