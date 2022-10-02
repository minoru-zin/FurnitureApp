using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayMakeupBoardPasteCostViewModel
    {
        public string BoardName { get; }
        public double? Length { get; }
        public double? Width { get; }
        public double? UnitLength { get; }
        public double? UnitPrice { get; }
        public int? Quantity { get; }
        public string TotalAmount { get; }
        private CommonData cd = CommonData.GetInstance();
        public DisplayMakeupBoardPasteCostViewModel(MakeupBoardPasteCost m)
        {
            this.BoardName = this.cd.BoardTypes.First(x => x.Code == m.BoardTypeCode).DisplayName;
            this.Length = m.Length;
            this.Width = m.Width;
            this.UnitLength = m.UnitLength;
            this.UnitPrice = m.UnitPrice;
            this.Quantity = m.Quantity;
            this.TotalAmount = $"{m.TotalAmount:C}";
        }
    }
}
