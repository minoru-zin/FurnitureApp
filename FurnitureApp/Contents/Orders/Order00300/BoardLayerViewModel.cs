using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FurnitureApp.Contents.Orders.Order00300
{
    public class BoardLayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int? MaterialInfoCode { get; set; }

        private double? thickness;

        public double? Thickness
        {
            get { return thickness; }
            set
            {
                thickness = value;
                NotifyPropertyChanged(nameof(Thickness));
            }
        }
        private string pasteUnitPrice;
        
        public string PasteUnitPrice
        {
            get { return pasteUnitPrice; }
            set
            {
                pasteUnitPrice = value;
                NotifyPropertyChanged(nameof(PasteUnitPrice));
            }
        }

        public MokumeDirectionType MokumeDirectionType { get; set; }
        public BoardLayer Model
        {
            get
            {
                return new BoardLayer
                {
                    MaterialInfoCode = this.MaterialInfoCode,
                    PasteUnitPrice = Utility.NumberFormatter.GetNullInt(this.PasteUnitPrice),
                    MokumeDirectionCode = MokumeDirectionType,
                };
            }
        }

        private CommonData cd = CommonData.GetInstance();

        public BoardLayerViewModel(BoardLayer boardLayer)
        {
            if (boardLayer?.MaterialInfoCode == null) { return; }
            this.MaterialInfoCode = boardLayer.MaterialInfoCode;
            this.Thickness = this.cd.MaterialInfoDict.GetValueOrDefault(boardLayer.MaterialInfoCode)?.Thickness;
            this.PasteUnitPrice = $"{boardLayer.PasteUnitPrice}";
            this.MokumeDirectionType = boardLayer.MokumeDirectionCode;
        }
        public void NotifyPropertyChanged(string PropertyName)
        {
            var e = new PropertyChangedEventArgs(PropertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
