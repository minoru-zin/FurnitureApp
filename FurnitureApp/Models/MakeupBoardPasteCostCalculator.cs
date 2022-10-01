using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Models
{
    public class MakeupBoardPasteCostCalculator
    {
        private CommonData cd = CommonData.GetInstance();

        public List<MakeupBoardPasteCost> GetMakeupBoardPasteCosts(Product product)
        {
            var costs = new List<MakeupBoardPasteCost>();

            var boardSizes = new BoardSizeCalculator(product).GetBoardSizes();

            foreach (var board in product.Boards)
            {
                var boardLayers = board.BoardLayers.Where(x => x.PasteUnitPrice > 0);

                if (!boardLayers.Any()) { continue; }

                var boardSize = boardSizes.First(x => x.BoardType == board.BoardCode);

                foreach (var boadLayer in boardLayers)
                {
                    costs.Add(this.GetCost(board, boadLayer, boardSize));
                }
            }

            return costs;
        }
        private MakeupBoardPasteCost GetCost(Board board, BoardLayer boardLayer, BoardSize boardSize)
        {
            var unitLength = boardSize.Length * boardSize.Width;

            return new MakeupBoardPasteCost
            {
                BoardTypeCode = board.BoardCode,
                Length = boardSize.Length,
                Width = boardSize.Width,
                UnitLength = unitLength,
                UnitPrice = boardLayer.PasteUnitPrice,
                Quantity = board.Quantity,
                TotalAmount = (int)(unitLength * boardLayer.PasteUnitPrice / (303 * 303) * board.Quantity)
            };
        }
    }
}
