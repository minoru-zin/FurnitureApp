using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00300
{
    public class ProductFileViewModel
    {
        public string Name { get; }
        public string Status { get; }
        public ProductFileEx Model { get; }
        public ProductFileViewModel(ProductFileEx productFileEx)
        {
            this.Model = productFileEx;
            this.Name = productFileEx.DisplayName;

            if (productFileEx.IsDeleted)
            {
                this.Status = "削除予定";
            }
        }
    }
}
