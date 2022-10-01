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
        public double FinishWidth { get; }
        public double FinishLength { get; }
        public double FinishThickness { get; }
        public int Quantity { get; }
        private CommonData cd = CommonData.GetInstance();
        public BoardSizeViewModel(BoardSize boardSize, double finishMargin)
        {
            this.BoardType = this.cd.BoardTypes.FirstOrDefault(x => x.Code == boardSize.BoardType)?.DisplayName;
            
            if (boardSize.Quantity == 0) { return; }
            
            this.Width = boardSize.Width + finishMargin;
            this.Length = boardSize.Length + finishMargin;
            this.FinishWidth = boardSize.Width;
            this.FinishLength = boardSize.Length;
            this.FinishThickness = boardSize.Thickness;
            this.Quantity = boardSize.Quantity;
        }
    }
}
