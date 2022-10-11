using FurnitureApp.Contents.Masters.Master00100;
using FurnitureApp.Contents.Masters.Master00200;
using FurnitureApp.Contents.Masters.Master00300;
using FurnitureApp.Contents.Masters.Master00400;
using FurnitureApp.Contents.Masters.Master00500;
using FurnitureApp.Contents.Orders.Order00200;
using FurnitureApp.Contents.Orders.Order00700;
using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.ProductCategoryInfos;
using FurnitureApp.Repository.Utilities;
using FurnitureApp.Utility;
using FurnitureApp.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
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
        private ControlFormatter cf = new ControlFormatter();
        private List<Order> orders = new List<Order>();
        public ObservableCollection<OrderViewModel> OrderViewModels { get; } = new ObservableCollection<OrderViewModel>();
        public ObservableCollection<string> ClientNameViewModels { get; } = new ObservableCollection<string>();
        private readonly string allClientName = "指定なし";
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
        public ObservableCollection<DisplayPaintCostViewModel> PaintCostViewModels { get; } = new ObservableCollection<DisplayPaintCostViewModel>();

        public Order00100_OrderListWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.ResetSearchControls();

            this.SearchOrders();
        }

        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    (FocusManager.GetFocusedElement(System.Windows.Window.GetWindow(this)) as System.Windows.FrameworkElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SearchNameTextBox.Focus();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as System.Windows.Controls.TextBox;

            if (textBox == null) { return; }

            textBox.SelectAll();
        }

        #region 受注検索
        private void CreatedDateFTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDates(this.CreatedDateFTextBox, this.CreatedDateTTextBox);
        }
        
        private void SearchOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            this.SearchOrders();
        }
        private void SearchOrders()
        {
            var createdDateF = Utility.DateTimeFormatter.GetDateTime(this.CreatedDateFTextBox.Text);
            var createdDateT = Utility.DateTimeFormatter.GetDateTime(this.CreatedDateTTextBox.Text);

            if (createdDateF == null || createdDateT == null)
            {
                this.cd.DialogService.ShowMessage("作成日を入力してください");
                return;
            }

            var orders = this.cd.OrderRepository.SelectTopOnlyFromCreatedDate((DateTime)createdDateF, (DateTime)createdDateT).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.Id).ToList();

            if (!string.IsNullOrEmpty(this.SearchNameTextBox.Text))
            {
                orders = orders.Where(x => $"{x.Name}".Contains(this.SearchNameTextBox.Text)).ToList();
            }

            this.orders = orders;
            this.OrderViewModels.Clear();
            this.OrderViewModels.AddRange(this.orders.Select(x => new OrderViewModel(x)));

            this.ClientNameViewModels.Clear();
            this.ClientNameViewModels.Add(this.allClientName);
            this.ClientNameViewModels.AddRange(this.OrderViewModels.GroupBy(x => x.ClientName).Select(x => x.Key).OrderBy(x => x));

            this.ResetOrderInfo();
        }
        
        private void ResetOrderInfo()
        {
            this.ProductViewModels.Clear();
            this.ResetProductPropertyViewModels();
            this.TotalAmountTextBlock.Text = "";
            this.TotalAmountTextBlock2.Text = "";
            this.SetOrderCount();
        }
        private void SetOrderCount()
        {
            this.OrderCountTextBlock.Text = $"{this.OrderViewModels.Count}件";
        }
        private void ResetSearchButton_Click(object sender, RoutedEventArgs e)
        {
            this.ResetSearchControls();
            this.SearchOrders();
        }
        private void ResetSearchControls()
        {
            this.CreatedDateFTextBox.Text = $"{DateTime.Now.Date.AddYears(-50):d}";
            this.CreatedDateTTextBox.Text = $"{DateTime.Now:d}";
            this.SearchNameTextBox.Text = "";
            this.SetOrderCount();
        }
        #endregion

        #region 受注検索結果DataGrid
        private void ClientNameUpButton_Click(object sender, RoutedEventArgs e)
        {
            var orders = this.OrderViewModels.Select(x => x.Model).OrderBy(x => x.ClientName).ThenByDescending(x => x.CreatedDate).ThenByDescending(x => x.Id).ToList();
            this.OrderViewModels.Clear();
            this.OrderViewModels.AddRange(orders.Select(x => new OrderViewModel(x)));
        }
        private void ClientNameDownButton_Click(object sender, RoutedEventArgs e)
        {
            var orders = this.OrderViewModels.Select(x => x.Model).OrderByDescending(x => x.ClientName).ThenByDescending(x => x.CreatedDate).ThenByDescending(x => x.Id).ToList();
            this.OrderViewModels.Clear();
            this.OrderViewModels.AddRange(orders.Select(x => new OrderViewModel(x)));
        }
        private void ClientNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            var text = comboBox.SelectedValue as string;

            if (string.IsNullOrEmpty(text)) { return; }

            this.OrderViewModels.Clear();
            
            if(text == this.allClientName)
            {
                this.OrderViewModels.AddRange(this.orders.Select(x => new OrderViewModel(x)));
            }
            else
            {
                this.OrderViewModels.AddRange(this.orders.Where(x => x.ClientName == text).Select(x => new OrderViewModel(x)));
            }

            this.SetOrderCount();
            this.OrderDataGrid.Focus();
        }
        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00200_EditOrderWindow(new Order());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (!w.IsChanged) { return; }

                this.SearchOrders();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void CopyOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = this.OrderDataGrid.SelectedItem as OrderViewModel;

                if (vm == null) { return; }

                var order = this.cd.OrderRepository.SelectById((int)vm.Model.Id);

                var clone = order.Clone();

                foreach(var p in clone.Products)
                {
                    foreach(var pf in p.ProductFiles)
                    {
                        pf.SourceFilePath = this.cd.OrderRepository.GetProductFilePath(clone.Id, pf.FileName);
                    }
                }

                clone.Id = null;
                clone.Name += " - コピー";

                var w = new Order00200_EditOrderWindow(clone);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                
                if (!w.IsChanged) { return; }
                
                this.SearchOrders();
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

                var order = this.cd.OrderRepository.SelectById((int)vm.Model.Id);
                var w = new Order00200_EditOrderWindow(order);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (!w.IsChanged) { return; }

                var index = this.OrderViewModels.IndexOf(vm);

                this.OrderViewModels.Remove(vm);

                if (w.IsDeleted) { return; }

                this.OrderViewModels.Insert(index, new OrderViewModel(this.cd.OrderRepository.SelectById((int)vm.Model.Id)));

                this.OrderDataGrid.SelectedItem = this.OrderViewModels[index];
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
                
                var order = this.cd.OrderRepository.SelectById((int)vm.Model.Id);

                this.ProductViewModels.Clear();
                this.ProductViewModels.AddRange(order.Products.Select(x => new ProductViewModel(x)));
                this.TotalAmountTextBlock.Text = $"総額 {this.ProductViewModels.Sum(x => x.TotalAmount):#,0}円";
                this.TotalAmountTextBlock2.Text = $"総額 {this.ProductViewModels.Sum(x => x.TotalAmount):#,0}円";
                if (this.ProductViewModels.Any())
                {
                    this.ProductDataGrid.SelectedItem = this.ProductViewModels[0];
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        #endregion


        private void ProductDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var vm = this.ProductDataGrid.SelectedItem as ProductViewModel;

                this.ResetProductPropertyViewModels();

                if (vm == null) { return; }

                this.SetProductToControls(vm.Model);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void ResetProductPropertyViewModels()
        {
            this.BoardViewModels.Clear();
            this.BoardLayerViewModels.Clear();
            this.CostViewModels.Clear();
            this.ProductFileViewModels.Clear();
            this.BoardViewModels2.Clear();
            this.BoardCostViewModels.Clear();
            this.KoguchiPasteCostViewModels.Clear();
            this.FinishCutCostViewModels.Clear();
            this.MakeupBoardPasteCostViewModels.Clear();
            this.PaintCostViewModels.Clear();
        }
        private void SetProductToControls(Product p)
        {
            this.ProductCategoryNameTextBlock.Text = this.cd.ProductCategoryInfoDict.GetValueOrDefault(p.ProductCategoryInfoCode)?.Name;
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
            this.ShikiriitaGawaitaHikaeTextBlock.Text = $"{p.ShikiriitaGawaitaHikae}";
            this.TanaitaGawaitaHikaeTextBlcok.Text = $"{p.TanaitaGawaitaHikae}";
            this.KoguchiPasteUnitPriceTextBlcok.Text = $"{p.KoguchiPasteUnitPrice}";
            this.FinishCutUnitPriceTextBlock.Text = $"{p.FinishCutUnitPrice}";
            this.FinishMarginTextBlock.Text = $"{p.FinishMargin}";
            this.CostTotalAmountTextBlock.Text = $"{p.Costs.Sum(x => x.TotalAmount):#,0}";
            this.CostTotalAmountTextBlock2.Text = $"合計 {p.Costs.Sum(x => x.TotalAmount):#,0}円";
            this.BoardCostTotalAmountTextBlock.Text = $"{p.BoardCosts.Sum(x => x.TotalAmount) / p.Quantity:#,0}";
            this.BoardCostTotalAmountTextBlock2.Text = $"合計 {p.BoardCosts.Sum(x => x.TotalAmount):#,0}円 ( {p.BoardCosts.Sum(x => x.TotalAmount) / p.Quantity:#,0}円 / 台 )";
            this.KoguchiPasteCostTotalAmountTextBlock.Text = $"{p.KoguchiPasteCosts.Sum(x => x.TotalAmount):#,0}";
            this.KoguchiPasteCostTotalAmountTextBlock2.Text = $"合計 {p.KoguchiPasteCosts.Sum(x => x.TotalAmount):#,0}円";
            this.FinishCutCostTotalAmountTextBlock.Text = $"{p.FinishCutCosts.Sum(x => x.TotalAmount):#,0}";
            this.FinishCutCostTotalAmountTextBlock2.Text = $"合計 {p.FinishCutCosts.Sum(x => x.TotalAmount):#,0}円";
            this.MakeupBoardPasteCostTotalAmountTextBlock.Text = $"{p.MakeupBoardPasteCosts.Sum(x => x.TotalAmount):#,0}";
            this.MakeupBoardPasteCostTotalAmountTextBlock2.Text = $"合計 {p.MakeupBoardPasteCosts.Sum(x => x.TotalAmount):#,0}円";
            this.PaintCostTotalAmountTextBlock.Text = $"{p.PaintCosts.Sum(x => x.TotalAmount):#,0}";
            this.PaintCostTotalAmountTextBlock2.Text = $"合計 {p.PaintCosts.Sum(x => x.TotalAmount):#,0}円";
            this.ProductUnitPriceTextBlock.Text = $"{p.GetUnitPrice():#,0}";


            var boardSizes = new BoardSizeCalculator(p).GetBoardSizes();

            this.BoardViewModels.Clear();
            this.BoardLayerViewModels.Clear();

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

            this.PaintCostViewModels.Clear();
            this.PaintCostViewModels.AddRange(p.PaintCosts.Select(x => new DisplayPaintCostViewModel(x)));
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
                var destFilePath = Path.Combine(this.cd.TempFileDirName, $"{vm.DisplayName}{Path.GetExtension(vm.Model.FileName)}");

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
        private void ShowPaintCostItemInfosButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00500_PaintCostItemInfoListWindow();
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
                foreach (var filePath in Directory.GetFiles(this.cd.TempFileDirName)) { File.Delete(filePath); }
            }
            catch (Exception)
            {
            }
        }

        private void OutputButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.OutputFile();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void OutputFile()
        {
            if (this.ProductViewModels.Count == 0)
            {
                this.cd.DialogService.ShowMessage("製品が存在しません");
                return;
            }

            var lines = new List<string>();
            var fields = new List<string>();

            fields.Clear();
            fields.Add($@"""カテゴリ""");
            fields.Add($@"""製品名""");
            fields.Add($@"""W""");
            fields.Add($@"""D""");
            fields.Add($@"""H""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""合計""");
            lines.Add(string.Join(",", fields));

            foreach (var pv in this.ProductViewModels)
            {
                fields.Clear();
                fields.Add($@"""{pv.ProductCategoryName}""");
                fields.Add($@"""{pv.Name}""");
                fields.Add($@"""{pv.Width}""");
                fields.Add($@"""{pv.Depth}""");
                fields.Add($@"""{pv.Height}""");
                fields.Add($@"""{pv.Quantity}""");
                fields.Add($@"""{pv.UnitPrice}""");
                fields.Add($@"""{pv.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""{this.ProductViewModels.Sum(x => x.TotalAmount)}""");
            lines.Add(string.Join(",", fields));

            var name = (this.OrderDataGrid.SelectedItem as OrderViewModel)?.Name;

            var filePath = Path.Combine(this.cd.TempFileDirName, $"{name}_原価計算.csv");

            Utility.FileWriter.WriteLine(string.Join("\r\n", lines), filePath, false);

            var app = new ProcessStartInfo();
            app.FileName = filePath;
            app.UseShellExecute = true;

            Process.Start(app);
        }
        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00700_ImportOrderWindow();
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (w.IsChanged)
                {
                    this.SearchOrders();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Export();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void Export()
        {
            var orders = new List<Order>();

            foreach (OrderViewModel vm in this.OrderDataGrid.SelectedItems)
            {
                orders.Add(this.cd.OrderRepository.SelectById((int)vm.Model.Id));
            }

            if (!orders.Any())
            {
                this.cd.DialogService.ShowMessage("エクスポートしたい受注を選択してください");
                return;
            }

            var coreDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Export_{DateTime.Now:yyyyMMddHHmmss}");

            Utility.DirectoryCreator.CreateSafely(coreDirPath);
            Utility.XmlWriter.WriteXml(this.cd.MaterialInfos, Path.Combine(coreDirPath, this.cd.ExportMaterialInfoFileName));
            Utility.XmlWriter.WriteXml(this.cd.ProductCategoryInfos, Path.Combine(coreDirPath, this.cd.ExportProductCategoryInfoFileName));
            Utility.XmlWriter.WriteXml(this.cd.PaintCostItemInfos, Path.Combine(coreDirPath, this.cd.ExportPaintCostItemInfoFileName));

            foreach (var order in orders)
            {
                var dirPath = Path.Combine(coreDirPath, $"{order.Id}");
                Utility.DirectoryCreator.CreateSafely(dirPath);
                Utility.XmlWriter.WriteXml(order, Path.Combine(dirPath, this.cd.ExportOrderFileName));
                foreach (var pf in order.Products.SelectMany(x => x.ProductFiles))
                {
                    var sPath = this.cd.OrderRepository.GetProductFilePath(order.Id, pf.FileName);
                    var dPath = Path.Combine(dirPath, pf.FileName);
                    File.Copy(sPath, dPath);
                }
            }

            this.cd.DialogService.ShowMessage($"デスクトップに「{Path.GetFileName(coreDirPath)}」フォルダを出力しました");
        }

        private void DisplayProductInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DisplayProductInfo();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void DisplayProductInfo()
        {
            var orderVm = this.OrderDataGrid.SelectedItem as OrderViewModel;
            var productVm = this.ProductDataGrid.SelectedItem as ProductViewModel;

            if (orderVm == null || productVm == null)
            {
                this.cd.DialogService.ShowMessage("製品を選択してください");
                return;
            }

            var o = orderVm.Model;
            var p = productVm.Model;

            var lines = new List<string>();
            var fields = new List<string>();

            fields.Add($@"""受注仕様""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""作成日""");
            fields.Add($@"""""");
            fields.Add($@"""{o.CreatedDate:d}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""物件名""");
            fields.Add($@"""""");
            fields.Add($@"""{o.Name}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""提出先""");
            fields.Add($@"""""");
            fields.Add($@"""{o.ClientName}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""納品日""");
            fields.Add($@"""""");
            fields.Add($@"""{o.DeliveryDate:d}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""備考""");
            fields.Add($@"""""");
            fields.Add($@"""{o.Remarks}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""製品仕様""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""カテゴリ""");
            fields.Add($@"""""");
            fields.Add($@"""{productVm.ProductCategoryName}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""製品名""");
            fields.Add($@"""""");
            fields.Add($@"""{productVm.Name}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""数量""");
            fields.Add($@"""""");
            fields.Add($@"""{productVm.Quantity}""");
            fields.Add($@"""台""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""本体寸法(W)""");
            fields.Add($@"""""");
            fields.Add($@"""{productVm.Width}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""本体寸法(D)""");
            fields.Add($@"""""");
            fields.Add($@"""{productVm.Depth}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""本体寸法(H)""");
            fields.Add($@"""""");
            fields.Add($@"""{productVm.Height}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""フィラL(W)""");
            fields.Add($@"""""");
            fields.Add($@"""{p.FillerL}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""フィラR(W)""");
            fields.Add($@"""""");
            fields.Add($@"""{p.FillerR}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""LVL(W)""");
            fields.Add($@"""""");
            fields.Add($@"""{p.LvlWidth}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""アンコピッチ""");
            fields.Add($@"""""");
            fields.Add($@"""{p.AnkoPitch}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""台輪(H)""");
            fields.Add($@"""""");
            fields.Add($@"""{p.DaiwaHeight}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""扉:天板隙間""");
            fields.Add($@"""""");
            fields.Add($@"""{p.TobiraTenitaSukima}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""扉:天板控え""");
            fields.Add($@"""""");
            fields.Add($@"""{p.TobiraTenitaHikae}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""扉:側板目地""");
            fields.Add($@"""""");
            fields.Add($@"""{p.TobiraGawaitaMokuji}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""扉間木目地""");
            fields.Add($@"""""");
            fields.Add($@"""{p.TobiraKanMokuji}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""木口貼り単価""");
            fields.Add($@"""""");
            fields.Add($@"""{p.KoguchiPasteUnitPrice}""");
            fields.Add($@"""mm/円""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""仕上カット単価""");
            fields.Add($@"""""");
            fields.Add($@"""{p.FinishCutUnitPrice}""");
            fields.Add($@"""mm/円""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""製作幅""");
            fields.Add($@"""""");
            fields.Add($@"""{p.FinishMargin}""");
            fields.Add($@"""mm""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""部位""");
            fields.Add($@"""製作縦""");
            fields.Add($@"""製作横""");
            fields.Add($@"""仕上縦""");
            fields.Add($@"""仕上横""");
            fields.Add($@"""厚さ""");
            fields.Add($@"""数量""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.BoardViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.BoardName}""");
                fields.Add($@"""{this.BoardViewModels2[i].Length}""");
                fields.Add($@"""{this.BoardViewModels2[i].Width}""");
                fields.Add($@"""{vm.Length}""");
                fields.Add($@"""{vm.Width}""");
                fields.Add($@"""{this.BoardViewModels2[i].Thickness}""");
                fields.Add($@"""{vm.Quantity}""");
                lines.Add(string.Join(",", fields));
            }

            #region 板取計算
            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""板取計算""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""名称""");
            fields.Add($@"""縦""");
            fields.Add($@"""横""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""小計""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.BoardCostViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.Name}""");
                fields.Add($@"""{vm.Length}""");
                fields.Add($@"""{vm.Width}""");
                fields.Add($@"""{vm.Quantity}""");
                fields.Add($@"""{vm.UnitPrice}""");
                fields.Add($@"""{vm.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            fields.Add($@"""{p.BoardCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""一台当たり""");
            fields.Add($@"""{p.BoardCosts.Sum(x => x.TotalAmount) / p.Quantity:C}""");
            lines.Add(string.Join(",", fields));

            #endregion

            #region 木口貼り
            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""木口貼り""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""部位""");
            fields.Add($@"""縦""");
            fields.Add($@"""横""");
            fields.Add($@"""箇所""");
            fields.Add($@"""長さ""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""小計""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.KoguchiPasteCostViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.BoardName}""");
                fields.Add($@"""{vm.Length}""");
                fields.Add($@"""{vm.Width}""");
                fields.Add($@"""{vm.KoguchiMakeupArea}""");
                fields.Add($@"""{vm.UnitLength}""");
                fields.Add($@"""{vm.Quantity}""");
                fields.Add($@"""{vm.UnitPrice}""");
                fields.Add($@"""{vm.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            fields.Add($@"""{p.KoguchiPasteCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            #endregion

            #region 仕上カット
            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""仕上カット""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""部位""");
            fields.Add($@"""縦""");
            fields.Add($@"""横""");
            fields.Add($@"""外周""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""小計""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.FinishCutCostViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.BoardName}""");
                fields.Add($@"""{vm.Length}""");
                fields.Add($@"""{vm.Width}""");
                fields.Add($@"""{vm.UnitLength}""");
                fields.Add($@"""{vm.Quantity}""");
                fields.Add($@"""{vm.UnitPrice}""");
                fields.Add($@"""{vm.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            fields.Add($@"""{p.FinishCutCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            #endregion

            #region 化粧板貼り
            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""化粧板貼り""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""部位""");
            fields.Add($@"""縦""");
            fields.Add($@"""横""");
            fields.Add($@"""面積""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""小計""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.MakeupBoardPasteCostViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.BoardName}""");
                fields.Add($@"""{vm.Length}""");
                fields.Add($@"""{vm.Width}""");
                fields.Add($@"""{vm.UnitLength}""");
                fields.Add($@"""{vm.Quantity}""");
                fields.Add($@"""{vm.UnitPrice}""");
                fields.Add($@"""{vm.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            fields.Add($@"""{p.MakeupBoardPasteCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            #endregion

            #region 塗装
            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""塗装""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""部位""");
            fields.Add($@"""縦""");
            fields.Add($@"""横""");
            fields.Add($@"""箇所""");
            fields.Add($@"""面積""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""小計""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.PaintCostViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.BoardName}""");
                fields.Add($@"""{vm.Length}""");
                fields.Add($@"""{vm.Width}""");
                fields.Add($@"""{vm.PaintArea}""");
                fields.Add($@"""{vm.UnitLength}""");
                fields.Add($@"""{vm.Quantity}""");
                fields.Add($@"""{vm.UnitPrice}""");
                fields.Add($@"""{vm.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            fields.Add($@"""{p.PaintCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            #endregion

            #region 費用
            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""費用""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""項目""");
            fields.Add($@"""数量""");
            fields.Add($@"""単価""");
            fields.Add($@"""小計""");
            lines.Add(string.Join(",", fields));

            foreach (var (vm, i) in this.CostViewModels.WithIndex())
            {
                fields.Clear();
                fields.Add($@"""{vm.Name}""");
                fields.Add($@"""{vm.Quantity}""");
                fields.Add($@"""{vm.UnitPrice}""");
                fields.Add($@"""{vm.TotalAmount}""");
                lines.Add(string.Join(",", fields));
            }
            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            fields.Add($@"""{p.Costs.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            #endregion

            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""総合計金額""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""""");
            fields.Add($@"""合計""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""板取""");
            fields.Add($@"""{p.BoardCosts.Sum(x => x.TotalAmount) / p.Quantity:C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""木口貼り""");
            fields.Add($@"""{p.KoguchiPasteCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""仕上カット""");
            fields.Add($@"""{p.FinishCutCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""化粧板貼り""");
            fields.Add($@"""{p.MakeupBoardPasteCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""塗装""");
            fields.Add($@"""{p.PaintCosts.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""費用""");
            fields.Add($@"""{p.Costs.Sum(x => x.TotalAmount):C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""総合計""");
            fields.Add($@"""{p.GetUnitPrice():C}""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""総合計 × 台数""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""総合計""");
            fields.Add($@"""台数""");
            fields.Add($@"""総合計 × 台数""");
            lines.Add(string.Join(",", fields));

            fields.Clear();
            fields.Add($@"""{p.GetUnitPrice():C}""");
            fields.Add($@"""{p.Quantity}""");
            fields.Add($@"""{p.GetUnitPrice() * p.Quantity:C}""");
            lines.Add(string.Join(",", fields));

            var filePath = Path.Combine(this.cd.TempFileDirName, $"{o.Name}_{p.Name}_原価計算詳細.csv");

            Utility.FileWriter.WriteLine(string.Join("\r\n", lines), filePath, false);

            var app = new ProcessStartInfo();
            app.FileName = filePath;
            app.UseShellExecute = true;

            Process.Start(app);

        }

        
    }
}
