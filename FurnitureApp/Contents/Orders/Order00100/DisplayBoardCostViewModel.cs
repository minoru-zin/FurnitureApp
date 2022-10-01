using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayBoardCostViewModel
    {
        public string Name { get; }
        public double? Length { get; }
        public double? Width { get; }
        public double? UnitPrice { get; }
        public int? Quantity { get; }
        public int? TotalAmount { get; }
        public DisplayBoardCostViewModel(BoardCost boardCost)
        {
            this.Name = boardCost.Name;
            this.Length = boardCost.Length;
            this.Width = boardCost.Width;
            this.UnitPrice = boardCost.UnitPrice;
            this.Quantity = boardCost.Quantity;
            this.TotalAmount = boardCost.TotalAmount;
        }
    }
}
