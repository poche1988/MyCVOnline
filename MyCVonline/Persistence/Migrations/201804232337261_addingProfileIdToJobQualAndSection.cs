namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingProfileIdToJobQualAndSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ProfileId", c => c.Int(nullable: false));
            AddColumn("dbo.Qualifications", "ProfileId", c => c.Int(nullable: false));
            AddColumn("dbo.Sections", "ProfileId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sections", "ProfileId");
            DropColumn("dbo.Qualifications", "ProfileId");
            DropColumn("dbo.Jobs", "ProfileId");
        }
    }
}
