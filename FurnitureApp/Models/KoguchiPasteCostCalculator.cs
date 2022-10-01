using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Models
{
    public class KoguchiPasteCostCalculator
    {
        public KoguchiPasteCostCalculator()
        {
        }
        public List<KoguchiPasteCost> GetKoguchiPasteCosts(Product product)
        {

            var costs = new List<KoguchiPasteCost>();

            var boardSizes = new BoardSizeCalculator(product).GetBoardSizes();

            foreach(var board in product.Boards)
            {
                if (board.KoguchiKeshouAreaCode == KoguchiMakeupArea.Nashi) { continue; }

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
                        costs.Add(this.GetCost(board, boardSize, boardSize.Width, boardSize.Length, product.KoguchiPasteUnitPrice));
                        break;
                    case BoardType.Gawaita:
                    case BoardType.Shikiriita:
                    case BoardType.Tobira:
                    case BoardType.DaiwaLeft:
                    case BoardType.DaiwaRight:
                        costs.Add(this.GetCost(board, boardSize, boardSize.Length, boardSize.Width, product.KoguchiPasteUnitPrice));
                        break;
                }
            }
            return costs;
        }

        private KoguchiPasteCost GetCost(Board board, BoardSize boardSize, double frontLength, double sideLength, double unitPrice)
        {
            var cost = new KoguchiPasteCost
            {
                BoardTypeCode = boardSize.BoardType,
                KoguchiMakeupAreaCode = board.KoguchiKeshouAreaCode,
                Width = boardSize.Width,
                Length = boardSize.Length,
                Quantity = board.Quantity,
                UnitPrice = unitPrice,
            };

            switch (board.KoguchiKeshouAreaCode)
            {
                case KoguchiMakeupArea.Front:
                    cost.UnitLength = frontLength;
                    break;
                case KoguchiMakeupArea.BothSide:
                    cost.UnitLength = sideLength * 2;
                    break;
                case KoguchiMakeupArea.FrontAndOneSide:
                    cost.UnitLength = frontLength + sideLength;
                    break;
                case KoguchiMakeupArea.FrontAndBothSide:
                    cost.UnitLength = frontLength + sideLength * 2;
                    break;
                case KoguchiMakeupArea.All:
                    cost.UnitLength = frontLength * 2 + sideLength * 2;
                    break;
                default:
                    throw new NotImplementedException($"木口コスト計算想定外 : {board.KoguchiKeshouAreaCode}");

            }

            cost.TotalAmount = (int)(cost.UnitLength * cost.UnitPrice * cost.Quantity);

            return cost;
        } 
    }
}
