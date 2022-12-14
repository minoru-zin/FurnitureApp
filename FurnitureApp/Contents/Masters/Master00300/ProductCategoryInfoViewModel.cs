using FurnitureApp.Repository.ProductCategoryInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00300
{
    public class ProductCategoryInfoViewModel
    {
        public int? Sequence { get; }
        public int? Code { get; }
        public string Name { get; }
        public DateTime? UpdatedDate { get; }
        public ProductCategoryInfo Model { get; }

        public ProductCategoryInfoViewModel(ProductCategoryInfo m)
        {
            this.Model = m;
            this.Sequence = m.Sequence;
            this.Code = m.Code;
            this.Name = m.Name;
            this.UpdatedDate = m.UpdatedDate;
        }
    }
}
