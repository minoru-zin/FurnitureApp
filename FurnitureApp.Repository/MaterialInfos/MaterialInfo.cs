using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.MaterialInfos
{
    public class MaterialInfo
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
        private double? thickness;
        /// <summary>
        /// 厚さ
        /// </summary>
        public double? Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
        private int? pasteUnitPrice;
        /// <summary>
        /// 貼り単価
        /// </summary>
        public int? PasteUnitPrice
        {
            get { return pasteUnitPrice; }
            set { pasteUnitPrice = value; }
        }
        private CutType cutType;
        /// <summary>
        /// カットタイプ
        /// </summary>
        public CutType CutType
        {
            get { return cutType; }
            set { cutType = value; }
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
        public MaterialInfo Clone()
        {
            return (MaterialInfo)MemberwiseClone();
        }
    }

    public enum CutType
    {
        /// <summary>
        /// 通常
        /// </summary>
        Normal,
        /// <summary>
        /// LVL
        /// </summary>
        Lvl,
    }

}
