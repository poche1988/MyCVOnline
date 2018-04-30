namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAllProptoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Summary", c => c.String());
            AddColumn("dbo.AspNetUsers", "Profession", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "Photo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "Photo");
            DropColumn("dbo.AspNetUsers", "Profession");
            DropColumn("dbo.AspNetUsers", "Summary");
        }
    }
}
