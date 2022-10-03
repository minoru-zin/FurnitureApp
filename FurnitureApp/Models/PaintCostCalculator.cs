using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.PaintCostItemInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Models
{
    public class PaintCostCalculator
    {
        private CommonData cd = CommonData.GetInstance();
        public PaintCostCalculator()
        {
        }
        public List<PaintCost> GetPaintCosts(Product product)
        {
            var costs = new List<PaintCost>();

            var boardSizes = new BoardSizeCalculator(product).GetBoardSizes();

            foreach (var board in product.Boards)
            {
                if (board.PaintCostItemInfoId == null) { continue; }
                if (board.PaintArea == PaintArea.Nashi) { continue; }

                var boardSize = boardSizes.First(x => x.BoardType == board.BoardCode);

                if (boardSize.Width == 0 || boardSize.Length == 0) { continue; }

                switch (boardSize.BoardType)
                {
                    case BoardType.Tenita:
                    case BoardType.Tenshita:
                    case BoardType.Tanaita:
                    case BoardType.Seita:
                    case BoardType.Jiita:
                    case BoardType.DaiwaFront:
                    case BoardType.DaiwaBack:
                        costs.Add(this.GetCost(board, boardSize, boardSize.Width, boardSize.Length));
                        break;
                    case BoardType.Gawaita:
                    case BoardType.Shikiriita:
                    case BoardType.Tobira:
                    case BoardType.DaiwaLeft:
                    case BoardType.DaiwaRight:
                        costs.Add(this.GetCost(board, boardSize, boardSize.Length, boardSize.Width));
                        break;
                }
            }
            return costs;
        }

        private PaintCost GetCost(Board board, BoardSize boardSize, double frontLength, double sideLength)
        {
            var info = this.cd.PaintCostItemInfos.First(x => x.Id == board.PaintCostItemInfoId);

            var cost = new PaintCost
            {
                BoardTypeCode = boardSize.BoardType,
                PaintArea = board.PaintArea,
                Width = boardSize.Width,
                Length = boardSize.Length,
                Thickness = boardSize.Thickness,
                PaintName = info.Name,
                Quantity = board.Quantity,
                UnitPrice = info.UnitPrice,
            };

            switch (board.PaintArea)
            {
                case PaintArea.OneSide_KoguchiFront:
                    cost.UnitLength = frontLength * sideLength + frontLength * boardSize.Thickness;
                    break;
                case PaintArea.OneSide_KoguchiBothSide:
                    cost.UnitLength = frontLength * sideLength + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.OneSide_KoguchiFrontAndOneSide:
                    cost.UnitLength = frontLength * sideLength + sideLength * boardSize.Thickness;
                    break;
                case PaintArea.OneSide_KoguchiFrontAndBothSide:
                    cost.UnitLength = frontLength * sideLength + frontLength * boardSize.Thickness + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.OneSide_KoguchiAll:
                    cost.UnitLength = frontLength * sideLength + frontLength * boardSize.Thickness * 2 + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.BothSide_KoguchiFront:
                    cost.UnitLength = frontLength * sideLength * 2 + frontLength * boardSize.Thickness;
                    break;
                case PaintArea.BothSide_KoguchiBothSide:
                    cost.UnitLength = frontLength * sideLength * 2 + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.BothSide_KoguchiFrontAndOneSide:
                    cost.UnitLength = frontLength * sideLength * 2 + frontLength * boardSize.Thickness + sideLength * boardSize.Thickness;
                    break;
                case PaintArea.BothSide_KoguchiFrontAndBothSide:
                    cost.UnitLength = frontLength * sideLength * 2 + frontLength * boardSize.Thickness + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.BothSide_KoguchiAll:
                    cost.UnitLength = frontLength * sideLength * 2 + frontLength * boardSize.Thickness * 2 + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.KoguchiFront:
                    cost.UnitLength = frontLength * boardSize.Thickness;
                    break;
                case PaintArea.KoguchiBothSide:
                    cost.UnitLength = sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.KoguchiFrontAndOneSide:
                    cost.UnitLength = frontLength * boardSize.Thickness + sideLength * boardSize.Thickness;
                    break;
                case PaintArea.KoguchiFrontAndBothSide:
                    cost.UnitLength = frontLength * boardSize.Thickness + sideLength * boardSize.Thickness * 2;
                    break;
                case PaintArea.KoguchiAll:
                    cost.UnitLength = frontLength * boardSize.Thickness * 2 + sideLength * boardSize.Thickness * 2;
                    break;
                default:
                    throw new NotImplementedException($"塗装コスト計算想定外 : {board.PaintArea}");

            }

            cost.TotalAmount = (int)(cost.UnitLength * cost.UnitPrice / (303 * 303) * cost.Quantity);

            return cost;
        }
    }
}
