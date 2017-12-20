namespace sixteenBars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class formattedText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "FormattedText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "FormattedText");
        }
    }
}
