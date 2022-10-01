using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00200
{
    public class StandardBoardCostViewModel
    {
        public string Name { get; }
        public double? Length { get; }
        public double? Width { get; }
        public int? UnitPrice { get; }
        public int? Quantity { get; }
        public int? SumPrice
        {
            get
            {
                return this.UnitPrice * Quantity;
            }
        }
        public BoardCost Model { get; }
        public StandardBoardCostViewModel(BoardCost standardBoardCost)
        {
            this.Model = standardBoardCost;
            this.Name = standardBoardCost.Name;
            this.Length = standardBoardCost.Length;
            this.Width = standardBoardCost.Width;
            this.UnitPrice = standardBoardCost.UnitPrice;
            this.Quantity = standardBoardCost.Quantity;
        }
    }
}
