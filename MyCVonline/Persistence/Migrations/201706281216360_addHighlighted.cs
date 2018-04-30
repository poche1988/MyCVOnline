namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addHighlighted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Qualifications", "Highlighted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Qualifications", "Highlighted");
        }
    }
}
