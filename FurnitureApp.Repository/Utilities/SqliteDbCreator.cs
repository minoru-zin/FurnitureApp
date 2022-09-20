using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace FurnitureApp.Repository.Utilities
{
    public class SqliteDbCreator
    {
        private ConnectionFactory connectionFactory = ConnectionFactory.GetInstance();

        public void Create()
        {
            if (File.Exists(this.connectionFactory.DbPath))
            {
                // TODO バージョンアップ

                return;
            }

            Utility.DirectoryCreator.CreateSafely(Path.GetDirectoryName(this.connectionFactory.DbPath));
            SQLiteConnection.CreateFile(this.connectionFactory.DbPath);
            this.CreateTables();
        }

        private void CreateTables()
        {
            RepositoryAction.Query(c =>
            {
                #region Orders
                var sql = $@"
CREATE TABLE Orders (
    Id integer,
    Name text,
    Remarks text,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                
                #endregion


                #region Products
                sql = $@"
CREATE TABLE Products (
Id integer,    
OrderId integer,
ProductCategoryCode integer,
Name text,
Quantity integer,
BodyWidth real,
BodyDepth real,
BodyHeight real,
FillerL real,
FillerR real,
TopBoardHikae real,
PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Products ON Products(OrderId)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Products2 ON Products(ProductCategoryCode,Name)
";
                c.Execute(sql);

                #endregion

                #region Boards
                sql = $@"
CREATE TABLE Boards (
    Id integer,
    ProductId integer,
    BoardCode integer,
    Quantity integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Boards ON Boards(ProductId)
";
                c.Execute(sql);
                #endregion

                #region BoardLayers
                sql = $@"
CREATE TABLE BoardLayers (
    Id  integer,
    BoardId  integer,
    Sequence  integer,
    MaterialInfoId text,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_BoardLayers ON BoardLayers(BoardId)
";
                c.Execute(sql);
                #endregion

                #region StandardBoardCosts
                sql = $@"
CREATE TABLE StandardBoardCosts (
    Id  integer,
    OrderId integer,
    Name  text,
    Length real,
    Width real,
    UnitPrice integer,
    Quantity integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_StandardBoardCosts ON StandardBoardCosts(OrderId)
";
                c.Execute(sql);

                #endregion

                #region CuttingCosts
                sql = $@"
CREATE TABLE CuttingCosts (
    Id  integer,
    OrderId integer,
    Name  text,
    UnitPrice integer,
    Length real,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                sql = $@"
CREATE INDEX IX_CuttingCosts ON CuttingCosts(OrderId)
";
                c.Execute(sql);
                #endregion

                #region Costs
                sql = $@"
CREATE TABLE Costs (
Id  integer,
    OrderId integer,
    Name  text,
    UnitPrice integer,
    Quantity integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Costs ON Costs(OrderId)
";
                c.Execute(sql);

                #endregion

                #region MaterialInfos
                sql = $@"
CREATE TABLE MaterialInfos (
Id integer,
Name text,
Sequence integer,
Thickness real,
PasteUnitPrice integer,
PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                
                #endregion

                #region MaterialSizeInfos
                sql = $@"
CREATE TABLE MaterialSizeInfos (
Id integer,
MaterialInfoId integer,
Name text,
Length real,
Width real,
UnitPrice integer,
PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_MaterialSizeInfos ON MaterialSizeInfos(MaterialInfoId)
";
                c.Execute(sql);
                #endregion

                #region ProductCategoryInfos
                sql = $@"
CREATE TABLE ProductCategoryInfos (
    Id  integer,
    Name text,
    Sequence  integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                #endregion

            });
        }
    }
}
