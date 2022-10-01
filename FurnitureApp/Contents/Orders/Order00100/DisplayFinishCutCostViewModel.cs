using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayFinishCutCostViewModel
    {
        public string BoardName { get; }
        public double? Length { get; }
        public double? Width { get; }
        public double? UnitLength { get; }
        public double? UnitPrice { get; }
        public int? Quantity { get; }
        public int? TotalAmount { get; }
        private CommonData cd = CommonData.GetInstance();
        public DisplayFinishCutCostViewModel(FinishCutCost f)
        {
            this.BoardName = this.cd.BoardTypes.First(x => x.Code == f.BoardTypeCode).DisplayName;
            this.Length = f.Length;
            this.Width = f.Width;
            this.UnitLength = f.UnitLength;
            this.UnitPrice = f.UnitPrice;
            this.Quantity = f.Quantity;
            this.TotalAmount = f.TotalAmount;
        }
    }
}
