using FurnitureApp.Contents.Orders.Order00400;
using FurnitureApp.Contents.Orders.Order00500;
using FurnitureApp.Contents.Orders.Order00600;
using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FurnitureApp.Contents.Orders.Order00300
{
    /// <summary>
    /// Order00300_EditProductWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00300_EditProductWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private ControlFormatter cf = new ControlFormatter();
        public ObservableCollection<BoardView> BoardViews { get; } = new ObservableCollection<BoardView>();
        public ObservableCollection<BoardSizeViewModel> BoardSizeViewModels { get; } = new ObservableCollection<BoardSizeViewModel>();
        public ObservableCollection<BoardSizeViewModel> BoardSizeViewModels2 { get; } = new ObservableCollection<BoardSizeViewModel>();
        public ObservableCollection<CostViewModel> CostViewModels { get; } = new ObservableCollection<CostViewModel>();
        public ObservableCollection<ProductFileViewModel> ProductFileViewModels { get; } = new ObservableCollection<ProductFileViewModel>();

        public List<DisplayInfo<int?>> ProductCategoryInfos { get; } = new List<DisplayInfo<int?>>();

        private Product oldProduct;
        public Product Product { get; private set; }
        public bool IsChanged = false;
        public bool IsDeleted = false;
        private bool canClose = false;

        public Order00300_EditProductWindow(Product product)
        {
            InitializeComponent();
            this.DataContext = this;
            this.oldProduct = product.Clone();
            this.Product = product.Clone();
            this.ProductCategoryInfos.AddRange(this.cd.ProductCategoryInfos.Select(x => new DisplayInfo<int?>(x.Code, x.Name)));
            this.InitializeControls();
            this.SetProductToControls();
        }
        private void Root_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    (FocusManager.GetFocusedElement(System.Windows.Window.GetWindow(this)) as System.Windows.FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    break;
                case Key.Down:
                    (FocusManager.GetFocusedElement(System.Windows.Window.GetWindow(this)) as System.Windows.FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    e.Handled = true;
                    break;
                case Key.Up:
                    (FocusManager.GetFocusedElement(System.Windows.Window.GetWindow(this)) as System.Windows.FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
                    e.Handled = true;
                    break;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.NameTextBox.Focus();
        }

        private void QuantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void UpQuantityButton_Click(object sender, RoutedEventArgs e)
        {
            var textBox = this.QuantityTextBox;
            var number = Utility.NumberFormatter.GetNullInt(textBox.Text) ?? 0;
            if (number < 0) { number = 0; }
            textBox.Text = $"{number + 1}";
        }

        private void DownQuantityButton_Click(object sender, RoutedEventArgs e)
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

        private void DoubleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDoubleNumberTextBox(sender as TextBox);
        }
        private void DoubleTextBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDoubleNumberTextBox(sender as TextBox, 2);
        }
        private void UpdatedDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDate(sender as TextBox);
        }
        private void InitializeControls()
        {
            #region 部材初期化
            this.BoardViews.Add(new BoardView(BoardType.Tenita));
            this.BoardViews.Add(new BoardView(BoardType.Tenshita));
            this.BoardViews.Add(new BoardView(BoardType.GawaitaLeft));
            this.BoardViews.Add(new BoardView(BoardType.GawaitaRight));
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
            this.ProductCategoryInfoComboBox.SelectedValue = this.Product.ProductCategoryInfoCode;
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
            this.ShikiriitaGawaitaHikaeTextBox.Text = $"{this.Product.ShikiriitaGawaitaHikae}"; // 仕切板：側板控え
            this.TanaitaGawaitaHikaeTextBox.Text = $"{this.Product.TanaitaGawaitaHikae}"; // 棚板：側板控え
            this.KoguchiPasteUnitPriceTextBox.Text = $"{this.Product.KoguchiPasteUnitPrice}"; // 木口テープ単価
            this.FinishCutUnitPriceTextBox.Text = $"{this.Product.FinishCutUnitPrice}"; // 仕上げカット単価
            this.FinishMarginTextBox.Text = $"{this.Product.FinishMargin}"; // 製作幅
            this.UpdatedDateTextBox.Text = $"{DateTime.Now.Date:d}";

            #region 部材セット
            foreach (var board in this.Product.Boards)
            {
                var boardView = this.BoardViews.First(x => x.BoardType == board.BoardCode);
                boardView.SetBoard(board);
            }
            #endregion

            #region コストセット
            this.CostViewModels.AddRange(this.Product.Costs.Select(x => new CostViewModel(x)));
            this.SetTotalCostAmountTextBlock();

            #endregion

            #region ファイルセット
            this.ProductFileViewModels.AddRange(this.Product.ProductFiles.Select(x => new ProductFileViewModel(x)));
            #endregion

            this.RefreshBordViewModels();
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
        private void SetTotalCostAmountTextBlock()
        {
            this.TotalCostAmountTextBlock.Text = $"合計 {this.CostViewModels.Sum(x => x.TotalAmount):#,0}円";
        }
        private void AddCostButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00400_EditCostWindow(new Cost());

                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.DeleteButton.IsEnabled = false;
                w.ShowDialog();

                if (w.IsChanged)
                {
                    this.CostViewModels.Add(new CostViewModel(w.Model));
                }
                this.SetTotalCostAmountTextBlock();
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
                var vm = this.CostDataGrid.SelectedItem as CostViewModel;

                if (vm == null) { return; }

                var w = new Order00400_EditCostWindow(vm.Model);

                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (w.IsChanged)
                {
                    var index = this.CostViewModels.IndexOf(vm);

                    this.CostViewModels.Remove(vm);

                    if (!w.IsDeleted)
                    {
                        this.CostViewModels.Insert(index, new CostViewModel(w.Model));
                    }
                }
                this.SetTotalCostAmountTextBlock();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void UpCostButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.CostDataGrid.SelectedItem as CostViewModel;

            if (vm == null) { return; }

            var index = this.CostViewModels.IndexOf(vm);

            if (index == 0) { return; }

            var clone = this.CostViewModels.ToList()[index];

            this.CostViewModels.Remove(vm);

            this.CostViewModels.Insert(index - 1, clone);

            this.CostDataGrid.SelectedItem = clone;
        }

        private void DownCostButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.CostDataGrid.SelectedItem as CostViewModel;

            if (vm == null) { return; }

            var index = this.CostViewModels.IndexOf(vm);

            if (index == this.CostViewModels.Count - 1) { return; }

            var clone = this.CostViewModels.ToList()[index];

            this.CostViewModels.Remove(vm);

            this.CostViewModels.Insert(index + 1, clone);

            this.CostDataGrid.SelectedItem = clone;
        }

        #endregion

        #region ファイル

        private void AddProductFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00500_EditProductFileWindow(new ProductFileEx());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.DeleteButton.IsEnabled = false;
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
        private void DisplayFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = this.ProductFileDataGrid.SelectedItem as ProductFileViewModel;

                if (vm == null) { return; }

                var orderId = this.Product.OrderId;
                var sourceFilePath = "";

                if (string.IsNullOrEmpty(vm.Model.SourceFilePath))
                {
                    sourceFilePath = this.cd.OrderRepository.GetProductFilePath(orderId, vm.Model.FileName);
                }
                else
                {
                    sourceFilePath = vm.Model.SourceFilePath;
                }

                var destFilePath = Path.Combine(this.cd.TempFileDirName, $"{vm.Model.DisplayName}{Path.GetExtension(vm.Model.FileName)}");

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
        #endregion
        private void RefreshCalButton_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshBordViewModels();
        }
        private void RefreshBordViewModels()
        {
            this.SetProductWithoutCosts();

            var tempBoardSizes = new BoardSizeCalculator(this.Product).GetBoardSizes();

            var finishMargin = Utility.NumberFormatter.GetNullDouble(this.FinishMarginTextBox.Text) ?? 0;


            var boardSizes = new List<BoardSize>();

            foreach (var boardType in this.cd.BoardTypes.Select(x => x.Code))
            {
                var bs = tempBoardSizes.FirstOrDefault(x => x.BoardType == boardType);

                if (bs == null)
                {
                    bs = new BoardSize { BoardType = boardType };
                }

                boardSizes.Add(bs);
            }

            this.BoardSizeViewModels.Clear();
            this.BoardSizeViewModels.AddRange(boardSizes.Select(x => new BoardSizeViewModel(x)));

            boardSizes = boardSizes.Select(x => x.Clone()).ToList();

            foreach (var bs in boardSizes)
            {
                bs.Width += finishMargin * 2;
                bs.Length += finishMargin * 2;
            }

            this.BoardSizeViewModels2.Clear();
            this.BoardSizeViewModels2.AddRange(boardSizes.Select(x => new BoardSizeViewModel(x)));
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
            if ((int?)this.ProductCategoryInfoComboBox.SelectedValue == null) { throw new Exception("カテゴリが不適"); }
            if (string.IsNullOrEmpty(this.NameTextBox.Text.Trim())) { throw new Exception("製品名が不適"); }
            var banTexts = new List<string> { @"\", "/", ":", "*", "?", "\"", "<", ">", "|" };
            if (banTexts.Any(x => this.NameTextBox.Text.Contains(x))) { throw new Exception($"製品名に禁止文字列「{string.Join(" ", banTexts)}」が含まれている"); }
            if ((Utility.NumberFormatter.GetNullInt( this.QuantityTextBox.Text) ?? 0) < 1) { throw new Exception("数量が不適"); }
            if (string.IsNullOrEmpty(this.UpdatedDateTextBox.Text)) { throw new Exception("更新日が不適"); }

            this.SetProdut();

            this.IsChanged = true;
            this.canClose = true;
            this.Close();
        }
        private void SetProductWithoutCosts()
        {
            this.Product.ProductCategoryInfoCode = (int?)this.ProductCategoryInfoComboBox.SelectedValue;
            this.Product.Name = $"{this.NameTextBox.Text}".Trim();
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
            this.Product.ShikiriitaGawaitaHikae = Utility.NumberFormatter.GetNullDouble(this.ShikiriitaGawaitaHikaeTextBox.Text) ?? 0;
            this.Product.TanaitaGawaitaHikae = Utility.NumberFormatter.GetNullDouble(this.TanaitaGawaitaHikaeTextBox.Text) ?? 0;
            this.Product.KoguchiPasteUnitPrice = Utility.NumberFormatter.GetNullDouble(this.KoguchiPasteUnitPriceTextBox.Text) ?? 0;
            this.Product.FinishCutUnitPrice = Utility.NumberFormatter.GetNullDouble(this.FinishCutUnitPriceTextBox.Text) ?? 0;
            this.Product.FinishMargin = Utility.NumberFormatter.GetNullDouble(this.FinishMarginTextBox.Text) ?? 0;
            this.Product.UpdatedDate = Utility.DateTimeFormatter.GetDateTime(this.UpdatedDateTextBox.Text);

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

            cutSizes.ForEach(x => x.Quantity = x.Quantity * this.Product.Quantity);

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

            // 塗装コスト
            var paints = new PaintCostCalculator().GetPaintCosts(this.Product);
            this.Product.PaintCosts.Clear();
            this.Product.PaintCosts.AddRange(paints);

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
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Delete();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void Delete()
        {
            if (!this.cd.DialogService.ShowComfirmationMessageDialog("本当に削除しますか？"))
            {
                return;
            }

            this.IsChanged = true;
            this.IsDeleted = true;
            this.canClose = true;
            this.Close();
        }

        private void Root_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.canClose)
            {
                this.SetProductWithoutCosts();
                this.canClose = this.oldProduct.IsSame(this.Product);
            }

            if (this.canClose) { return; }

            if (!this.cd.DialogService.ShowComfirmationMessageDialog("編集中ですが、閉じますか？"))
            {
                e.Cancel = true;
                return;
            }
        }

       
    }
}
