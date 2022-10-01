using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class OrderViewModel
    {
        public DateTime? CreatedDate { get; }
        public string Name { get; }
        public string ClientName { get; }
        public DateTime? DeliveryDate { get; }
        public string Remarks { get; }
        public Order Model { get; }
        public OrderViewModel(Order order)
        {
            this.Model = order;
            this.CreatedDate = order.CreatedDate;
            this.Name = order.Name;
            this.ClientName = order.ClientName;
            this.DeliveryDate = order.DeliveryDate;
            this.Remarks = order.Remarks;
        }
    }
}
