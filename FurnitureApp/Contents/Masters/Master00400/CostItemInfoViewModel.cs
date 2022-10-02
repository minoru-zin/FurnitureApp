using FurnitureApp.Repository.CostItemInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00400
{
    public class CostItemInfoViewModel
    {
        public string Name { get; }
        public int? Sequence { get; }
        public string UnitPrice { get; }
        public CostItemInfo Model { get; }
        public CostItemInfoViewModel(CostItemInfo costItemInfo)
        {
            this.Model = costItemInfo;
            this.Name = costItemInfo.Name;
            this.Sequence = costItemInfo.Sequence;
            this.UnitPrice = $"{costItemInfo.UnitPrice:C}";
        }
    }
}
