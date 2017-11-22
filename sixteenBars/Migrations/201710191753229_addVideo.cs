namespace sixteenBars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addVideo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tracks", "Video", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tracks", "Video");
        }
    }
}
