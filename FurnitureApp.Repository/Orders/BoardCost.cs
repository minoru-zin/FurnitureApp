using System;
using System.Collections.Generic;
using System.Text;
using WinCopies.Util;

namespace FurnitureApp.Repository.Orders
{
    public class BoardCost
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
        private int? productId;
        /// <summary>
        /// ProductId
        /// </summary>
        public int? ProductId
        {
            get { return productId; }
            set { productId = value; }
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
        private int? quantity;
        /// <summary>
        /// 数量
        /// </summary>
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private int? totalAmount;
        /// <summary>
        /// 総額
        /// </summary>
        public int? TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public BoardCost Clone()
        {
            return (BoardCost)MemberwiseClone();
        }
        public bool IsSame(BoardCost o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(BoardCost.Id), nameof(BoardCost.ProductId) };
                
                if (!Utility.Reflector.IsSame(this, o, ignores)) { return false; }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
