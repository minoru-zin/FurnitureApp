using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.PaintCostItemInfos
{
    class PaintCostItemInfoDao:DaoBase<PaintCostItemInfo>
    {
        public PaintCostItemInfoDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(PaintCostItemInfo)}s")
        {
        }

    }
}
