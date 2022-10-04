using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Models
{
    public class CutSizeCalculator
    {
        private CommonData cd = CommonData.GetInstance();
        public CutSizeCalculator()
        {
        }
        public List<CutSize> GetCutSizes(IEnumerable<Product> products)
        {
            var tempCutSizes = new List<CutSize>();

            foreach (var product in products)
            {
                tempCutSizes.AddRange(this.GetTempCutSizes(product));
            }

            var cutSizes = new List<CutSize>();

            foreach (var g in tempCutSizes.OrderBy(x => x.MaterialInfoId)
                .ThenBy(x => x.Width)
                .ThenBy(x => x.Length)
                .GroupBy(x => new { x.MaterialInfoId, x.MaterialName, x.Width, x.Length }))
            {
                cutSizes.Add(new CutSize
                {
                    MaterialInfoId = g.Key.MaterialInfoId,
                    MaterialName = g.Key.MaterialName,
                    Width = g.Key.Width,
                    Length = g.Key.Length,
                    Quantity = g.Sum(x => x.Quantity)
                });
            }

            return cutSizes;
        }
        public List<CutSize> GetCutSizes(Product product)
        {
            return this.GetCutSizes(new List<Product> { product });
        }
        private List<CutSize> GetTempCutSizes(Product product)
        {
            var tempCutSizes = new List<CutSize>();

            var boardSizes = new BoardSizeCalculator(product).GetBoardSizes();

            foreach (var board in product.Boards)
            {
                var boardSize = boardSizes.First(x => x.BoardType == board.BoardCode);

                foreach (var boardLayer in board.BoardLayers)
                {
                    var materialInfo = this.cd.MaterialInfos.First(x => x.Id == boardLayer.MaterialInfoCode);

                    switch (materialInfo.CutType)
                    {
                        case CutType.Normal:
                            tempCutSizes.Add(new CutSize
                            {
                                MaterialInfoId = (int)materialInfo.Id,
                                MaterialName = materialInfo.Name,
                                Width = boardSize.Width + product.FinishMargin * 2,
                                Length = boardSize.Length + product.FinishMargin * 2,
                                Quantity = boardSize.Quantity,
                            });
                            break;
                        case CutType.Lvl:
                            tempCutSizes.AddRange(this.GetLvlCutSizes(boardSize, materialInfo, product));
                            break;
                        default:
                            throw new NotImplementedException($"未実装 : {nameof(CutType)} {materialInfo.CutType}");
                    }
                }
            }

            return tempCutSizes;
        }
        private List<CutSize> GetLvlCutSizes(BoardSize boardSize, MaterialInfo materialInfo, Product product)
        {
            var cutSizes = new List<CutSize>();

            double frashWidth;
            double frashLength;
            
            // 長い辺をwidthに設定
            if (boardSize.Width > boardSize.Length)
            {
                frashWidth = boardSize.Width + product.FinishMargin * 2;
                frashLength = boardSize.Length + product.FinishMargin * 2;
            }
            else
            {
                frashWidth = boardSize.Length + product.FinishMargin * 2;
                frashLength = boardSize.Width + product.FinishMargin * 2;
            }

            cutSizes.Add(new CutSize
            {
                MaterialInfoId = (int)materialInfo.Id,
                MaterialName = materialInfo.Name,
                Width = frashWidth,
                Length = product.LvlWidth,
                Quantity = boardSize.Quantity * 2
            });

            cutSizes.Add(new CutSize
            {
                MaterialInfoId = (int)materialInfo.Id,
                MaterialName = materialInfo.Name,
                Width = product.LvlWidth,
                Length = frashLength - (product.LvlWidth * 2),
                Quantity = boardSize.Quantity * ((int)(frashWidth / (product.LvlWidth + product.AnkoPitch)) + 1)
            });

            return cutSizes;
        }
    }

    public class CutSize
    {
        public int MaterialInfoId { get; set; }
        public string MaterialName { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public int Quantity { get; set; }

    }

}
