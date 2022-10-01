using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayBoardViewModel
    {
        public string BoardName { get; }
        public double? Length { get; }
        public double? Width { get; }
        public double? Thickness { get; }
        public int? Quantity { get; }
        public string KoguchiPasteArea { get; }

        private CommonData cd = CommonData.GetInstance();

        public Board Model { get; }
        public DisplayBoardViewModel(Board board, BoardSize boardSize)
        {
            this.Model = board;
            this.BoardName = this.cd.BoardTypes.First(x => x.Code == board.BoardCode).DisplayName;
            this.Length = boardSize?.Length;
            this.Width = boardSize?.Width;
            this.Thickness = board.GetThickness(this.cd.MaterialInfoDict);
            this.Quantity = board.Quantity;
            this.KoguchiPasteArea = this.cd.KoguchiKeshouAreas.First(x => x.Code == board.KoguchiKeshouAreaCode).DisplayName;
        }
    }
}
