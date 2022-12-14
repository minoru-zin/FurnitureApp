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
using System.Windows.Media.Animation;
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
        public List<DisplayInfo<int?>> MaterialInfos { get; } = new List<DisplayInfo<int?>>();
        public List<DisplayInfo<KoguchiMakeupArea>> KoguchiKeshouAreas { get; }
        public List<DisplayInfo<int?>> PaintCostItemInfos { get; } = new List<DisplayInfo<int?>> { new DisplayInfo<int?>(null, "無し") };
        public List<DisplayInfo<PaintArea>> PaintAreas { get; }
        public List<DisplayInfo<MokumeDirectionType>> MokumeDirectionTypes { get; }
        public ObservableCollection<BoardLayerViewModel> ViewModels { get; } = new ObservableCollection<BoardLayerViewModel>();

        public BoardView(BoardType boardType)
        {
            InitializeComponent();
            this.DataContext = this;

            this.BoardType = boardType;

            if(boardType == BoardType.Tanaita)
            {
                this.BoardCountUnitTextBlock.Text = "段";
            }
            this.MaterialInfos.AddRange(this.cd.MaterialInfos.Select(x => new DisplayInfo<int?>(x.Code, x.Name)));
            this.BoardTypeTextBlock.Text = this.cd.BoardTypes.First(x => x.Code == boardType).DisplayName;
            this.KoguchiKeshouAreas = this.cd.KoguchiKeshouAreas;
            this.PaintCostItemInfos.AddRange(this.cd.PaintCostItemInfos.Select(x => new DisplayInfo<int?>(x.Code, x.Name)));
            this.PaintAreas = this.cd.PaintAreas;
            this.MokumeDirectionTypes = this.cd.MokumeDirectionTypes;

            this.KoguchiKeshouAreaComboBox.SelectedValue = KoguchiMakeupArea.Nashi;
            this.PaintAreaComboBox.SelectedValue = PaintArea.Nashi;
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
            this.ViewModels.Add(new BoardLayerViewModel(null));
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

            this.ViewModels.Remove(vm);

            this.ViewModels.Insert(index - 1, vm);

            this.BoardLayerDataGrid.SelectedItem = vm;
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

            var info = this.cd.MaterialInfoDict.GetValueOrDefault(vm.MaterialInfoCode);

            vm.Thickness = info?.Thickness;
            vm.PasteUnitPrice = $"{info?.PasteUnitPrice}";

            this.CalSumThickness();
        }

        private void CalSumThickness()
        {
            this.SumThicknessTextBlock.Text = $"{this.ViewModels.Where(x => x.MaterialInfoCode != null).Sum(x => x.Thickness)}";
        }
        public void SetBoard(Board board)
        {
            this.QuantityTextBox.Text = $"{board.Quantity}";
            this.KoguchiKeshouAreaComboBox.SelectedValue = board.KoguchiKeshouAreaCode;
            this.PaintCostItemInfoComboBox.SelectedValue = board.PaintCostItemInfoCode;
            this.PaintAreaComboBox.SelectedValue = board.PaintArea;
            this.ViewModels.Clear();
            this.ViewModels.AddRange(board.BoardLayers.Select(x => new BoardLayerViewModel(x)));
            this.CalSumThickness();
        }
        public Board GetBoardOrDefault()
        {
            var quantity = Utility.NumberFormatter.GetNullInt(this.QuantityTextBox.Text) ?? 0;

            if (quantity == 0) { return null; }

            var vms = this.ViewModels.Where(x => x.MaterialInfoCode != null);

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
                PaintCostItemInfoCode = (int?)this.PaintCostItemInfoComboBox.SelectedValue,
                PaintArea = (PaintArea)this.PaintAreaComboBox.SelectedValue,
                BoardLayers = boardLayers,
            };

            return board;
        }

        private void CountUpButton_Click(object sender, RoutedEventArgs e)
        {
            var textBox = this.QuantityTextBox;
            var number = Utility.NumberFormatter.GetNullInt(textBox.Text) ?? 0;
            if (number < 0) { number = 0; }
            textBox.Text = $"{number + 1}";
        }

        private void CountDownButton_Click(object sender, RoutedEventArgs e)
        {
            var textBox = this.QuantityTextBox;
            var number = Utility.NumberFormatter.GetNullInt(textBox.Text) ?? 0;
            if (number <= 0)
            {
                textBox.Text = "0";
                return;
            }
            textBox.Text = $"{number - 1}";
        }
    }
}
