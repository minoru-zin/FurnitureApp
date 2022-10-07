using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00400
{
    public class CostViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            var e = new PropertyChangedEventArgs(PropertyName);
            PropertyChanged?.Invoke(this, e);
        }
        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                this.NotifyPropertyChanged(nameof(Name));
            }
        }

        
        private string unitPrice;

        public string UnitPrice
        {
            get { return unitPrice; }
            set
            {
                unitPrice = value;
                this.NotifyPropertyChanged(nameof(UnitPrice));
            }
        }
        private string quantity;

        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                this.NotifyPropertyChanged(nameof(Quantity));
            }
        }

        public int? TotalAmount { get; set; }
        public Cost Model
        {
            get
            {
                var unitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPrice);
                var quantity = Utility.NumberFormatter.GetNullInt(this.Quantity);

                if (string.IsNullOrEmpty(this.Name) || unitPrice == null | quantity == null)
                {
                    throw new Exception("未入力項目が存在します");
                }

                if (unitPrice <= 0 || quantity <= 0)
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
            this.TotalAmount = cost.TotalAmount;
        }
    }
}
