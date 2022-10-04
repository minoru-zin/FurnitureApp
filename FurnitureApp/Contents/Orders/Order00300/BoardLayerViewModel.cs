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


        public MaterialInfo MaterialInfo { get; set; }
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
                    MaterialInfoCode = this.MaterialInfo.Id,
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
        public void NotifyPropertyChanged(string PropertyName)
        {
            var e = new PropertyChangedEventArgs(PropertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
