using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class Product
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
        private int? orderId;
        /// <summary>
        /// 受注Id
        /// </summary>
        public int? OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }
        private int? productCategoryInfoCode;
        /// <summary>
        /// 製品カテゴリ
        /// </summary>
        public int? ProductCategoryInfoCode
        {
            get { return productCategoryInfoCode; }
            set { productCategoryInfoCode = value; }
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
        private int quantity = 1;
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private double bodyWidth;
        /// <summary>
        /// 本体幅
        /// </summary>
        public double BodyWidth
        {
            get { return bodyWidth; }
            set { bodyWidth = value; }
        }
        private double bodyDepth;
        /// <summary>
        /// 本体奥行
        /// </summary>
        public double BodyDepth
        {
            get { return bodyDepth; }
            set { bodyDepth = value; }
        }
        private double bodyHeight;
        /// <summary>
        /// 本体高さ
        /// </summary>
        public double BodyHeight
        {
            get { return bodyHeight; }
            set { bodyHeight = value; }
        }
        private double fillerL;
        /// <summary>
        /// フィラー左
        /// </summary>
        public double FillerL
        {
            get { return fillerL; }
            set { fillerL = value; }
        }
        private double fillerR;
        /// <summary>
        /// フィラー右
        /// </summary>
        public double FillerR
        {
            get { return fillerR; }
            set { fillerR = value; }
        }
        private double lvlWidth = 40;
        /// <summary>
        /// LVL
        /// フラッシュ構造の枠の幅
        /// </summary>
        public double LvlWidth
        {
            get { return lvlWidth; }
            set { lvlWidth = value; }
        }
        private double ankoPitch = 300;
        /// <summary>
        /// アンコピッチ
        /// </summary>
        public double AnkoPitch
        {
            get { return ankoPitch; }
            set { ankoPitch = value; }
        }
        private double daiwaHeight = 60;
        /// <summary>
        /// 台輪（H）
        /// </summary>
        public double DaiwaHeight
        {
            get { return daiwaHeight; }
            set { daiwaHeight = value; }
        }

        private double tobiraTenitaSukima;
        /// <summary>
        /// 扉：天板隙間
        /// </summary>
        public double TobiraTenitaSukima
        {
            get { return tobiraTenitaSukima; }
            set { tobiraTenitaSukima = value; }
        }

        private double tobiraTenitaHikae;
        /// <summary>
        /// 扉：天板控え
        /// </summary>
        public double TobiraTenitaHikae
        {
            get { return tobiraTenitaHikae; }
            set { tobiraTenitaHikae = value; }
        }

        private double tobiraGawaitaMokuji = 4;
        /// <summary>
        /// 扉：側板目地
        /// </summary>
        public double TobiraGawaitaMokuji
        {
            get { return tobiraGawaitaMokuji; }
            set { tobiraGawaitaMokuji = value; }
        }

        private double tobiraKanMokuji = 3;
        /// <summary>
        /// 扉間目地
        /// </summary>
        public double TobiraKanMokuji
        {
            get { return tobiraKanMokuji; }
            set { tobiraKanMokuji = value; }
        }

        private double shikiriitaGawaitaHikae;
        /// <summary>
        /// 仕切板：側板控え
        /// </summary>
        public double ShikiriitaGawaitaHikae
        {
            get { return shikiriitaGawaitaHikae; }
            set { shikiriitaGawaitaHikae = value; }
        }
        private double tanaitaGawaitaHikae;
        /// <summary>
        /// 棚板：側板控え
        /// </summary>
        public double TanaitaGawaitaHikae
        {
            get { return tanaitaGawaitaHikae; }
            set { tanaitaGawaitaHikae = value; }
        }
        private double koguchiPasteUnitPrice = 0.05;
        /// <summary>
        /// 木口テープ単価
        /// </summary>
        public double KoguchiPasteUnitPrice
        {
            get { return koguchiPasteUnitPrice; }
            set { koguchiPasteUnitPrice = value; }
        }
        private double finishCutUnitPrice = 0.04;
        /// <summary>
        /// 仕上げカット単価
        /// </summary>
        public double FinishCutUnitPrice
        {
            get { return finishCutUnitPrice; }
            set { finishCutUnitPrice = value; }
        }

        private double finishMargin = 5;
        /// <summary>
        /// 製作幅
        /// </summary>
        public double FinishMargin
        {
            get { return finishMargin; }
            set { finishMargin = value; }
        }
        private DateTime? updatedDate;

        public DateTime? UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }


        /// <summary>
        /// 部材リスト
        /// </summary>
        public List<Board> Boards { get; set; } = new List<Board>();
        /// <summary>
        /// 板コストリスト
        /// </summary>
        public List<BoardCost> BoardCosts { get; set; } = new List<BoardCost>();
        /// <summary>
        /// 木口貼りコストリスト
        /// </summary>
        public List<KoguchiPasteCost> KoguchiPasteCosts { get; set; } = new List<KoguchiPasteCost>();
        /// <summary>
        /// 仕上げカットコストリスト
        /// </summary>
        public List<FinishCutCost> FinishCutCosts { get; set; } = new List<FinishCutCost>();
        /// <summary>
        /// 化粧板貼りコストリスト
        /// </summary>
        public List<MakeupBoardPasteCost> MakeupBoardPasteCosts { get; set; } = new List<MakeupBoardPasteCost>();
        /// <summary>
        /// 塗装コスト
        /// </summary>
        public List<PaintCost> PaintCosts { get; set; } = new List<PaintCost>();
        /// <summary>
        /// コストリスト
        /// </summary>
        public List<Cost> Costs { get; set; } = new List<Cost>();
        /// <summary>
        /// 画像リスト
        /// </summary>
        public List<ProductFileEx> ProductFiles { get; set; } = new List<ProductFileEx>();

        public static List<string> GetIgnorePropertyNames()
        {
            return new List<string>
            {
                nameof(Product.Boards),
                nameof(Product.BoardCosts),
                nameof(Product.KoguchiPasteCosts),
                nameof(Product.FinishCutCosts),
                nameof(Product.MakeupBoardPasteCosts),
                nameof(Product.PaintCosts),
                nameof(Product.Costs),
                nameof(Product.ProductFiles),
            };
        }
        public Product Clone()
        {
            var clone = (Product)MemberwiseClone();
            clone.Boards = this.Boards.Select(x => x.Clone()).ToList();
            clone.BoardCosts = this.BoardCosts.Select(x => x.Clone()).ToList();
            clone.KoguchiPasteCosts = this.KoguchiPasteCosts.Select(x => x.Clone()).ToList();
            clone.FinishCutCosts = this.FinishCutCosts.Select(x => x.Clone()).ToList();
            clone.MakeupBoardPasteCosts = this.MakeupBoardPasteCosts.Select(x => x.Clone()).ToList();
            clone.PaintCosts = this.PaintCosts.Select(x => x.Clone()).ToList();
            clone.Costs = this.Costs.Select(x => x.Clone()).ToList();
            clone.ProductFiles = this.ProductFiles.Select(x => x.Clone()).ToList();
            return clone;
        }

        public int GetUnitPrice()
        {
            var boardCost = (this.BoardCosts.Sum(x => x.TotalAmount) ?? 0) / this.Quantity;
            var koguchiCost = this.KoguchiPasteCosts.Sum(x => x.TotalAmount) ?? 0;
            var finishCutCost = this.FinishCutCosts.Sum(x => x.TotalAmount) ?? 0;
            var makeCost = this.MakeupBoardPasteCosts.Sum(x => x.TotalAmount) ?? 0;
            var paintCost = this.PaintCosts.Sum(x => x.TotalAmount) ?? 0;
            var cost = this.Costs.Sum(x => x.TotalAmount) ?? 0;

            return boardCost + koguchiCost + finishCutCost + makeCost + paintCost + cost;
        }
        public bool IsSame(Product o)
        {
            try
            {
                if (!Utility.Reflector.IsSame(this, o, GetIgnorePropertyNames().ToHashSet())) { return false; }

                if (this.Boards.Count != o.Boards.Count) { return false; }

                for (var i = 0; i < this.Boards.Count; i++)
                {
                    if (!this.Boards[i].IsSame(o.Boards[i])) { return false; }
                }
                if (this.BoardCosts.Count != o.BoardCosts.Count) { return false; }

                for (var i = 0; i < this.BoardCosts.Count; i++)
                {
                    if (!this.BoardCosts[i].IsSame(o.BoardCosts[i])) { return false; }
                }
                if (this.KoguchiPasteCosts.Count != o.KoguchiPasteCosts.Count) { return false; }

                for (var i = 0; i < this.KoguchiPasteCosts.Count; i++)
                {
                    if (!this.KoguchiPasteCosts[i].IsSame(o.KoguchiPasteCosts[i])) { return false; }
                }
                if (this.FinishCutCosts.Count != o.FinishCutCosts.Count) { return false; }

                for (var i = 0; i < this.FinishCutCosts.Count; i++)
                {
                    if (!this.FinishCutCosts[i].IsSame(o.FinishCutCosts[i])) { return false; }
                }
                if (this.MakeupBoardPasteCosts.Count != o.MakeupBoardPasteCosts.Count) { return false; }

                for (var i = 0; i < this.MakeupBoardPasteCosts.Count; i++)
                {
                    if (!this.MakeupBoardPasteCosts[i].IsSame(o.MakeupBoardPasteCosts[i])) { return false; }
                }
                if (this.PaintCosts.Count != o.PaintCosts.Count) { return false; }

                for (var i = 0; i < this.PaintCosts.Count; i++)
                {
                    if (!this.PaintCosts[i].IsSame(o.PaintCosts[i])) { return false; }
                }
                if (this.Costs.Count != o.Costs.Count) { return false; }

                for (var i = 0; i < this.Costs.Count; i++)
                {
                    if (!this.Costs[i].IsSame(o.Costs[i])) { return false; }
                }
                if (this.ProductFiles.Count != o.ProductFiles.Count) { return false; }

                for (var i = 0; i < this.ProductFiles.Count; i++)
                {
                    if (!this.ProductFiles[i].IsSame(o.ProductFiles[i])) { return false; }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
