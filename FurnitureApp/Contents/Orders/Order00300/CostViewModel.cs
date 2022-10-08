using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00300
{
    public class CostViewModel
    {
        public string Name { get; }
        public int? UnitPrice { get; }
        public int? Quantity { get; }
        public int? TotalAmount { get; }
        public Cost Model { get; }
        public CostViewModel(Cost c)
        {
            this.Model = c;
            this.Name = c.Name;
            this.UnitPrice = c.UnitPrice;
            this.Quantity = c.Quantity;
            this.TotalAmount = c.TotalAmount;
        }
    }
}
