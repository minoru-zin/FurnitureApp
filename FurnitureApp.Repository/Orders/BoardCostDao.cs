using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class BoardCostDao : DaoBase<BoardCost>
    {
        public BoardCostDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(BoardCost)}s")
        {
        }

        public IEnumerable<BoardCost> SelectByProductId(int productId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(BoardCost.ProductId)} = {productId}
";
            #endregion

            return this.connection.Query<BoardCost>(sql, null, this.transaction);
        }
        public void DeleteByProductId(int productId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(BoardCost.ProductId)} = {productId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
    }
}
