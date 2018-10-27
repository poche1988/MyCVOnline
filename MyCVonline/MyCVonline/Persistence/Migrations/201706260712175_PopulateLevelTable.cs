namespace MyCVonline.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateLevelTable : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into QualificationLevels values ('Doctorate')");
            Sql("Insert into QualificationLevels values ('Master')");
            Sql("Insert into QualificationLevels values ('Postgraduate Diploma')");
            Sql("Insert into QualificationLevels values ('Graduate Diploma')");
            Sql("Insert into QualificationLevels values ('Bachelor Degree')");
            Sql("Insert into QualificationLevels values ('Certificate')");
            Sql("Insert into QualificationLevels values ('Diploma')");
            Sql("Insert into QualificationLevels values ('Course')");
            Sql("Insert into QualificationLevels values ('Seminar')");
        }
        
        public override void Down()
        {
            Sql("Delete from QualificationLevels where QualificationLevelId in (1, 2, 3, 4, 5, 6, 7, 8, 9)");
        }
    }
}
