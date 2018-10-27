namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "OnGoing", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "CompanyName", c => c.String(nullable: false));
            DropColumn("dbo.Jobs", "CompanyPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CompanyPhoto", c => c.String());
            AlterColumn("dbo.Jobs", "CompanyName", c => c.String());
            AlterColumn("dbo.Jobs", "JobTitle", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Jobs", "OnGoing");
        }
    }
}
