using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace FurnitureApp.Repository.Utilities
{
    internal class ConnectionFactory
    {
        private static ConnectionFactory connectionFactory = new ConnectionFactory();
        
        public string DbPath;
        private readonly string settingFilePath = @"Resources\ConnectionSettings.xml";
        private ConnectionFactory()
        {
            ConnectionSetting settings = null;

            if (!File.Exists(this.settingFilePath))
            {
                settings = new ConnectionSetting();
                Utility.DirectoryCreator.CreateSafely(Path.GetDirectoryName(this.settingFilePath));
                Utility.XmlWriter.WriteXml(settings, this.settingFilePath);
            }

            settings = Utility.XmlReader.ReadXml<ConnectionSetting>(this.settingFilePath);

            this.DbPath = settings.ConnectionString;
        }

        public static ConnectionFactory GetInstance()
        {
            return connectionFactory;
        }

        public SQLiteConnection Create()
        {
            var connection = new SQLiteConnection($"Data Source={this.DbPath};Version=3;");
            connection.Open();

            return connection;
        }
    }
}
