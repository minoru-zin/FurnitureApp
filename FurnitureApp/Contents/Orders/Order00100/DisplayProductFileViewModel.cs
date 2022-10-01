using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayProductFileViewModel
    {
        public string DisplayName { get; }

        public ProductFile Model { get; }

        public DisplayProductFileViewModel(ProductFile productFile)
        {
            this.Model = productFile;
            this.DisplayName = productFile.DisplayName;
        }
    }
}
