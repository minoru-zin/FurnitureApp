using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00300
{
    public class CutSizeViewModel
    {
        public string MaterialName { get; }
        public double Width { get; }
        public double Length { get; }
        public int Quantity { get; }
        public CutSizeViewModel(CutSize cutSize)
        {
            this.MaterialName = cutSize.MaterialName;
            this.Width = cutSize.Width;
            this.Length = cutSize.Length;
            this.Quantity = cutSize.Quantity;
        }
    }
}
