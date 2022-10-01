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
    }
}
