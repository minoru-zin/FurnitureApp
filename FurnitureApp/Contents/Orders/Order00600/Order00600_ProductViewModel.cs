using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00600
{
    public class Order00600_ProductViewModel
    {
        public string ProductCategoryName { get; }
        public string Name { get; }
        public double? Width { get; }
        public double? Depth { get; }
        public double? Height { get; }

        public Product Model { get; }
        private CommonData cd = CommonData.GetInstance();
        public Order00600_ProductViewModel(Product product)
        {
            this.Model = product;
            this.ProductCategoryName = this.cd.ProductCategoryInfoDict.GetValueOrDefault(product.ProductCategoryInfoCode)?.Name;
            this.Name = product.Name;
            this.Width = product.BodyWidth;
            this.Depth = product.BodyDepth;
            this.Height = product.BodyHeight;
        }
    }
}
