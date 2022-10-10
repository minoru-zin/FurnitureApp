using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.MaterialSizeInfos
{
    class MaterialSizeInfoDao : DaoBase<MaterialSizeInfo>
    {
        public MaterialSizeInfoDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(MaterialSizeInfo)}s")
        {
        }
        public bool ExistMaterialInfoCode(int? materialInfoCode)
        {
            #region SQL
            var sql = $@"
SELECT Id 
FROM {this.tableName}
WHERE
{nameof(MaterialSizeInfo.MaterialInfoCode)} = {materialInfoCode}
LIMIT 1
";
            #endregion

            var id = this.connection.Query<int?>(sql, null, this.transaction).FirstOrDefault();

            return id != null;
        }
    }
}
