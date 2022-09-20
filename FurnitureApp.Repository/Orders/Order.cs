using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class Order
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
        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string remarks;
        /// <summary>
        /// 備考
        /// </summary>
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        /// <summary>
        /// 製品リスト
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();
        /// <summary>
        /// 規格板費用リスト
        /// </summary>
        public List<StandardBoardCost> StandardPlateCosts { get; set; } = new List<StandardBoardCost>();
        /// <summary>
        /// カット費用リスト
        /// </summary>
        public List<CuttingCost> CuttingCosts { get; set; } = new List<CuttingCost>();
        /// <summary>
        /// 費用リスト
        /// </summary>
        public List<Cost> Costs { get; set; } = new List<Cost>();
    }
}
