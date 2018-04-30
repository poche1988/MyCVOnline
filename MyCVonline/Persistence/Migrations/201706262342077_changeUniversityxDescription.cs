namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUniversityxDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Qualifications", "University", c => c.String(nullable: false));
            DropColumn("dbo.Qualifications", "QualificationDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Qualifications", "QualificationDescription", c => c.String(nullable: false));
            DropColumn("dbo.Qualifications", "University");
        }
    }
}
