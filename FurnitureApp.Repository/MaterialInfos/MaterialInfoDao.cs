using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.MaterialInfos
{
    class MaterialInfoDao : DaoBase<MaterialInfo>
    {
        public MaterialInfoDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(MaterialInfo)}s")
        {
        }
    }
}
