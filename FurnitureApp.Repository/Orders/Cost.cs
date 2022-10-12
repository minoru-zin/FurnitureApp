using System;
using System.Collections.Generic;
using System.Text;
using WinCopies.Util;

namespace FurnitureApp.Repository.Orders
{
    public class Cost
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

        public Cost Clone()
        {
            return (Cost)MemberwiseClone();
        }
        public bool IsSame(Cost o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(Cost.Id), nameof(Cost.ProductId) };
                
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
