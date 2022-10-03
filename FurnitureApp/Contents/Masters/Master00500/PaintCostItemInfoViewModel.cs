using FurnitureApp.Repository.PaintCostItemInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00500
{
    public class PaintCostItemInfoViewModel
    {
        public string Name { get; }
        public int? Sequence { get; }
        public string UnitPrice { get; }
        public PaintCostItemInfo Model { get; }
        public PaintCostItemInfoViewModel(PaintCostItemInfo costItemInfo)
        {
            this.Model = costItemInfo;
            this.Name = costItemInfo.Name;
            this.Sequence = costItemInfo.Sequence;
            this.UnitPrice = $"{costItemInfo.UnitPrice:C}";
        }
    }
}
