namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldUserIdToSections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sections", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "UserId");
        }
    }
}
