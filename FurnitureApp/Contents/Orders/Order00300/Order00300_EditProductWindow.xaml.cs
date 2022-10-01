﻿using FurnitureApp.Contents.Orders.Order00400;
using FurnitureApp.Contents.Orders.Order00500;
using FurnitureApp.Contents.Orders.Order00600;
using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
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
using System.Windows.Shapes;

namespace FurnitureApp.Contents.Orders.Order00300
{
    /// <summary>
    /// Order00300_EditProductWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00300_EditProductWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        public ObservableCollection<BoardView> BoardViews { get; } = new ObservableCollection<BoardView>();
        public ObservableCollection<BoardSizeViewModel> BoardSizeViewModels { get; } = new ObservableCollection<BoardSizeViewModel>();
        public ObservableCollection<CutSizeViewModel> CutSizeViewModels { get; } = new ObservableCollection<CutSizeViewModel>();
        public ObservableCollection<CostViewModel> CostViewModels { get; } = new ObservableCollection<CostViewModel>();
        public ObservableCollection<ProductFileViewModel> ProductFileViewModels { get; } = new ObservableCollection<ProductFileViewModel>();

        public List<DisplayInfo<int?>> ProductCategoryInfos { get; } = new List<DisplayInfo<int?>>();

        public Product Product { get; private set; }
        public bool IsChanged = false;

        public Order00300_EditProductWindow(Product product)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Product = product.Clone();
            this.ProductCategoryInfos.AddRange(this.cd.ProductCategoryInfos.Select(x => new DisplayInfo<int?>(x.Id, x.Name)));
            this.InitializeControls();
            this.SetProductToControls();
        }
        private void InitializeControls()
        {
            #region 部材初期化
            this.BoardViews.Add(new BoardView(BoardType.Tenita));
            this.BoardViews.Add(new BoardView(BoardType.Tenshita));
            this.BoardViews.Add(new BoardView(BoardType.Gawaita));
            this.BoardViews.Add(new BoardView(BoardType.Shikiriita));
            this.BoardViews.Add(new BoardView(BoardType.Tanaita));
            this.BoardViews.Add(new BoardView(BoardType.Tobira));
            this.BoardViews.Add(new BoardView(BoardType.Seita));
            this.BoardViews.Add(new BoardView(BoardType.Jiita));
            this.BoardViews.Add(new BoardView(BoardType.DaiwaFront));
            this.BoardViews.Add(new BoardView(BoardType.DaiwaBack));
            this.BoardViews.Add(new BoardView(BoardType.DaiwaLeft));
            this.BoardViews.Add(new BoardView(BoardType.DaiwaRight));

            #endregion
        }
        private void SetProductToControls()
        {
            this.ProductCategoryInfoComboBox.SelectedValue = this.Product.ProductCategoryInfoId;
            this.NameTextBox.Text = this.Product.Name;
            this.QuantityTextBox.Text = $"{this.Product.Quantity}";
            this.BodyWidthTextBox.Text = $"{this.Product.BodyWidth}";
            this.BodyDepthTextBox.Text = $"{this.Product.BodyDepth}";
            this.BodyHeightTextBox.Text = $"{this.Product.BodyHeight}";
            this.FillerLTextBox.Text = $"{this.Product.FillerL}";
            this.FillerRTextBox.Text = $"{this.Product.FillerR}";
            this.LvlWidthTextBox.Text = $"{this.Product.LvlWidth}"; // LVL
            this.AnkoPitchTextBox.Text = $"{this.Product.AnkoPitch}"; // アンコピッチ
            this.DaiwaHeightTextBox.Text = $"{this.Product.DaiwaHeight}"; // 台輪（H）
            this.TobiraTenitaSukimaTextBox.Text = $"{this.Product.TobiraTenitaSukima}"; // 扉：天板隙間
            this.TobiraTenitaHikaeTextBox.Text = $"{this.Product.TobiraTenitaHikae}"; // 扉：天板控え
            this.TobiraGawaitaMokujiTextBox.Text = $"{this.Product.TobiraGawaitaMokuji}"; // 扉：側板目地
            this.TobiraKanMokujiTextBox.Text = $"{this.Product.TobiraKanMokuji}"; // 扉間目地
            this.TobiraShikiriitaMokujiTextBox.Text = $"{this.Product.TobiraShikiriitaMokuji}"; // 扉：仕切板目地
            this.ShikiriitaGawaitaHikaeTextBox.Text = $"{this.Product.ShikiriitaGawaitaHikae}"; // 仕切板：側板控え
            this.TanaitaGawaitaHikaeTextBox.Text = $"{this.Product.TanaitaGawaitaHikae}"; // 棚板：側板控え
            this.KoguchiTapeUnitPriceTextBox.Text = $"{this.Product.KoguchiPasteUnitPrice}"; // 木口テープ単価
            this.FinishCutUnitPriceTextBox.Text = $"{this.Product.FinishCutUnitPrice}"; // 仕上げカット単価
            this.FinishMarginTextBox.Text = $"{this.Product.FinishMargin}"; // 製作幅

            #region 部材セット
            foreach (var board in this.Product.Boards)
            {
                var boardView = this.BoardViews.First(x => x.BoardType == board.BoardCode);
                boardView.SetBoard(board);
            }
            #endregion

            #region コストセット
            this.CostViewModels.AddRange(this.Product.Costs.Select(x => new CostViewModel(x)));
            #endregion

            #region ファイルセット
            this.ProductFileViewModels.AddRange(this.Product.ProductFiles.Select(x => new ProductFileViewModel(x)));
            #endregion
        }
        private void SelectProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00600_SelectProductWindow(this.ProductCategoryInfoComboBox.SelectedValue as int?);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (w.IsSelected)
                {
                    var product = w.Product;
                    product.ProductFiles.Clear();
                    this.Product = product;
                    this.SetProductToControls();
                }
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        #region コスト
        private void EditCostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ShowEditCostWindow();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void CostDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.ShowEditCostWindow();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ShowEditCostWindow()
        {
            var w = new Order00400_EditCostWindow(this.CostViewModels.Select(x => x.Model));
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            w.ShowDialog();

            if (w.IsChanged)
            {
                this.CostViewModels.Clear();
                this.CostViewModels.AddRange(w.Costs.Select(x => new CostViewModel(x)));
            }
        }
        #endregion

        #region ファイル
        private void AddProductFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00500_EditProductFileWindow(new ProductFileEx());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (w.IsChanged)
                {
                    this.ProductFileViewModels.Add(new ProductFileViewModel(w.ProductFile));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ProductFileDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = this.ProductFileDataGrid.SelectedItem as ProductFileViewModel;

                if (vm == null) { return; }

                var w = new Order00500_EditProductFileWindow(vm.Model);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (w.IsChanged)
                {
                    var index = this.ProductFileViewModels.IndexOf(vm);
                    this.ProductFileViewModels.Remove(vm);
                    if (w.ProductFile.Id == null && w.ProductFile.IsDeleted) { return; }
                    this.ProductFileViewModels.Insert(index, new ProductFileViewModel(w.ProductFile));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        #endregion
        private void RefreshCalButton_Click(object sender, RoutedEventArgs e)
        {
            this.SetProductWithoutCosts();

            var boardSizes = new BoardSizeCalculator(this.Product).GetBoardSizes();

            this.BoardSizeViewModels.Clear();

            foreach (var boardType in this.cd.BoardTypes.Select(x => x.Code))
            {
                var bs = boardSizes.FirstOrDefault(x => x.BoardType == boardType);

                if (bs == null)
                {
                    bs = new BoardSize { BoardType = boardType };
                }

                this.BoardSizeViewModels.Add(new BoardSizeViewModel(bs, this.cd.FinishMargin));
            }

            var cutSizes = new CutSizeCalculator().GetCutSizes(this.Product);

            this.CutSizeViewModels.Clear();
            this.CutSizeViewModels.AddRange(cutSizes.Select(x => new CutSizeViewModel(x)));

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Update();
            }
            catch (Exception ex)
            {
                this.cd.DialogService.ShowMessage(ex.Message);
                this.logger.Error(ex);
            }
        }

        private void Update()
        {
            this.SetProdut();

            this.IsChanged = true;
            this.Close();
        }
        private void SetProductWithoutCosts()
        {
            this.Product.ProductCategoryInfoId = (int?)this.ProductCategoryInfoComboBox.SelectedValue;
            this.Product.Name = this.NameTextBox.Text;
            this.Product.Quantity = Utility.NumberFormatter.GetNullInt(this.QuantityTextBox.Text) ?? 0;
            this.Product.BodyWidth = Utility.NumberFormatter.GetNullDouble(this.BodyWidthTextBox.Text) ?? 0;
            this.Product.BodyDepth = Utility.NumberFormatter.GetNullDouble(this.BodyDepthTextBox.Text) ?? 0;
            this.Product.BodyHeight = Utility.NumberFormatter.GetNullDouble(this.BodyHeightTextBox.Text) ?? 0;
            this.Product.FillerL = Utility.NumberFormatter.GetNullDouble(this.FillerLTextBox.Text) ?? 0;
            this.Product.FillerR = Utility.NumberFormatter.GetNullDouble(this.FillerRTextBox.Text) ?? 0;
            this.Product.LvlWidth = Utility.NumberFormatter.GetNullDouble(this.LvlWidthTextBox.Text) ?? 0;
            this.Product.AnkoPitch = Utility.NumberFormatter.GetNullDouble(this.AnkoPitchTextBox.Text) ?? 0;
            this.Product.DaiwaHeight = Utility.NumberFormatter.GetNullDouble(this.DaiwaHeightTextBox.Text) ?? 0;
            this.Product.TobiraTenitaSukima = Utility.NumberFormatter.GetNullDouble(this.TobiraTenitaSukimaTextBox.Text) ?? 0;
            this.Product.TobiraTenitaHikae = Utility.NumberFormatter.GetNullDouble(this.TobiraTenitaHikaeTextBox.Text) ?? 0;
            this.Product.TobiraGawaitaMokuji = Utility.NumberFormatter.GetNullDouble(this.TobiraGawaitaMokujiTextBox.Text) ?? 0;
            this.Product.TobiraKanMokuji = Utility.NumberFormatter.GetNullDouble(this.TobiraKanMokujiTextBox.Text) ?? 0;
            this.Product.TobiraShikiriitaMokuji = Utility.NumberFormatter.GetNullDouble(this.TobiraShikiriitaMokujiTextBox.Text) ?? 0;
            this.Product.ShikiriitaGawaitaHikae = Utility.NumberFormatter.GetNullDouble(this.ShikiriitaGawaitaHikaeTextBox.Text) ?? 0;
            this.Product.TanaitaGawaitaHikae = Utility.NumberFormatter.GetNullDouble(this.TanaitaGawaitaHikaeTextBox.Text) ?? 0;

            this.Product.Boards.Clear();

            foreach (var b in this.BoardViews)
            {
                var board = b.GetBoardOrDefault();

                if (board == null) { continue; }

                this.Product.Boards.Add(board);
            }
        }
        private void SetProdut()
        {
            this.SetProductWithoutCosts();

            // 板コスト
            var boardSizes = new BoardSizeCalculator(this.Product);
            var cutSizes = new CutSizeCalculator().GetCutSizes(this.Product);

            var bcs = new RectPacker().GetBoardCosts(this.Product.Name, cutSizes, this.cd.MaterialSizeInfos);
            this.Product.BoardCosts.Clear();
            this.Product.BoardCosts.AddRange(bcs);

            // 木口貼りコスト
            var koguchis = new KoguchiPasteCostCalculator().GetKoguchiPasteCosts(this.Product);
            this.Product.KoguchiPasteCosts.Clear();
            this.Product.KoguchiPasteCosts.AddRange(koguchis);

            // 仕上げカットコスト
            var finishes = new FinishCutCostCalculator().GetFinishCutCosts(this.Product);
            this.Product.FinishCutCosts.Clear();
            this.Product.FinishCutCosts.AddRange(finishes);

            // 化粧板貼りコスト
            var makeups = new MakeupBoardPasteCostCalculator().GetMakeupBoardPasteCosts(this.Product);
            this.Product.MakeupBoardPasteCosts.Clear();
            this.Product.MakeupBoardPasteCosts.AddRange(makeups);

            // コスト
            this.Product.Costs.Clear();
            this.Product.Costs.AddRange(this.CostViewModels.Select(x => x.Model));

            this.Product.ProductFiles.Clear();
            this.Product.ProductFiles.AddRange(this.ProductFileViewModels.Select(x => x.Model));
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}