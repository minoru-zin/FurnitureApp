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
        public string Width { get; }
        public string Length { get; }
        public string Thickness { get; }
        public int Quantity { get; }
        
        private CommonData cd = CommonData.GetInstance();
        public BoardSizeViewModel(BoardSize boardSize)
        {
            this.BoardType = this.cd.BoardTypes.FirstOrDefault(x => x.Code == boardSize.BoardType)?.DisplayName;
            
            if (boardSize.Quantity == 0) { return; }
            
            this.Width = $"{boardSize.Width:0.0}".Replace(".0", "");
            this.Length = $"{boardSize.Length:0.0}".Replace(".0", "");
            this.Thickness = $"{boardSize.Thickness:0.0}".Replace(".0", "");
            this.Quantity = boardSize.Quantity;
        }
    }
}
