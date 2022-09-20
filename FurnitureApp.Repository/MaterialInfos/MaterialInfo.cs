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

    }
}
