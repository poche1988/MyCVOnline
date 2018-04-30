namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profileUserId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserProfiles", name: "User_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.UserProfiles", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            AddColumn("dbo.UserProfiles", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "UserId");
            RenameIndex(table: "dbo.UserProfiles", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserProfiles", name: "ApplicationUser_Id", newName: "User_Id");
        }
    }
}
