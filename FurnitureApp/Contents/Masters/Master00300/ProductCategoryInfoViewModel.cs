using FurnitureApp.Repository.ProductCategoryInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Masters.Master00300
{
    public class ProductCategoryInfoViewModel
    {
        public int? Sequence { get; }
        public string Name { get; }
        public ProductCategoryInfo Model { get; }

        public ProductCategoryInfoViewModel(ProductCategoryInfo m)
        {
            this.Model = m;
            this.Sequence = m.Sequence;
            this.Name = m.Name;
        }
    }
}
