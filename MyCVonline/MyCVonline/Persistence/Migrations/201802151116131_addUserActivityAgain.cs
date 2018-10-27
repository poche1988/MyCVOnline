namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserActivityAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserActivities",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ActivityId })
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActivities", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserActivities", "ActivityId", "dbo.Activities");
            DropIndex("dbo.UserActivities", new[] { "ActivityId" });
            DropIndex("dbo.UserActivities", new[] { "UserID" });
            DropTable("dbo.UserActivities");
        }
    }
}
