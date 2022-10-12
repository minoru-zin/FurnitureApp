using System;
using System.Collections.Generic;
using System.Linq;
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
        private DateTime? createdDate;
        /// <summary>
        /// 作成日
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }
        private string name;
        /// <summary>
        /// 物件名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string clientName;
        /// <summary>
        /// 提出先
        /// </summary>
        public string ClientName
        {
            get { return clientName; }
            set { clientName = value; }
        }

        private DateTime? deliveryDate;
        /// <summary>
        /// 納品日
        /// </summary>
        public DateTime? DeliveryDate
        {
            get { return deliveryDate; }
            set { deliveryDate = value; }
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

        public Order Clone()
        {
            var clone = (Order)MemberwiseClone();
            clone.Products = this.Products.Select(x => x.Clone()).ToList();

            return clone;
        }
        public static List<string> GetIgnorePropertyNames()
        {
            return new List<string>
            {
                nameof(Order.Products)
            };
        }
        public bool IsSame(Order o)
        {
            try
            {
                var ignorePropertyNames = new HashSet<string> { nameof(Order.Products) };

                if (!Utility.Reflector.IsSame(this, o, ignorePropertyNames)) { return false; }

                if (this.Products.Count != o.Products.Count) { return false; }

                for (var i = 0; i < this.Products.Count; i++)
                {
                    if (!this.Products[i].IsSame(o.Products[i])) { return false; }
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
