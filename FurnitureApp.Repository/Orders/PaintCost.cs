using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class PaintCost
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
        private double? thickness;
        /// <summary>
        /// 厚さ
        /// </summary>
        public double? Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        private string paintName;
        /// <summary>
        /// 塗装名
        /// </summary>
        public string PaintName
        {
            get { return paintName; }
            set { paintName = value; }
        }

        private PaintArea paintArea;
        /// <summary>
        /// 塗装箇所
        /// </summary>
        public PaintArea PaintArea
        {
            get { return paintArea; }
            set { paintArea = value; }
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

        public PaintCost Clone()
        {
            return (PaintCost)MemberwiseClone();
        }
        public bool IsSame(PaintCost o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(PaintCost.Id), nameof(PaintCost.ProductId) };

                if (!Utility.Reflector.IsSame(this, o, ignores)) { return false; }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public enum PaintArea
    {
        /// <summary>
        /// 無し
        /// </summary>
        Nashi,
        /// <summary>
        /// 片面_木口(正面)
        /// </summary>
        OneSide_KoguchiFront,
        /// <summary>
        /// 片面_木口(左右)
        /// </summary>
        OneSide_KoguchiBothSide,
        /// <summary>
        /// 片面_木口(正面_左or右)
        /// </summary>
        OneSide_KoguchiFrontAndOneSide,
        /// <summary>
        /// 片面_木口(正面_左右)
        /// </summary>
        OneSide_KoguchiFrontAndBothSide,
        /// <summary>
        /// 片面_木口(全面)
        /// </summary>
        OneSide_KoguchiAll,
        /// <summary>
        /// 両面_木口(正面)
        /// </summary>
        BothSide_KoguchiFront,
        /// <summary>
        /// 両面_木口(左右)
        /// </summary>
        BothSide_KoguchiBothSide,
        /// <summary>
        /// 両面_木口(正面_左or右)
        /// </summary>
        BothSide_KoguchiFrontAndOneSide,
        /// <summary>
        /// 両面_木口(正面_左右)
        /// </summary>
        BothSide_KoguchiFrontAndBothSide,
        /// <summary>
        /// 両面_木口(全面)
        /// </summary>
        BothSide_KoguchiAll,
        /// <summary>
        /// 木口(正面)
        /// </summary>
        KoguchiFront,
        /// <summary>
        /// 木口(左右)
        /// </summary>
        KoguchiBothSide,
        /// <summary>
        /// 木口(正面_左or右)
        /// </summary>
        KoguchiFrontAndOneSide,
        /// <summary>
        /// 木口(正面_左右)
        /// </summary>
        KoguchiFrontAndBothSide,
        /// <summary>
        /// 木口(全面)
        /// </summary>
        KoguchiAll,
    }
}
