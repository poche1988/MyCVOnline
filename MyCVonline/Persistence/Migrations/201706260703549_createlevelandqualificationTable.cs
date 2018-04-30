namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createlevelandqualificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QualificationLevels",
                c => new
                    {
                        QualificationLevelId = c.Int(nullable: false, identity: true),
                        LevelName = c.String(),
                    })
                .PrimaryKey(t => t.QualificationLevelId);
            
            CreateTable(
                "dbo.Qualifications",
                c => new
                    {
                        QualificationId = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        QualificationTitle = c.String(nullable: false, maxLength: 100),
                        QualificationDescription = c.String(nullable: false),
                        Active = c.Boolean(nullable: false),
                        level_QualificationLevelId = c.Int(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.QualificationId)
                .ForeignKey("dbo.QualificationLevels", t => t.level_QualificationLevelId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.level_QualificationLevelId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Qualifications", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Qualifications", "level_QualificationLevelId", "dbo.QualificationLevels");
            DropIndex("dbo.Qualifications", new[] { "User_Id" });
            DropIndex("dbo.Qualifications", new[] { "level_QualificationLevelId" });
            DropTable("dbo.Qualifications");
            DropTable("dbo.QualificationLevels");
        }
    }
}
