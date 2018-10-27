namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForeignKeysToQualification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Qualifications", "level_QualificationLevelId", "dbo.QualificationLevels");
            DropIndex("dbo.Qualifications", new[] { "level_QualificationLevelId" });
            RenameColumn(table: "dbo.Qualifications", name: "level_QualificationLevelId", newName: "QualificationLevelId");
            RenameColumn(table: "dbo.Qualifications", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Qualifications", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.Qualifications", "QualificationLevelId", c => c.Int(nullable: false));
            CreateIndex("dbo.Qualifications", "QualificationLevelId");
            AddForeignKey("dbo.Qualifications", "QualificationLevelId", "dbo.QualificationLevels", "QualificationLevelId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Qualifications", "QualificationLevelId", "dbo.QualificationLevels");
            DropIndex("dbo.Qualifications", new[] { "QualificationLevelId" });
            AlterColumn("dbo.Qualifications", "QualificationLevelId", c => c.Int());
            RenameIndex(table: "dbo.Qualifications", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Qualifications", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Qualifications", name: "QualificationLevelId", newName: "level_QualificationLevelId");
            CreateIndex("dbo.Qualifications", "level_QualificationLevelId");
            AddForeignKey("dbo.Qualifications", "level_QualificationLevelId", "dbo.QualificationLevels", "QualificationLevelId");
        }
    }
}
