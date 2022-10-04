using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class BoardLayer
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
        private int? boardId;
        /// <summary>
        /// 部材Id
        /// </summary>
        public int? BoardId
        {
            get { return boardId; }
            set { boardId = value; }
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
        private int? materialInfoCode;
        /// <summary>
        /// 素材Id
        /// </summary>
        public int? MaterialInfoCode
        {
            get { return materialInfoCode; }
            set { materialInfoCode = value; }
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
        private MokumeDirectionType mokumeDirectionCode;
        /// <summary>
        /// 木目方向
        /// </summary>
        public MokumeDirectionType MokumeDirectionCode
        {
            get { return mokumeDirectionCode; }
            set { mokumeDirectionCode = value; }
        }


        public BoardLayer Clone()
        {
            return (BoardLayer)MemberwiseClone();
        }
    }
    /// <summary>
    /// 木目方向
    /// </summary>
    public enum MokumeDirectionType
    {
        /// <summary>
        /// 無し
        /// </summary>
        Nashi,
        /// <summary>
        /// 縦
        /// </summary>
        Length,
        /// <summary>
        /// 横
        /// </summary>
        Width,
    }
}
