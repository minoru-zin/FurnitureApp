using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Utility;
using FurnitureApp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FurnitureApp.Contents.Orders.Order00300
{
    /// <summary>
    /// ProductPartView.xaml の相互作用ロジック
    /// </summary>
    public partial class BoardView : UserControl
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        public BoardType BoardType { get; }
        public List<DisplayInfo<MaterialInfo>> MaterialInfos { get; } = new List<DisplayInfo<MaterialInfo>>();
        public List<DisplayInfo<KoguchiMakeupArea>> KoguchiKeshouAreas { get; }
        public List<DisplayInfo<MokumeDirectionType>> MokumeDirectionTypes { get; }
        public ObservableCollection<BoardLayerViewModel> ViewModels { get; } = new ObservableCollection<BoardLayerViewModel>();

        public BoardView(BoardType boardType)
        {
            InitializeComponent();
            this.DataContext = this;

            this.BoardType = boardType;
            this.MaterialInfos.AddRange(this.cd.MaterialInfos.Select(x => new DisplayInfo<MaterialInfo>(x, x.Name)));
            this.BoardTypeTextBlock.Text = this.cd.BoardTypes.First(x => x.Code == boardType).DisplayName;
            this.KoguchiKeshouAreas = this.cd.KoguchiKeshouAreas;
            this.MokumeDirectionTypes = this.cd.MokumeDirectionTypes;
        }
        public BoardView()
        {
        }
        private void QuantityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (Utility.NumberFormatter.GetNullInt(textBox.Text) > 0)
            {
                this.Background = new SolidColorBrush(Colors.AliceBlue);
            }
            else
            {
                this.Background = new SolidColorBrush(Colors.LightGray);
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModels.Add(new BoardLayerViewModel(null, null));
            this.CalSumThickness();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.BoardLayerDataGrid.SelectedItem as BoardLayerViewModel;

            if (vm == null) { return; }

            var index = this.ViewModels.IndexOf(vm);

            this.ViewModels.Remove(vm);

            this.CalSumThickness();

            if (!this.ViewModels.Any()) { return; }

            if (this.ViewModels.Count > index)
            {
                this.BoardLayerDataGrid.SelectedItem = this.ViewModels[index];
            }
            else
            {
                this.BoardLayerDataGrid.SelectedItem = this.ViewModels[this.ViewModels.Count - 1];
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.BoardLayerDataGrid.SelectedItem as BoardLayerViewModel;

            if (vm == null) { return; }

            var index = this.ViewModels.IndexOf(vm);

            if (index == 0) { return; }

            var clone = this.ViewModels.ToList()[index];

            this.ViewModels.Remove(vm);

            this.ViewModels.Insert(index - 1, clone);

            this.BoardLayerDataGrid.SelectedItem = clone;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.BoardLayerDataGrid.SelectedItem as BoardLayerViewModel;

            if (vm == null) { return; }

            var index = this.ViewModels.IndexOf(vm);

            if (index == this.ViewModels.Count - 1) { return; }

            var clone = this.ViewModels.ToList()[index];

            this.ViewModels.Remove(vm);

            this.ViewModels.Insert(index + 1, clone);

            this.BoardLayerDataGrid.SelectedItem = clone;

        }

        private void MaterialInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.BoardLayerDataGrid.SelectedItem as BoardLayerViewModel;

            if (vm == null) { return; }

            vm.PasteUnitPrice = $"{vm.MaterialInfo.PasteUnitPrice}";

            this.CalSumThickness();

            this.ViewModels.Update();
        }

        private void CalSumThickness()
        {
            this.SumThicknessTextBlock.Text = $"{this.ViewModels.Where(x => x.MaterialInfo != null).Sum(x => x.MaterialInfo.Thickness)}";
        }
        public void SetBoard(Board board)
        {
            this.QuantityTextBox.Text = $"{board.Quantity}";
            this.KoguchiKeshouAreaComboBox.SelectedValue = board.KoguchiKeshouAreaCode;
            this.ViewModels.Clear();
            this.ViewModels.AddRange(board.BoardLayers.Select(x => new BoardLayerViewModel(x, this.cd.MaterialInfos.FirstOrDefault(m => m.Id == x.MaterialInfoId))));
            this.CalSumThickness();
        }
        public Board GetBoardOrDefault()
        {
            var quantity = Utility.NumberFormatter.GetNullInt(this.QuantityTextBox.Text) ?? 0;

            if (quantity == 0) { return null; }

            var vms = this.ViewModels.Where(x => x.MaterialInfo != null);

            if (!vms.Any()) { return null; }

            var boardLayers = new List<BoardLayer>();

            foreach (var (vm, index) in vms.WithIndex())
            {
                var boardLayer = vm.Model;
                boardLayer.Sequence = index;
                boardLayers.Add(boardLayer);
            }

            var board = new Board
            {
                BoardCode = this.BoardType,
                Quantity = quantity,
                KoguchiKeshouAreaCode = (KoguchiMakeupArea)this.KoguchiKeshouAreaComboBox.SelectedValue,
                BoardLayers = boardLayers,
            };

            return board;
        }
    }
}
