using System;
using System.Collections.Generic;
using System.Text;

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
        public List<BoardLayer> BoardLayers { get; set; } = new List<BoardLayer>();
    }
    public enum BoardType
    {
        /// <summary>
        /// 天板
        /// </summary>
        TopBoard,
    }
}
