using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.MaterialSizeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00200
{
    public class MaterialSizeInfoViewModel
    {
        public string MaterialName { get; }
        public string Name { get; }
        public double? Length { get; }
        public double? Width { get; }
        public int? UnitPrice { get; }
        public MaterialSizeInfo Model { get; }
        public MaterialSizeInfoViewModel(MaterialSizeInfo m, Dictionary<int?, MaterialInfo> materialDict)
        {
            this.Model = m;
            this.MaterialName = materialDict.GetValueOrDefault(m.MaterialInfoId)?.Name;
            this.Name = m.Name;
            this.Length = m.Length;
            this.Width = m.Width;
            this.UnitPrice = m.UnitPrice;
        }
    }
}
