using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.CostItemInfos
{
    public class CostItemInfo
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
        private int? unitPrice;
        /// <summary>
        /// 単価
        /// </summary>
        public int? UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
        private string categoryName;
        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        private DateTime? updatedDate;
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }

        public CostItemInfo Clone()
        {
            return (CostItemInfo)MemberwiseClone();
        }
    }
}
