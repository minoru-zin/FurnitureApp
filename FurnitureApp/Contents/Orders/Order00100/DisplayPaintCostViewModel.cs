using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayPaintCostViewModel
    {
        public string BoardName { get; }
        public double? Length { get; }
        public double? Width { get; }
        public double? Thickness { get; }
        public string Name { get; }
        public string PaintArea { get; }
        public double? UnitLength { get; }
        public double? UnitPrice { get; }
        public int? Quantity { get; }
        public string TotalAmount { get; }
        private CommonData cd = CommonData.GetInstance();
        public DisplayPaintCostViewModel(PaintCost p)
        {
            this.BoardName = this.cd.BoardTypes.First(x => x.Code == p.BoardTypeCode).DisplayName;
            this.Name = p.PaintName;
            this.Length = p.Length;
            this.Width = p.Width;
            this.Thickness = p.Thickness;
            this.PaintArea = this.cd.PaintAreas.First(x => x.Code == p.PaintArea).DisplayName;
            this.UnitLength = p.UnitLength;
            this.UnitPrice = p.UnitPrice;
            this.Quantity = p.Quantity;
            this.TotalAmount = $"{p.TotalAmount:C}";
        }
    }
}
