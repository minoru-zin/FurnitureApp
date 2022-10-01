using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00400
{
    public class CostViewModel
    {
        public string Name { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string TotalAmount { get; set; }
        public Cost Model
        {
            get
            {
                var unitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPrice);
                var quantity = Utility.NumberFormatter.GetNullInt(this.Quantity);

                if(string.IsNullOrEmpty(this.Name) || unitPrice == null | quantity == null)
                {
                    throw new Exception("未入力項目が存在します");
                }

                if(unitPrice <= 0 || quantity <= 0)
                {
                    throw new Exception("0より大きい値を入力してください");
                }

                return new Cost
                {
                    Name = this.Name,
                    UnitPrice = unitPrice,
                    Quantity = quantity,
                    TotalAmount = unitPrice * quantity
                };
            }
        }
        public CostViewModel(Cost cost)
        {
            this.Name = cost.Name;
            this.UnitPrice = $"{cost.UnitPrice}";
            this.Quantity = $"{cost.Quantity}";
            this.TotalAmount = $"{cost.TotalAmount}";
        }
    }
}
