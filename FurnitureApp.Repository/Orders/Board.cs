using FurnitureApp.Repository.MaterialInfos;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using WinCopies.Util;

namespace FurnitureApp.Repository.Orders
{
    public class Board
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
        /// 製品Id
        /// </summary>
        public int? ProductId
        {
            get { return productId; }
            set { productId = value; }
        }
        private BoardType? boardCode;
        /// <summary>
        /// 部材コード
        /// </summary>
        public BoardType? BoardCode
        {
            get { return boardCode; }
            set { boardCode = value; }
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
        private KoguchiMakeupArea koguchiKeshouAreaCode;

        public KoguchiMakeupArea KoguchiKeshouAreaCode
        {
            get { return koguchiKeshouAreaCode; }
            set { koguchiKeshouAreaCode = value; }
        }
        private int? paintCostItemInfoCode;

        public int? PaintCostItemInfoCode
        {
            get { return paintCostItemInfoCode; }
            set { paintCostItemInfoCode = value; }
        }
        private PaintArea paintArea;

        public PaintArea PaintArea
        {
            get { return paintArea; }
            set { paintArea = value; }
        }

        public List<BoardLayer> BoardLayers { get; set; } = new List<BoardLayer>();
        public static List<string> GetIgnorePropertyNames()
        {
            return new List<string>
            {
                nameof(Board.BoardLayers),
            };
        }
        public double GetThickness(Dictionary<int?, MaterialInfo> materialInfoDict)
        {
            return this.BoardLayers.Sum(x => materialInfoDict.GetValueOrDefault(x.MaterialInfoCode)?.Thickness ?? 0);
        }
        public Board Clone()
        {
            var clone = (Board)MemberwiseClone();
            clone.BoardLayers = this.BoardLayers.Select(x => x.Clone()).ToList();
            return clone;
        }
        public bool IsSame(Board o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(Board.Id), nameof(Board.ProductId) };
                ignores.AddRange(GetIgnorePropertyNames());

                if (!Utility.Reflector.IsSame(this, o, ignores)) { return false; }

                if (this.BoardLayers.Count != o.BoardLayers.Count) { return false; }

                for (var i = 0; i < this.BoardLayers.Count; i++)
                {
                    if (!this.BoardLayers[i].IsSame(o.BoardLayers[i])) { return false; }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public enum BoardType
    {
        /// <summary>
        /// 天板
        /// </summary>
        Tenita,
        /// <summary>
        /// 天下
        /// </summary>
        Tenshita,
        /// <summary>
        /// 側板左
        /// </summary>
        GawaitaLeft,
        /// <summary>
        /// 側板右
        /// </summary>
        GawaitaRight,
        /// <summary>
        /// 仕切板
        /// </summary>
        Shikiriita,
        /// <summary>
        /// 棚板
        /// </summary>
        Tanaita,
        /// <summary>
        /// 扉
        /// </summary>
        Tobira,
        /// <summary>
        /// 背板
        /// </summary>
        Seita,
        /// <summary>
        /// 地板
        /// </summary>
        Jiita,
        /// <summary>
        /// 台輪正面
        /// </summary>
        DaiwaFront,
        /// <summary>
        /// 台輪後ろ
        /// </summary>
        DaiwaBack,
        /// <summary>
        /// 台輪左
        /// </summary>
        DaiwaLeft,
        /// <summary>
        /// 台輪右
        /// </summary>
        DaiwaRight,
    }
    public enum KoguchiMakeupArea
    {
        /// <summary>
        /// なし
        /// </summary>
        Nashi,
        /// <summary>
        /// 正面
        /// </summary>
        Front,
        /// <summary>
        /// 左右
        /// </summary>
        BothSide,
        /// <summary>
        /// 正面,左or右
        /// </summary>
        FrontAndOneSide,
        /// <summary>
        /// 正面左右
        /// </summary>
        FrontAndBothSide,
        /// <summary>
        /// 前面
        /// </summary>
        All,
    }
}
