using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class BoardDao : DaoBase<Board>
    {
        public BoardDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(Board)}s",
            nameof(Board.BoardLayers))
        {
        }

        public IEnumerable<Board> SelectByProductId(int productId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(Board.ProductId)} = {productId}
";
            #endregion

            return this.connection.Query<Board>(sql, null, this.transaction);
        }
        public void DeleteByProductId(int productId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(Board.ProductId)} = {productId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
        public bool ExistPaintCostItemInfoCode(int? paintCostItemInfoCode)
        {
            #region SQL
            var sql = $@"
SELECT Id 
FROM {this.tableName}
WHERE
{nameof(Board.PaintCostItemInfoCode)} = {paintCostItemInfoCode}
LIMIT 1
";
            #endregion

            var id = this.connection.Query<int?>(sql, null, this.transaction).FirstOrDefault();

            return id != null;
        }
    }
}
