using Dapper;
using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.MaterialSizeInfos;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.PaintCostItemInfos;
using FurnitureApp.Repository.ProductCategoryInfos;
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


            var materialInfos = new List<MaterialInfo>
            {
                new MaterialInfo
                {
                    Sequence = 10,
                    Code = 1,
                    Name = "ポストホーム",
                    Thickness = 1,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 20,
                    Code = 2,
                    Name = "人口大理石",
                    Thickness = 1,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 30,
                    Code = 3,
                    Name = "メラミン1t",
                    Thickness = 1,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 40,
                    Code = 4,
                    Name = "ポリ合板2.5t",
                    Thickness = 2.5,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 50,
                    Code = 5,
                    Name = "ラワンベニヤ2.5t",
                    Thickness = 2.5,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 60,
                    Code = 6,
                    Name = "ポリ合板3.8t",
                    Thickness = 3.8,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 70,
                    Code = 7,
                    Name = "Rベニヤ2.5t",
                    Thickness = 2.5,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 80,
                    Code = 8,
                    Name = "Rベニヤ4t",
                    Thickness = 4,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 90,
                    Code = 9,
                    Name = "Rベニヤ5.5t",
                    Thickness = 5.5,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 100,
                    Code = 10,
                    Name = "Rベニヤ9t",
                    Thickness = 9,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 110,
                    Code = 11,
                    Name = "LVL12t",
                    Thickness = 12,
                    CutType = CutType.Lvl
                },
new MaterialInfo
                {
                    Sequence = 120,
                    Code = 12,
                    Name = "LVL15t",
                    Thickness = 15,
                    CutType = CutType.Lvl
                },
new MaterialInfo
                {
                    Sequence = 130,
                    Code = 13,
                    Name = "LC12t",
                    Thickness = 12,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 140,
                    Code = 14,
                    Name = "LC15t",
                    Thickness = 15,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 150,
                    Code = 15,
                    Name = "LC18t",
                    Thickness = 18,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 160,
                    Code = 16,
                    Name = "LC21t",
                    Thickness = 21,
                    CutType = CutType.Normal
                },
new MaterialInfo
                {
                    Sequence = 170,
                    Code = 17,
                    Name = "LC24t",
                    Thickness = 24,
                    CutType = CutType.Normal
                },
            };
            var materialSizeInfos = new List<MaterialSizeInfo>
            {
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 5,
                    Name = "ラワンベニヤ2.5t",
                    Length = 1820,
                    Width = 910,
                    UnitPrice = 570,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 5,
                    Name = "ラワンベニヤ2.5t",
                    Length = 2430,
                    Width = 1230,
                    UnitPrice = 1200,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 4,
                    Name = "ポリ合板2.5t LP",
                    Length = 1820,
                    Width = 910,
                    UnitPrice = 3810,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 4,
                    Name = "ポリ合板2.5t LP",
                    Length = 2430,
                    Width = 1230,
                    UnitPrice = 7640,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 12,
                    Name = "LVL15t",
                    Length = 3900,
                    Width = 45,
                    UnitPrice = 240,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 11,
                    Name = "LVL12t",
                    Length = 3900,
                    Width = 45,
                    UnitPrice = 240,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 3,
                    Name = "メラミン1t TJ",
                    Length = 1820,
                    Width = 910,
                    UnitPrice = 5400,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 3,
                    Name = "メラミン1t TJ",
                    Length = 2430,
                    Width = 1230,
                    UnitPrice = 9600,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 13,
                    Name = "LC12t",
                    Length = 1820,
                    Width = 910,
                    UnitPrice = 1700,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 13,
                    Name = "LC12t",
                    Length = 2430,
                    Width = 1230,
                    UnitPrice = 2900,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 14,
                    Name = "LC15t",
                    Length = 1820,
                    Width = 910,
                    UnitPrice = 1900,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 14,
                    Name = "LC15t",
                    Length = 2430,
                    Width = 1230,
                    UnitPrice = 3200,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 15,
                    Name = "LC18t",
                    Length = 1820,
                    Width = 910,
                    UnitPrice = 2100,
                },
                new MaterialSizeInfo
                {
                    MaterialInfoCode = 15,
                    Name = "LC18t",
                    Length = 2430,
                    Width = 1230,
                    UnitPrice = 3700,
                },
            };

            var productCategoryInfos = new List<ProductCategoryInfo>
            {
                new ProductCategoryInfo
                {
                    Sequence = 1,
                    Code = 1,
                    Name = "洗面台",
                },
                new ProductCategoryInfo
                {
                    Sequence = 2,
                    Code = 2,
                    Name = "収納棚",
                }
            };

            var order = new Order();
            order.CreatedDate = new DateTime(2022, 09, 26);
            order.Name = "KOLN 三宮 新築工事";
            order.ClientName = "大垣林業";
            order.DeliveryDate = new DateTime(2023, 12, 31);
            order.Remarks = "なし";

            var product1 = new Product();
            product1.ProductCategoryInfoCode = 1;
            product1.Name = "洗面台";
            product1.Quantity = 1;
            product1.BodyWidth = 3600;
            product1.BodyDepth = 600;
            product1.BodyHeight = 900;
            product1.FillerL = 0;
            product1.FillerR = 0;
            product1.LvlWidth = 35;
            product1.AnkoPitch = 300;
            product1.DaiwaHeight = 60;
            product1.TobiraTenitaSukima = 3;
            product1.TobiraTenitaHikae = 10;
            product1.TobiraGawaitaMokuji = 4;
            product1.TobiraKanMokuji = 2;
            product1.ShikiriitaGawaitaHikae = 0;
            product1.TanaitaGawaitaHikae = 10;

            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.Tenshita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.Gawaita,
                Quantity = 2,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 11,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.Shikiriita,
                Quantity = 3,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.Tobira,
                Quantity = 8,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.Seita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.Jiita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaFront,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaBack,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 12,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaLeft,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                }
            });
            product1.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaRight,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                }
            });

            order.Products.Add(product1);

            var orders = new List<Order>();
            orders.Add(order);

            order = order.Clone();
            order.CreatedDate = new DateTime(2022, 09, 26);
            order.Name = "テスト";
            order.ClientName = "テスト";
            order.DeliveryDate = new DateTime(2023, 12, 31);
            order.Remarks = "なし";

            var product2 = new Product();
            product2.ProductCategoryInfoCode = 2;
            product2.Name = "収納棚　外：メラミン　内：ポリ";
            product2.Quantity = 1;
            product2.BodyWidth = 800;
            product2.BodyDepth = 400;
            product2.BodyHeight = 1000;
            product2.FillerL = 0;
            product2.FillerR = 0;
            product2.LvlWidth = 40;
            product2.AnkoPitch = 300;
            product2.DaiwaHeight = 60;
            product2.TobiraTenitaSukima = 3;
            product2.TobiraTenitaHikae = 10;
            product2.TobiraGawaitaMokuji = 4;
            product2.TobiraKanMokuji = 3;
            product2.ShikiriitaGawaitaHikae = 0;
            product2.TanaitaGawaitaHikae = 10;

            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Tenita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 13,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 3,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Tenshita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Gawaita,
                Quantity = 2,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Tanaita,
                Quantity = 2,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 4,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Tobira,
                Quantity = 2,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 13,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                    new BoardLayer{ MaterialInfoCode = 3,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Seita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.Jiita,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 4,},
                    new BoardLayer{ MaterialInfoCode = 12,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaFront,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 15,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaBack,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 15,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaLeft,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 15,},
                }
            });
            product2.Boards.Add(new Board
            {
                BoardCode = BoardType.DaiwaRight,
                Quantity = 1,
                BoardLayers = new List<BoardLayer>
                {
                    new BoardLayer{ MaterialInfoCode = 3,},
                    new BoardLayer{ MaterialInfoCode = 5,},
                }
            });

            order.Products = new List<Product> { product2 };

            orders.Add(order);

            order = order.Clone();
            order.Products = new List<Product> { product1, product2 };
            
            orders.Add(order);

            var paintCostInfos = new List<PaintCostItemInfo>
            {
                new PaintCostItemInfo
                {
                    Sequence = 1,
                    Code = 1,
                    Name = "塗装1",
                    UnitPrice = 150,
                }
            };

            new MaterialInfoRepository().Insert(materialInfos);
            new MaterialSizeInfoRepository().Insert(materialSizeInfos);
            new ProductCategoryInfoRepository().Insert(productCategoryInfos);
            new OrderRepository().Insert(orders);
            new PaintCostItemInfoRepository().Insert(paintCostInfos);
        }

        private void CreateTables()
        {
            RepositoryAction.Query(c =>
            {
                #region Orders
                var sql = $@"
CREATE TABLE Orders (
    Id integer,
    CreatedDate date,
    Name text,
    ClientName text,
    DeliveryDate date,
    Remarks text,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Orders ON Orders(CreatedDate)
";
                c.Execute(sql);
                #endregion


                #region Products
                sql = $@"
CREATE TABLE Products (
Id integer,    
OrderId integer,
ProductCategoryInfoCode integer,
Name text,
Quantity integer,
BodyWidth real,
BodyDepth real,
BodyHeight real,
FillerL real,
FillerR real,
LvlWidth real,
AnkoPitch real,
DaiwaHeight real,
TobiraTenitaSukima real,
TobiraTenitaHikae real,
TobiraGawaitaMokuji real,
TobiraKanMokuji real,
ShikiriitaGawaitaHikae real,
TanaitaGawaitaHikae real,
KoguchiPasteUnitPrice real,
FinishCutUnitPrice real,
FinishMargin real,
PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Products ON Products(OrderId)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Products2 ON Products(ProductCategoryInfoCode, Name)
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
    KoguchiKeshouAreaCode integer,
    PaintCostItemInfoCode integer,
    PaintArea integer,
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
    MaterialInfoCode integer,
    PasteUnitPrice int,
    MokumeDirectionCode int,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_BoardLayers ON BoardLayers(BoardId)
";
                c.Execute(sql);
                #endregion

                #region Costs
                sql = $@"
CREATE TABLE Costs (
    Id integer,
    ProductId integer,
    Name  text,
    UnitPrice integer,
    Quantity integer,
    TotalAmount integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_Costs ON Costs(ProductId)
";
                c.Execute(sql);

                #endregion
                #region BoardCosts
                sql = $@"
CREATE TABLE BoardCosts (
    Id  integer,
    ProductId integer,
    Name  text,
    Length real,
    Width real,
    UnitPrice integer,
    Quantity integer,
    TotalAmount integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_BoardCosts ON BoardCosts(ProductId)
";
                c.Execute(sql);

                #endregion

                #region KoguchiPasteCosts
                sql = $@"
CREATE TABLE KoguchiPasteCosts (
    Id  integer,
    ProductId integer,
    BoardTypeCode integer,
    Length real,
    Width real,
    KoguchiMakeupAreaCode integer,
    UnitLength real,
    Quantity integer,
    UnitPrice real,
    TotalAmount integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                sql = $@"
CREATE INDEX IX_KoguchiPasteCosts ON KoguchiPasteCosts(ProductId)
";
                c.Execute(sql);
                #endregion

                #region FinishCutCosts
                sql = $@"
CREATE TABLE FinishCutCosts (
    Id  integer,
    ProductId integer,
    BoardTypeCode integer,
    Length real,
    Width real,
    UnitLength real,
    Quantity integer,
    UnitPrice real,
    TotalAmount integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                sql = $@"
CREATE INDEX IX_FinishCutCosts ON FinishCutCosts(ProductId)
";
                c.Execute(sql);
                #endregion

                #region MakeupBoardPasteCosts
                sql = $@"
CREATE TABLE MakeupBoardPasteCosts (
    Id  integer,
    ProductId integer,
    BoardTypeCode integer,
    MaterialName text,
    Length real,
    Width real,
    UnitLength real,
    Quantity integer,
    UnitPrice integer,
    TotalAmount integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                sql = $@"
CREATE INDEX IX_MakeupBoardPasteCosts ON MakeupBoardPasteCosts(ProductId)
";
                c.Execute(sql);
                #endregion

                #region PaintCosts
                sql = $@"
CREATE TABLE PaintCosts (
    Id  integer,
    ProductId integer,
    BoardTypeCode integer,
    Length real,
    Width real,
    Thickness real,
    PaintName text,
    PaintArea integer,
    UnitLength real,
    Quantity integer,
    UnitPrice real,
    TotalAmount integer,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                sql = $@"
CREATE INDEX IX_PaintCosts ON KoguchiPasteCosts(ProductId)
";
                c.Execute(sql);
                #endregion
                
                #region ProductFiles
                sql = $@"
CREATE TABLE ProductFiles (
    Id  integer,
    ProductId integer,
    DisplayName text,
    FileName text,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                sql = $@"
CREATE INDEX IX_ProductFiles ON ProductFiles(ProductId)
";
                c.Execute(sql);
                #endregion

                #region MaterialInfos
                sql = $@"
CREATE TABLE MaterialInfos (
Id integer,
Code integer,
Name text,
Sequence integer,
Thickness real,
PasteUnitPrice integer,
CutType integer,
UpdatedDate date,
PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);

                #endregion

                #region MaterialSizeInfos
                sql = $@"
CREATE TABLE MaterialSizeInfos (
Id integer,
MaterialInfoCode integer,
Name text,
Length real,
Width real,
UnitPrice integer,
UpdatedDate date,
PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                sql = $@"
CREATE INDEX IX_MaterialSizeInfos ON MaterialSizeInfos(MaterialInfoCode)
";
                c.Execute(sql);
                #endregion

                #region ProductCategoryInfos
                sql = $@"
CREATE TABLE ProductCategoryInfos (
    Id  integer,
    Code integer,
    Name text,
    Sequence  integer,
UpdatedDate date,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                #endregion

                #region CostItemInfos
                sql = $@"
CREATE TABLE CostItemInfos (
    Id  integer,
    CategoryName text,
    Name text,
    Sequence  integer,
    UnitPrice integer,
UpdatedDate date,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                #endregion

                #region PaintCostItemInfos
                sql = $@"
CREATE TABLE PaintCostItemInfos (
    Id  integer,
    Code integer,
    Name text,
    Sequence  integer,
    UnitPrice integer,
UpdatedDate date,
    PRIMARY KEY(Id AUTOINCREMENT)
)
";
                c.Execute(sql);
                #endregion

            });
        }
    }
}
