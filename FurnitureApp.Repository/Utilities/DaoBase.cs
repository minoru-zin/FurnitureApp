using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.Utilities
{
    abstract class DaoBase<T>
    {
        /// <summary>
        /// テーブル名
        /// </summary>
        protected string tableName;
        /// <summary>
        /// propertyName1, propertyName2, propertyName3, ... 
        /// </summary>
        protected readonly string insertQuery1;
        /// <summary>
        /// @propertyName1, @propertyName2, @propertyName3, ... 
        /// </summary>
        protected readonly string insertQuery2;
        /// <summary>
        /// propertyName1 = @propertyName1, propertyName2 = @propertyName2, ...
        /// </summary>
        protected readonly string updateQuery1;

        protected SQLiteConnection connection { get; }
        protected SQLiteTransaction transaction { get; }

        public DaoBase(SQLiteConnection connection, SQLiteTransaction transaction, string tableName, params string[] ignoreProperties)
        {
            this.connection = connection;
            this.transaction = transaction;


            this.tableName = tableName;
            var ipns = new List<string>() { "Id" };
            ipns.AddRange(ignoreProperties);

            var fields = typeof(T).GetProperties().Select(x => x.Name).Where(x => ipns.Contains(x) == false);

            this.insertQuery1 = string.Join(", ", fields.Select(s => s));
            this.insertQuery2 = string.Join(", ", fields.Select(s => "@" + s));
            this.updateQuery1 = string.Join(", ", fields.Select(s => $"{s} = @{s}"));
        }

        public T SelectLast()
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
ORDER BY Id DESC
LIMIT 1
";
            #endregion

            if (this.transaction == null) { throw new NullReferenceException("トランザクション必須"); }

            return this.connection.Query<T>(sql, null, this.transaction).First();
        }

        /// <summary>
        /// サロゲートキーで取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T SelectById(int id)
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
WHERE
Id = @id
LIMIT 1
";
            #endregion

            return this.connection.Query<T>(sql, new { id }, this.transaction).FirstOrDefault();
        }
        /// <summary>
        /// すべてのフィールドを更新
        /// 無視フィールド以外
        /// </summary>
        /// <param name="data"></param>
        public void Update(T data)
        {
            #region SQL
            var sql = $@"
UPDATE {this.tableName}
SET
{this.updateQuery1}
WHERE
Id = @Id
";
            #endregion

            this.connection.Execute(sql, data, this.transaction);
        }
        /// <summary>
        /// インサート
        /// 無視フィールド以外
        /// </summary>
        /// <param name="data"></param>
        public void Insert(T data)
        {
            #region SQL
            var sql = $@"
INSERT INTO {this.tableName}
(
{this.insertQuery1}
)
VALUES
(
{this.insertQuery2}
)
";
            #endregion

            try
            {
                this.connection.Execute(sql, data, this.transaction);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// サロゲートキーで削除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteById(int id)
        {
            #region SQL
            var sql = $@"
DELETE FROM {this.tableName}
WHERE Id = @id
";
            #endregion

            this.connection.Execute(sql, new { id }, this.transaction);
        }

        /// <summary>
        /// すべてのレコードを取得
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> SelectAll()
        {
            #region SQL
            var sql = $@"
SELECT *
FROM {this.tableName}
";
            #endregion

            return this.connection.Query<T>(sql, param: null, transaction: this.transaction);
        }
    }
}
