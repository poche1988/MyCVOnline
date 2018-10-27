﻿using NUnit.Framework;
using System;
using System.Data.Entity;

namespace MyCvOnlineNUnit.IntegrationTests
{
    [SetUpFixture]
    public class TestFixtureLifeCycle
    {
        public TestFixtureLifeCycle()
        {
            EnsureDataDirectoryConnectionStringPlaceholderIsSet();

            EnsureNoExistingDatabaseFiles();
        }

        private static void EnsureDataDirectoryConnectionStringPlaceholderIsSet()
        {
            // When not running inside MVC application the |DataDirectory| placeholder 
            // is null in a connection string, e.g AttachDBFilename=|DataDirectory|\TestBankingSiteDb.mdf

            AppDomain.CurrentDomain.SetData("DataDirectory", NUnit.Framework.TestContext.CurrentContext.TestDirectory);
        }

        private void EnsureNoExistingDatabaseFiles()
        {
            const string connectionString = "name=DefaultConnection";

            if (Database.Exists(connectionString))
            {
                Database.Delete(connectionString);
            }
        }
    }
}
