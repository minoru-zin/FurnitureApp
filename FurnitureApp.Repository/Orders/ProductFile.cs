using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class ProductFile
    {
        private int? id;

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
        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }
        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        public ProductFile Clone()
        {
            return (ProductFile)MemberwiseClone();
        }
        public bool IsSame(ProductFile o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(ProductFile.Id), nameof(ProductFile.ProductId) };

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
