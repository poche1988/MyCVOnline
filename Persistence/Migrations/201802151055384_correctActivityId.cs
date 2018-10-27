namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctActivityId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserActivities", "Activity_ActivityId", "dbo.Activities");
            DropIndex("dbo.UserActivities", new[] { "Activity_ActivityId" });
            RenameColumn(table: "dbo.UserActivities", name: "Activity_ActivityId", newName: "ActivityId");
            DropPrimaryKey("dbo.UserActivities");
            AlterColumn("dbo.UserActivities", "ActivityId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserActivities", new[] { "UserID", "ActivityId" });
            CreateIndex("dbo.UserActivities", "ActivityId");
            AddForeignKey("dbo.UserActivities", "ActivityId", "dbo.Activities", "ActivityId", cascadeDelete: true);
            DropColumn("dbo.UserActivities", "NotificationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserActivities", "NotificationId", c => c.Int(nullable: false));
            DropForeignKey("dbo.UserActivities", "ActivityId", "dbo.Activities");
            DropIndex("dbo.UserActivities", new[] { "ActivityId" });
            DropPrimaryKey("dbo.UserActivities");
            AlterColumn("dbo.UserActivities", "ActivityId", c => c.Int());
            AddPrimaryKey("dbo.UserActivities", new[] { "UserID", "NotificationId" });
            RenameColumn(table: "dbo.UserActivities", name: "ActivityId", newName: "Activity_ActivityId");
            CreateIndex("dbo.UserActivities", "Activity_ActivityId");
            AddForeignKey("dbo.UserActivities", "Activity_ActivityId", "dbo.Activities", "ActivityId");
        }
    }
}
