namespace MyCVonline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fieldLinkAddedToNotifications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "Link");
        }
    }
}
