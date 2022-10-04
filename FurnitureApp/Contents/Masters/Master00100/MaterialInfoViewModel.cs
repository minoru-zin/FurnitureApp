using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00100
{
    public class MaterialInfoViewModel
    {
        public int? Sequence { get; }
        public int? Code { get; }
        public string Name { get; }
        public double? Thickness { get; }
        public int? PasteUnitPrice { get; }
        public string CutType { get; }
        public MaterialInfo Model { get; }
        private CommonData cd = CommonData.GetInstance();
        public MaterialInfoViewModel(MaterialInfo m)
        {
            this.Model = m;
            this.Sequence = m.Sequence;
            this.Code = m.Code;
            this.Name = m.Name;
            this.Thickness = m.Thickness;
            this.PasteUnitPrice = m.PasteUnitPrice;
            this.CutType = this.cd.CutTypes.FirstOrDefault(x => x.Code == m.CutType)?.DisplayName;
        }
    }
}
