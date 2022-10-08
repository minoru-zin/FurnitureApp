using FurnitureApp.Repository.PaintCostItemInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00500
{
    public class PaintCostItemInfoViewModel
    {
        public int? Sequence { get; }
        public int? Code { get; }
        public string Name { get; }
        public string UnitPrice { get; }
        public DateTime? UpdatedDate { get; }
        public PaintCostItemInfo Model { get; }
        public PaintCostItemInfoViewModel(PaintCostItemInfo costItemInfo)
        {
            this.Model = costItemInfo;
            this.Sequence = costItemInfo.Sequence;
            this.Code = costItemInfo.Code;
            this.Name = costItemInfo.Name;
            this.UnitPrice = $"{costItemInfo.UnitPrice:C}";
            this.UpdatedDate = costItemInfo.UpdatedDate;
        }
    }
}
