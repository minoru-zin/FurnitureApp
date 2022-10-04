using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.PaintCostItemInfos
{
    public class PaintCostItemInfo
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
        private int? code;
        /// <summary>
        /// コード
        /// </summary>
        public int? Code
        {
            get { return code; }
            set { code = value; }
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

        public PaintCostItemInfo Clone()
        {
            return (PaintCostItemInfo)MemberwiseClone();
        }
    }
}
