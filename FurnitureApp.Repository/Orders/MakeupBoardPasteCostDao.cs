using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class MakeupBoardPasteCostDao : DaoBase<MakeupBoardPasteCost>
    {
        public MakeupBoardPasteCostDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(MakeupBoardPasteCost)}s")
        {
        }

        public IEnumerable<MakeupBoardPasteCost> SelectByProductId(int productId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(MakeupBoardPasteCost.ProductId)} = {productId}
";
            #endregion

            return this.connection.Query<MakeupBoardPasteCost>(sql, null, this.transaction);
        }
        public void DeleteByProductId(int productId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(MakeupBoardPasteCost.ProductId)} = {productId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
    }
}
