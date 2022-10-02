using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayCostViewModel
    {
        public string Name { get; }
        public string UnitPrice { get; }
        public int? Quantity { get; }
        public string TotalAmount { get; }
        public Cost Model { get; }
        public DisplayCostViewModel(Cost cost)
        {
            this.Model = cost;
            this.Name = cost.Name;
            this.UnitPrice = $"{cost.UnitPrice:C}";
            this.Quantity = cost.Quantity;
            this.TotalAmount = $"{cost.TotalAmount:C}";
        }
    }
}
