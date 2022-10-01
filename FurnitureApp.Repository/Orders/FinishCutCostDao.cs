using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class FinishCutCostDao : DaoBase<FinishCutCost>
    {
        public FinishCutCostDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(FinishCutCost)}s")
        {
        }

        public IEnumerable<FinishCutCost> SelectByProductId(int productId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(FinishCutCost.ProductId)} = {productId}
";
            #endregion

            return this.connection.Query<FinishCutCost>(sql, null, this.transaction);
        }
        public void DeleteByProductId(int productId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(FinishCutCost.ProductId)} = {productId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
    }
}
