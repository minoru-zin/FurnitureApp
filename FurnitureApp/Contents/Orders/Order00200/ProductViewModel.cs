using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00200
{
    public class ProductViewModel
    {
        public string ProductCategoryName { get; }
        public string Name { get; }
        public double? Width { get; }
        public double? Depth { get; }
        public double? Height { get; }
        public int? UnitPrice { get; }
        public int? Quantity { get; }
        public int? TotalAmount { get; }
        public Product Model { get; }

        public ProductViewModel(Product product, string productCategoryName)
        {
            this.Model = product;
            this.ProductCategoryName = productCategoryName;
            this.Name = product.Name;
            this.Width = product.BodyWidth;
            this.Depth = product.BodyDepth;
            this.Height = product.BodyHeight;
            this.UnitPrice = product.GetUnitPrice();
            this.Quantity = product.Quantity;
            this.TotalAmount = this.UnitPrice * this.Quantity;
        }
    }
}
