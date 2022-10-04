using Dapper;
using FurnitureApp.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    class BoardLayerDao : DaoBase<BoardLayer>
    {
        public BoardLayerDao(SQLiteConnection connection, SQLiteTransaction transaction) : base(connection, transaction, $"{nameof(BoardLayer)}s")
        {
        }
        public IEnumerable<BoardLayer> SelectByBoardId(int boardId)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
{nameof(BoardLayer.BoardId)} = {boardId}
";
            #endregion

            return this.connection.Query<BoardLayer>(sql, null, this.transaction);
        }
        public void DeleteByBoardId(int boardId)
        {
            #region SQL
            var sql = $@"
DELETE
FROM {this.tableName}
WHERE
{nameof(BoardLayer.BoardId)} = {boardId}
";
            #endregion

            this.connection.Execute(sql, null, this.transaction);

        }
        public bool ExistMaterialInfoCode(int? materialInfoId)
        {
            #region SQL
            var sql = $@"
SELECT Id 
FROM {this.tableName}
WHERE
{nameof(BoardLayer.MaterialInfoCode)} = {materialInfoId}
LIMIT 1
";
            #endregion

            var id = this.connection.Query<int?>(sql, null, this.transaction);

            return id != null;
        }
    }
}
