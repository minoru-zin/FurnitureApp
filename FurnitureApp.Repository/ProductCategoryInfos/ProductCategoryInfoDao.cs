using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.ProductCategoryInfos
{
    class ProductCategoryInfoDao : DaoBase<ProductCategoryInfo>
    {
        public ProductCategoryInfoDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(ProductCategoryInfo)}s")
        {
        }
    }
}
