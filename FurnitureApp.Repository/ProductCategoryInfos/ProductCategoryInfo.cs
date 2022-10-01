using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.ProductCategoryInfos
{
    public class ProductCategoryInfo
    {
        private int? id;
        /// <summary>
        /// Id
        /// </summary>
        public int? Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int? sequence;
        /// <summary>
        /// 順番
        /// </summary>
        public int? Sequence
        {
            get { return sequence; }
            set { sequence = value; }
        }
        public ProductCategoryInfo Clone()
        {
            return (ProductCategoryInfo)MemberwiseClone();
        }
    }
}
