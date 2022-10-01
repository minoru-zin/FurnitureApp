﻿using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class ProductDao : DaoBase<Product>
    {
        public ProductDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(Product)}s",
            nameof(Product.Boards),
            nameof(Product.BoardCosts),
            nameof(Product.KoguchiPasteCosts),
            nameof(Product.FinishCutCosts),
            nameof(Product.MakeupBoardPasteCosts),
            nameof(Product.Costs),
            nameof(Product.ProductFiles))
        {
        }
        public IEnumerable<Product> SelectByOrderId(int orderId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(Product.OrderId)} = {orderId}
";
            #endregion

            return this.connection.Query<Product>(sql, null, this.transaction);
        }
        public void DeleteByOrderId(int orderId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(Product.OrderId)} = {orderId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
        public bool ExistProductCategoryInfoId(int? productCategoryInfoId)
        {
            #region SQL
            var sql = $@"
SELECT Id 
FROM {this.tableName}
WHERE
{nameof(Product.ProductCategoryInfoId)} = {productCategoryInfoId}
LIMIT 1
";
            #endregion

            var id = this.connection.Query<int?>(sql, null, this.transaction);

            return id != null;
        }
        public IEnumerable<Product> SelectByProductCategoryInfoId(int productCategoryInfoId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(Product.ProductCategoryInfoId)} = {productCategoryInfoId}
";
            #endregion

            return this.connection.Query<Product>(sql, null, this.transaction);
        }
    }
}