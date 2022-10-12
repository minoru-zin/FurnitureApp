using FurnitureApp.Models;
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
        public string UnitPriceText { get; }
        public int? Quantity { get; }
        public int? TotalAmount { get; }
        public string TotalAmountText { get; }
        public DateTime? UpdatedDate { get; }
        public Product Model { get; }
        private CommonData cd = CommonData.GetInstance();
        public ProductViewModel(Product product)
        {
            this.Model = product;
            this.ProductCategoryName = this.cd.ProductCategoryInfoDict.GetValueOrDefault(product.ProductCategoryInfoCode)?.Name;
            this.Name = product.Name;
            this.Width = product.BodyWidth;
            this.Depth = product.BodyDepth;
            this.Height = product.BodyHeight;
            this.UnitPrice = product.GetUnitPrice();
            this.UnitPriceText = $"{this.UnitPrice:C}";
            this.Quantity = product.Quantity;
            this.TotalAmount = this.UnitPrice * this.Quantity;
            this.TotalAmountText = $"{this.UnitPrice * this.Quantity:C}";
            this.UpdatedDate = product.UpdatedDate;
        }
    }
}
