﻿using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class OrderDao : DaoBase<Order>
    {
        public OrderDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(Order)}s",
            $"{nameof(Order.Products)}")
        {
        }

        public IEnumerable<Order> SelectFromCreatedDate(DateTime createdDateF, DateTime createdDateT)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(Order.CreatedDate)} >= '{createdDateF:yyyy-MM-dd}'
AND
{nameof(Order.CreatedDate)} <= '{createdDateT:yyyy-MM-dd}'
";
            #endregion

            return this.connection.Query<Order>(sql, null, transaction);
        }
    }
}
