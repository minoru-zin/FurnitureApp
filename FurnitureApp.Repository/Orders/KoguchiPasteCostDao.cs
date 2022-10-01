using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class KoguchiPasteCostDao : DaoBase<KoguchiPasteCost>
    {
        public KoguchiPasteCostDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(KoguchiPasteCost)}s")
        {
        }

        public IEnumerable<KoguchiPasteCost> SelectByProductId(int productId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(KoguchiPasteCost.ProductId)} = {productId}
";
            #endregion

            return this.connection.Query<KoguchiPasteCost>(sql, null, this.transaction);
        }
        public void DeleteByProductId(int productId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(KoguchiPasteCost.ProductId)} = {productId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
    }
}
