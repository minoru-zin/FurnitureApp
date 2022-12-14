using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.MaterialSizeInfos
{
    public class MaterialSizeInfo
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
        private int? materialInfoCode;
        /// <summary>
        /// 素材コード
        /// </summary>
        public int? MaterialInfoCode
        {
            get { return materialInfoCode; }
            set { materialInfoCode = value; }
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
        private double? length;
        /// <summary>
        /// 縦
        /// </summary>
        public double? Length
        {
            get { return length; }
            set { length = value; }
        }
        private double? width;
        /// <summary>
        /// 横
        /// </summary>
        public double? Width
        {
            get { return width; }
            set { width = value; }
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
        private DateTime? updatedDate;
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
        public MaterialSizeInfo Clone()
        {
            return (MaterialSizeInfo)MemberwiseClone();
        }
    }
}
