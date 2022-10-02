using FurnitureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00300
{
    public class BoardSizeViewModel
    {
        public string BoardType { get;}
        public double Width { get; }
        public double Length { get; }
        public double Thickness { get; }
        public int Quantity { get; }
        
        private CommonData cd = CommonData.GetInstance();
        public BoardSizeViewModel(BoardSize boardSize)
        {
            this.BoardType = this.cd.BoardTypes.FirstOrDefault(x => x.Code == boardSize.BoardType)?.DisplayName;
            
            if (boardSize.Quantity == 0) { return; }
            
            this.Width = boardSize.Width;
            this.Length = boardSize.Length;
            this.Thickness = boardSize.Thickness;
            this.Quantity = boardSize.Quantity;
        }
    }
}
