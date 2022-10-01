using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.MaterialSizeInfos
{
    class MaterialSizeInfoDao : DaoBase<MaterialSizeInfo>
    {
        public MaterialSizeInfoDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(MaterialSizeInfo)}s")
        {
        }
        public bool ExistMaterialInfoId(int? materialInfoId)
        {
            #region SQL
            var sql = $@"
SELECT Id 
FROM {this.tableName}
WHERE
{nameof(MaterialSizeInfo.MaterialInfoId)} = {materialInfoId}
LIMIT 1
";
            #endregion

            var id = this.connection.Query<int?>(sql, null, this.transaction);

            return id != null;
        }
    }
}
