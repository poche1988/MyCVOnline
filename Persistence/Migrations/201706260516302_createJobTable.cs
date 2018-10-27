namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createJobTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobId = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        JobTitle = c.String(),
                        JobDescription = c.String(),
                        CompanyPhoto = c.String(),
                        CompanyName = c.String(),
                        Reference = c.String(),
                        Active = c.Boolean(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.JobId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Jobs", new[] { "User_Id" });
            DropTable("dbo.Jobs");
        }
    }
}
