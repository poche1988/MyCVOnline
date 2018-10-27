namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class useractivitydeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.UserActivities", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserActivities", new[] { "UserID" });
            DropIndex("dbo.UserActivities", new[] { "ActivityId" });
            DropTable("dbo.UserActivities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserActivities",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        ActivityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserID, t.ActivityId });
            
            CreateIndex("dbo.UserActivities", "ActivityId");
            CreateIndex("dbo.UserActivities", "UserID");
            AddForeignKey("dbo.UserActivities", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserActivities", "ActivityId", "dbo.Activities", "ActivityId", cascadeDelete: true);
        }
    }
}
