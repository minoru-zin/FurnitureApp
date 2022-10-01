using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.CostItemInfos
{
    class CostItemInfoDao : DaoBase<CostItemInfo>
    {
        public CostItemInfoDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(CostItemInfo)}s")
        {
        }
    }
}
