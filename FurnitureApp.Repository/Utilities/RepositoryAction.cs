using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Utilities
{
    internal class RepositoryAction
    {
        public static void Query(Action<SQLiteConnection> action)
        {
            using (var conn = ConnectionFactory.GetInstance().Create())
            {
                action(conn);
            }
        }

        public static void Transaction(Action<SQLiteConnection, SQLiteTransaction> action)
        {
            using (var connection = ConnectionFactory.GetInstance().Create())
            using (var tran = connection.BeginTransaction())
            {
                try
                {
                    action(connection, tran);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
    }
}
