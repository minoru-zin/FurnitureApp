using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00100
{
    public class DisplayBoardLayerViewModel
    {
        public string MaterialName { get; }
        public double? Thickness { get; }
        public double? PasteUnitPrice { get; }
        public string MokumeDirection { get; }

        private CommonData cd = CommonData.GetInstance();
        public DisplayBoardLayerViewModel(BoardLayer boardLayer)
        {
            var materialInfo = this.cd.MaterialInfoDict.GetValueOrDefault(boardLayer.MaterialInfoId);
            this.MaterialName = materialInfo?.Name;
            this.Thickness = materialInfo?.Thickness;
            this.PasteUnitPrice = boardLayer.PasteUnitPrice;
            this.MokumeDirection = this.cd.MokumeDirectionTypes.First(x => x.Code == boardLayer.MokumeDirectionCode).DisplayName;
        }
    }
}
