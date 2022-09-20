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
        private int? partId;
        /// <summary>
        /// 部材Id
        /// </summary>
        public int? PartId
        {
            get { return partId; }
            set { partId = value; }
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
        private int? materialInfoId;
        /// <summary>
        /// 素材Id
        /// </summary>
        public int? MaterialInfoId
        {
            get { return materialInfoId; }
            set { materialInfoId = value; }
        }

    }
}
