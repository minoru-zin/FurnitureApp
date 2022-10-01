using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Models
{
    public class FinishCutCostCalculator
    {
        public List<FinishCutCost> GetFinishCutCosts(Product product)
        {
            var boardSizes = new BoardSizeCalculator(product).GetBoardSizes();

            var costs = new List<FinishCutCost>();

            foreach(var boardSize in boardSizes)
            {
                if (boardSize.Width == 0 || boardSize.Length == 0) { continue; }

                costs.Add(this.GetCost(boardSize, product.FinishCutUnitPrice));
            }
            return costs;
        }

        private FinishCutCost GetCost(BoardSize boardSize, double finishCutUnitPrice)
        {
            var unitLength = boardSize.Width * 2 + boardSize.Length * 2;

            return new FinishCutCost
            {
                BoardTypeCode = boardSize.BoardType,
                Width = boardSize.Width,
                Length = boardSize.Length,
                UnitLength = unitLength,
                UnitPrice = finishCutUnitPrice,
                Quantity = boardSize.Quantity,
                TotalAmount = (int)(unitLength * finishCutUnitPrice * boardSize.Quantity)
            };
        }
    }
}
