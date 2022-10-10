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
        public int? OrderId { get; }
        public DateTime? CreatedDate { get; }
        public string OrderName { get; }
        public string ClientName { get; }
        public int? ProductCategoryCode { get; }
        public string ProductCategoryName { get; }
        public int? ProductCategorySequence { get; }
        public string Name { get; }
        public double? Width { get; }
        public double? Depth { get; }
        public double? Height { get; }
        public int? Quantity { get; }

        public Product Model { get; }
        private CommonData cd = CommonData.GetInstance();
        public Order00600_ProductViewModel(Order order, Product product)
        {
            this.Model = product;
            this.OrderId = order.Id;
            this.CreatedDate = order.CreatedDate;
            this.OrderName = order.Name;
            this.ClientName = order.ClientName;
            var pcInfo = this.cd.ProductCategoryInfoDict.GetValueOrDefault(product.ProductCategoryInfoCode);
            this.ProductCategoryCode = pcInfo?.Code;
            this.ProductCategoryName = pcInfo?.Name;
            this.ProductCategorySequence = pcInfo?.Sequence;
            this.Name = product.Name;
            this.Width = product.BodyWidth;
            this.Depth = product.BodyDepth;
            this.Height = product.BodyHeight;
            this.Quantity = product.Quantity;
        }
    }
}
