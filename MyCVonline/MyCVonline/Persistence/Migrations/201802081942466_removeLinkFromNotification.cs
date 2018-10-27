namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeLinkFromNotification : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Notifications", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Link", c => c.String());
        }
    }
}
