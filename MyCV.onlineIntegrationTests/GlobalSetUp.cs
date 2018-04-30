using NUnit.Framework;
using System.Data.Entity.Migrations;

namespace MyCV.onlineIntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            var configuration = new MyCVonline.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
