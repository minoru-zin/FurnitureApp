using System;
using System.Collections.Generic;
using System.Text;
using WinCopies.Util;

namespace FurnitureApp.Repository.Orders
{
    public class KoguchiPasteCost
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

        private BoardType? boardTypeCode;
        /// <summary>
        /// 板タイプ
        /// </summary>
        public BoardType? BoardTypeCode
        {
            get { return boardTypeCode; }
            set { boardTypeCode = value; }
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
        private KoguchiMakeupArea? koguchiMakeupAreaCode;
        /// <summary>
        /// 木口貼り箇所
        /// </summary>
        public KoguchiMakeupArea? KoguchiMakeupAreaCode
        {
            get { return koguchiMakeupAreaCode; }
            set { koguchiMakeupAreaCode = value; }
        }
        private double? unitLength;
        /// <summary>
        /// 一個あたりの長さ
        /// </summary>
        public double? UnitLength
        {
            get { return unitLength; }
            set { unitLength = value; }
        }
        private double? unitPrice;
        /// <summary>
        /// 単価
        /// </summary>
        public double? UnitPrice
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

        public KoguchiPasteCost Clone()
        {
            return (KoguchiPasteCost)MemberwiseClone();
        }
        public bool IsSame(KoguchiPasteCost o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(KoguchiPasteCost.Id), nameof(KoguchiPasteCost.ProductId) };

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
