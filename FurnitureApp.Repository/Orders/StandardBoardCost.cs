using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class StandardBoardCost
    {
        private int? id;
        /// <summary>
        /// Id
        /// </summary>
        public int? Id
        {
            get { return id; }
            set { Id = value; }
        }
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { Name = value; }
        }
        private double? length;
        /// <summary>
        /// 縦
        /// </summary>
        public double? Length
        {
            get { return length; }
            set { Length = value; }
        }
        private double? width;
        /// <summary>
        /// 横
        /// </summary>
        public double? Width
        {
            get { return width; }
            set { Width = value; }
        }
        private int? unitPrice;
        /// <summary>
        /// 単価
        /// </summary>
        public int? UnitPrice
        {
            get { return unitPrice; }
            set { UnitPrice = value; }
        }
        private int? quantity;
        /// <summary>
        /// 数量
        /// </summary>
        public int? Quantity
        {
            get { return quantity; }
            set { Quantity = value; }
        }

    }
}
