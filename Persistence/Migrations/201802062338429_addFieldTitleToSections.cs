namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldTitleToSections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "Title");
        }
    }
}
