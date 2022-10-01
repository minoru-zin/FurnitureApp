using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00300
{
    public class BoardLayerViewModel
    {
        public MaterialInfo MaterialInfo { get; set; }
        public string PasteUnitPrice { get; set; }
        public MokumeDirectionType MokumeDirectionType { get; set; }
        public BoardLayer Model
        {
            get
            {
                return new BoardLayer
                {
                    MaterialInfoId = this.MaterialInfo.Id,
                    PasteUnitPrice = Utility.NumberFormatter.GetNullInt(this.PasteUnitPrice),
                    MokumeDirectionCode = MokumeDirectionType,
                };
            }
        }


        public BoardLayerViewModel(BoardLayer boardLayer, MaterialInfo materialInfo)
        {
            this.MaterialInfo = materialInfo;
            this.PasteUnitPrice = $"{boardLayer?.PasteUnitPrice}";
            this.MokumeDirectionType = boardLayer?.MokumeDirectionCode ?? MokumeDirectionType.Nashi;
        }
    }
}
