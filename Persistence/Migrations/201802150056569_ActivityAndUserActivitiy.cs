namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityAndUserActivitiy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId);
            
            CreateTable(
                "dbo.UserActivities",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        NotificationId = c.Int(nullable: false),
                        Activity_ActivityId = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserID, t.NotificationId })
                .ForeignKey("dbo.Activities", t => t.Activity_ActivityId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.Activity_ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActivities", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserActivities", "Activity_ActivityId", "dbo.Activities");
            DropIndex("dbo.UserActivities", new[] { "Activity_ActivityId" });
            DropIndex("dbo.UserActivities", new[] { "UserID" });
            DropTable("dbo.UserActivities");
            DropTable("dbo.Activities");
        }
    }
}
