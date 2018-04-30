namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TemplatePositionsAndSection : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TemplatePositions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        SectionId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Position_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SectionId)
                .ForeignKey("dbo.TemplatePositions", t => t.Position_ID)
                .Index(t => t.Position_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sections", "Position_ID", "dbo.TemplatePositions");
            DropIndex("dbo.Sections", new[] { "Position_ID" });
            DropTable("dbo.Sections");
            DropTable("dbo.TemplatePositions");
        }
    }
}
