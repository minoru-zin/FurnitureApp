using System;
using System.Collections.Generic;
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
        private int? productCategoryCode;
        /// <summary>
        /// 製品カテゴリ
        /// </summary>
        public int? ProductCategoryCode
        {
            get { return productCategoryCode; }
            set { productCategoryCode = value; }
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
        private int? quantity;
        /// <summary>
        /// 数量
        /// </summary>
        public int? Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }
        private double? bodyWidth;
        /// <summary>
        /// 本体幅
        /// </summary>
        public double? BodyWidth
        {
            get { return bodyWidth; }
            set { bodyWidth = value; }
        }
        private double? bodyDepth;
        /// <summary>
        /// 本体奥行
        /// </summary>
        public double? BodyDepth
        {
            get { return bodyDepth; }
            set { bodyDepth = value; }
        }
        private double? bodyHeight;
        /// <summary>
        /// 本体高さ
        /// </summary>
        public double? BodyHeight
        {
            get { return bodyHeight; }
            set { bodyHeight = value; }
        }
        private double? fillerL;
        /// <summary>
        /// フィラー左
        /// </summary>
        public double? FillerL
        {
            get { return fillerL; }
            set { fillerL = value; }
        }
        private double? fillerR;
        /// <summary>
        /// フィラー右
        /// </summary>
        public double? FillerR
        {
            get { return fillerR; }
            set { fillerR = value; }
        }
        private double? topBoardHikae;
        /// <summary>
        /// 天板控え
        /// </summary>
        public double? TopBoardHikae
        {
            get { return topBoardHikae; }
            set { topBoardHikae = value; }
        }

        /// <summary>
        /// 部材リスト
        /// </summary>
        public List<Board> Boards { get; set; } = new List<Board>();

    }
}
