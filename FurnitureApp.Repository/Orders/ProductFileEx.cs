using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Orders
{
    public class ProductFileEx : ProductFile
    {
        public string SourceFilePath { get; set; }
        public bool IsDeleted { get; set; }

        public new ProductFileEx Clone()
        {
            return (ProductFileEx)MemberwiseClone();
        }
        public bool IsSame(ProductFileEx o)
        {
            try
            {
                var ignores = new HashSet<string> { nameof(ProductFileEx.Id), nameof(ProductFileEx.ProductId) };

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
