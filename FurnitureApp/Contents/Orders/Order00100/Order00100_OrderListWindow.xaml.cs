﻿using FurnitureApp.Contents.Masters.Master00100;
using FurnitureApp.Contents.Masters.Master00200;
using FurnitureApp.Contents.Masters.Master00300;
using FurnitureApp.Contents.Masters.Master00400;
using FurnitureApp.Contents.Orders.Order00200;
using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.ProductCategoryInfos;
using FurnitureApp.Repository.Utilities;
using FurnitureApp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FurnitureApp.Contents.Orders.Order00100
{
    /// <summary>
    /// Order00100_OrderListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00100_OrderListWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private readonly string tempFileDirName = "TempFiles";
        public ObservableCollection<OrderViewModel> OrderViewModels { get; } = new ObservableCollection<OrderViewModel>();
        public ObservableCollection<ProductViewModel> ProductViewModels { get; } = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<DisplayBoardViewModel> BoardViewModels { get; } = new ObservableCollection<DisplayBoardViewModel>();
        public ObservableCollection<DisplayBoardLayerViewModel> BoardLayerViewModels { get; } = new ObservableCollection<DisplayBoardLayerViewModel>();
        public ObservableCollection<DisplayCostViewModel> CostViewModels { get; } = new ObservableCollection<DisplayCostViewModel>();
        public ObservableCollection<DisplayProductFileViewModel> ProductFileViewModels { get; } = new ObservableCollection<DisplayProductFileViewModel>();

        public ObservableCollection<DisplayBoardViewModel> BoardViewModels2 { get; } = new ObservableCollection<DisplayBoardViewModel>();
        public ObservableCollection<DisplayBoardCostViewModel> BoardCostViewModels { get; } = new ObservableCollection<DisplayBoardCostViewModel>();
        public ObservableCollection<DisplayKoguchiPasteCostViewModel> KoguchiPasteCostViewModels { get; } = new ObservableCollection<DisplayKoguchiPasteCostViewModel>();
        public ObservableCollection<DisplayFinishCutCostViewModel> FinishCutCostViewModels { get; } = new ObservableCollection<DisplayFinishCutCostViewModel>();
        public ObservableCollection<DisplayMakeupBoardPasteCostViewModel> MakeupBoardPasteCostViewModels { get; } = new ObservableCollection<DisplayMakeupBoardPasteCostViewModel>();


        private Dictionary<int?, ProductCategoryInfo> productCateogryInfoDict;
        public Order00100_OrderListWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.CreatedDateTextBox.Text = $"{DateTime.Now.Date.AddMonths(-6):d}";
            this.productCateogryInfoDict = this.cd.ProductCategoryInfos.ToDictionary(x => x.Id);

            this.DisplayOrders();
        }

        private void DisplayOrders()
        {
            var createdDate = Utility.DateTimeFormatter.GetDateTime(this.CreatedDateTextBox.Text);

            if (createdDate == null)
            {
                this.cd.DialogService.ShowMessage("作成日を入力してください");
                return;
            }

            var orders = this.cd.OrderRepository.SelectFromCreatedDate((DateTime)createdDate);

            this.OrderViewModels.Clear();

            foreach (var order in orders)
            {
                this.OrderViewModels.Add(new OrderViewModel(order));
            }

            this.ProductViewModels.Clear();
            this.BoardViewModels.Clear();
            this.BoardLayerViewModels.Clear();
            this.CostViewModels.Clear();
            this.ProductFileViewModels.Clear();
            this.BoardViewModels2.Clear();
            this.BoardCostViewModels.Clear();
            this.KoguchiPasteCostViewModels.Clear();
            this.FinishCutCostViewModels.Clear();
            this.MakeupBoardPasteCostViewModels.Clear();

        }
        private void RefreshOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            this.DisplayOrders();
        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00200_EditOrderWindow(new Order());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (!w.IsChanged) { return; }

                this.DisplayOrders();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void OrderDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = this.OrderDataGrid.SelectedItem as OrderViewModel;

                if (vm == null) { return; }

                var w = new Order00200_EditOrderWindow(vm.Model);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (!w.IsChanged) { return; }

                this.DisplayOrders();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void OrderDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var vm = this.OrderDataGrid.SelectedItem as OrderViewModel;

                if (vm == null) { return; }

                this.ProductViewModels.Clear();
                this.ProductViewModels.AddRange(vm.Model.Products.Select(x => new ProductViewModel(x, this.productCateogryInfoDict.GetValueOrDefault(x.ProductCategoryInfoId)?.Name)));
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ProductDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var vm = this.ProductDataGrid.SelectedItem as ProductViewModel;

                if (vm == null) { return; }

                this.SetProductToControls(vm.Model);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void SetProductToControls(Product p)
        {
            this.ProductCategoryNameTextBlock.Text = this.productCateogryInfoDict.GetValueOrDefault(p.ProductCategoryInfoId)?.Name;
            this.NameTextBlock.Text = p.Name;
            this.QuantityTextBlock.Text = $"{p.Quantity}";
            this.BodyWidthTextBlock.Text = $"{p.BodyWidth}";
            this.BodyDepthTextBlock.Text = $"{p.BodyDepth}";
            this.BodyHeightTextBlock.Text = $"{p.BodyHeight}";
            this.FillerLTextBlock.Text = $"{p.FillerL}";
            this.FillerRTextBlock.Text = $"{p.FillerR}";
            this.LvlWidthTextBlock.Text = $"{p.LvlWidth}";
            this.AnkoPitchTextBlock.Text = $"{p.AnkoPitch}";
            this.DaiwaHeightTextBlock.Text = $"{p.DaiwaHeight}";
            this.TobiraTenitaSukimaTextBlcok.Text = $"{p.TobiraTenitaSukima}";
            this.TobiraTenitaHikaeTextBlock.Text = $"{p.TobiraTenitaHikae}";
            this.TobiraGawaitaMokujiTextBlock.Text = $"{p.TobiraGawaitaMokuji}";
            this.TobiraKanMokujiTextBlock.Text = $"{p.TobiraKanMokuji}";
            this.TobiraShikiriitaMokujiTextBlcok.Text = $"{p.TobiraShikiriitaMokuji}";
            this.ShikiriitaGawaitaHikaeTextBlock.Text = $"{p.ShikiriitaGawaitaHikae}";
            this.TanaitaGawaitaHikaeTextBlcok.Text = $"{p.TanaitaGawaitaHikae}";
            this.KoguchiPasteUnitPriceTextBlcok.Text = $"{p.KoguchiPasteUnitPrice}";
            this.FinishCutUnitPriceTextBlock.Text = $"{p.FinishCutUnitPrice}";
            this.FinishMarginTextBlock.Text = $"{p.FinishMargin}";

            var boardSizes = new BoardSizeCalculator(p).GetBoardSizes();

            this.BoardViewModels.Clear();

            foreach (var boardType in this.cd.BoardTypes.Select(x => x.Code))
            {
                var board = p.Boards.FirstOrDefault(x => x.BoardCode == boardType) ?? new Board { BoardCode = boardType };
                var boardSize = boardSizes.FirstOrDefault(x => x.BoardType == boardType);

                this.BoardViewModels.Add(new DisplayBoardViewModel(board, boardSize));
            }

            this.CostViewModels.Clear();
            this.CostViewModels.AddRange(p.Costs.Select(x => new DisplayCostViewModel(x)));

            this.ProductFileViewModels.Clear();
            this.ProductFileViewModels.AddRange(p.ProductFiles.Select(x => new DisplayProductFileViewModel(x)));

            this.BoardViewModels2.Clear();
            foreach (var boardType in this.cd.BoardTypes.Select(x => x.Code))
            {
                var board = p.Boards.FirstOrDefault(x => x.BoardCode == boardType) ?? new Board { BoardCode = boardType };
                var boardSize = boardSizes.FirstOrDefault(x => x.BoardType == boardType);

                if (boardSize != null)
                {
                    boardSize = boardSize.Clone();
                    boardSize.Width += p.FinishMargin * 2;
                    boardSize.Length += p.FinishMargin * 2;
                }
                this.BoardViewModels2.Add(new DisplayBoardViewModel(board, boardSize));
            }

            this.BoardCostViewModels.Clear();
            this.BoardCostViewModels.AddRange(p.BoardCosts.Select(x => new DisplayBoardCostViewModel(x)));

            this.KoguchiPasteCostViewModels.Clear();
            this.KoguchiPasteCostViewModels.AddRange(p.KoguchiPasteCosts.Select(x => new DisplayKoguchiPasteCostViewModel(x)));

            this.FinishCutCostViewModels.Clear();
            this.FinishCutCostViewModels.AddRange(p.FinishCutCosts.Select(x => new DisplayFinishCutCostViewModel(x)));

            this.MakeupBoardPasteCostViewModels.Clear();
            this.MakeupBoardPasteCostViewModels.AddRange(p.MakeupBoardPasteCosts.Select(x => new DisplayMakeupBoardPasteCostViewModel(x)));
        }

        private void BoardDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var vm = this.BoardDataGrid.SelectedItem as DisplayBoardViewModel;

            if (vm == null) { return; }

            this.BoardLayerViewModels.Clear();

            this.BoardLayerViewModels.AddRange(vm.Model.BoardLayers.Select(x => new DisplayBoardLayerViewModel(x)));
        }
        private void ProductFileDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = this.ProductFileDataGrid.SelectedItem as DisplayProductFileViewModel;

                if (vm == null) { return; }

                var orderId = (this.ProductDataGrid.SelectedItem as ProductViewModel).Model.OrderId;
                var sourceFilePath = this.cd.OrderRepository.GetProductFilePath(orderId, vm.Model.FileName);
                var destFilePath = Path.Combine(this.tempFileDirName, vm.Model.FileName);

                Utility.DirectoryCreator.CreateSafely(Path.GetDirectoryName(destFilePath));

                File.Copy(sourceFilePath, destFilePath, true);

                var app = new ProcessStartInfo();
                app.FileName = destFilePath;
                app.UseShellExecute = true;

                Process.Start(app);
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        #region マスタ
        private void ShowMaterialInfosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00100_MaterialInfoListWindow();
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ShowMaterialSizeInfosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00200_MaterialSizeInfoListWindow();
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ShowProductCategoryInfosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00300_ProductCategoryInfoListWindow();
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void ShowCostItemInfosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00400_CostItemInfoListWindow();
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }


        #endregion

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                foreach (var filePath in Directory.GetFiles(this.tempFileDirName)) { File.Delete(filePath); }
            }
            catch (Exception)
            {
            }
        }
    }
}