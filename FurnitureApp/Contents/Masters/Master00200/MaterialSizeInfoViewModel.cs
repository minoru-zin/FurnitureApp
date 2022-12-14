using FurnitureApp.Models;
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
        public string UnitPrice { get; }
        public DateTime? UpdatedDate { get; }
        public MaterialSizeInfo Model { get; }
        private CommonData cd = CommonData.GetInstance();
        public MaterialSizeInfoViewModel(MaterialSizeInfo m)
        {
            this.Model = m;
            this.MaterialName = this.cd.MaterialInfoDict.GetValueOrDefault(m.MaterialInfoCode)?.Name;
            this.Name = m.Name;
            this.Length = m.Length;
            this.Width = m.Width;
            this.UnitPrice = $"{m.UnitPrice:C}";
            this.UpdatedDate = m.UpdatedDate;
        }
    }
}
