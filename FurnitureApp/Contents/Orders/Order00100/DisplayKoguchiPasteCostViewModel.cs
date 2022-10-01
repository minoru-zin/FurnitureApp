using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayKoguchiPasteCostViewModel
    {
        public string BoardName { get; }
        public double? Length { get; }
        public double? Width { get; }
        public string KoguchiMakeupArea { get; }
        public double? UnitLength { get; }
        public double? UnitPrice { get; }
        public int? Quantity { get; }
        public int? TotalAmount { get; }
        private CommonData cd = CommonData.GetInstance();
        public DisplayKoguchiPasteCostViewModel(KoguchiPasteCost k)
        {
            this.BoardName = this.cd.BoardTypes.First(x => x.Code == k.BoardTypeCode).DisplayName;
            this.Length = k.Length;
            this.Width = k.Width;
            this.KoguchiMakeupArea = this.cd.KoguchiKeshouAreas.First(x => x.Code == k.KoguchiMakeupAreaCode).DisplayName;
            this.UnitLength = k.UnitLength;
            this.UnitPrice = k.UnitPrice;
            this.Quantity = k.Quantity;
            this.TotalAmount = k.TotalAmount;
        }
    }
}
