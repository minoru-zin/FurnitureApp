using FurnitureApp.Repository.CostItemInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00400
{
    public class CostItemInfoViewModel
    {
        public string CategoryName { get; }
        public string Name { get; }
        public int? Sequence { get; }
        public string UnitPrice { get; }
        public DateTime? UpdatedDate { get; }
        public CostItemInfo Model { get; }
        public CostItemInfoViewModel(CostItemInfo costItemInfo)
        {
            this.Model = costItemInfo;
            this.CategoryName = costItemInfo.CategoryName;
            this.Name = costItemInfo.Name;
            this.Sequence = costItemInfo.Sequence;
            this.UnitPrice = $"{costItemInfo.UnitPrice:C}";
            this.UpdatedDate = costItemInfo.UpdatedDate;
        }
    }
}
