using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class ProductFileDao : DaoBase<ProductFile>
    {
        public ProductFileDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(ProductFile)}s")
        {
        }

        public IEnumerable<ProductFileEx> SelectByProductId(int productId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(ProductFile.ProductId)} = {productId}
";
            #endregion

            return this.connection.Query<ProductFileEx>(sql, null, this.transaction);
        }
        public void DeleteByProductId(int productId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(ProductFile.ProductId)} = {productId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
    }
}
