namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_IdInJobTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "User_Id" });
            AlterColumn("dbo.Jobs", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Jobs", "User_Id");
            AddForeignKey("dbo.Jobs", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "User_Id" });
            AlterColumn("dbo.Jobs", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Jobs", "User_Id");
            AddForeignKey("dbo.Jobs", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
