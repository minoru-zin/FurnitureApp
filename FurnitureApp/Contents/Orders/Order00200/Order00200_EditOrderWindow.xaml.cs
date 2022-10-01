using FurnitureApp.Contents.Orders.Order00300;
using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.ProductCategoryInfos;
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

namespace FurnitureApp.Contents.Orders.Order00200
{
    /// <summary>
    /// Order00200_EditOrderWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00200_EditOrderWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        public ObservableCollection<ProductViewModel> ProductViewModels { get; } = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<BoardSizeViewModel> BoardSizeViewModels { get; } = new ObservableCollection<BoardSizeViewModel>();
        public ObservableCollection<CutSizeViewModel> CutSizeViewModels { get; } = new ObservableCollection<CutSizeViewModel>();
        public ObservableCollection<StandardBoardCostViewModel> StandardBoardCostViewModels { get; } = new ObservableCollection<StandardBoardCostViewModel>();

        private Dictionary<int?, ProductCategoryInfo> productCateogryInfoDict;
        private Order order;
        public bool IsChanged = false;
        public Order00200_EditOrderWindow(Order order)
        {
            InitializeComponent();
            this.DataContext = this;
            this.order = order.Clone();
            this.productCateogryInfoDict = this.cd.ProductCategoryInfos.ToDictionary(x => x.Id);
            this.SetOrderToControls();
            this.SetTotalAmount();
        }
        private void SetOrderToControls()
        {
            this.CreatedDateTextBox.Text = $"{this.order.CreatedDate:d}";
            this.NameTextBox.Text = this.order.Name;
            this.ClientNameTextBox.Text = this.order.ClientName;
            this.DeliveryDateTextBox.Text = $"{this.order.DeliveryDate:d}";
            this.RemarksTextBox.Text = this.order.Remarks;

            this.ProductViewModels.AddRange(this.order.Products.Select(x => new ProductViewModel(x, this.productCateogryInfoDict.GetValueOrDefault(x.ProductCategoryInfoId)?.Name)));
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
                logger.Error(ex);
            }
        }

        private void Update()
        {
            this.order.CreatedDate = Utility.DateTimeFormatter.GetDateTime(this.CreatedDateTextBox.Text);
            this.order.Name = this.NameTextBox.Text;
            this.order.ClientName = this.ClientNameTextBox.Text;
            this.order.DeliveryDate = Utility.DateTimeFormatter.GetDateTime(this.DeliveryDateTextBox.Text);
            this.order.Remarks = this.RemarksTextBox.Text;
            this.order.Products = this.ProductViewModels.Select(x => x.Model).ToList();

            if (this.order.Id == null)
            {
                // add
                this.cd.OrderRepository.Insert(this.order);
            }
            else
            {
                // update
                this.cd.OrderRepository.Update(this.order);
            }

            this.IsChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00300_EditProductWindow(new Product());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (!w.IsChanged) { return; }

                this.ProductViewModels.Add(new ProductViewModel(w.Product, this.productCateogryInfoDict.GetValueOrDefault(w.Product.ProductCategoryInfoId)?.Name));

                this.SetTotalAmount();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void ProductDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = this.ProductDataGrid.SelectedItem as ProductViewModel;
                if (vm == null) { return; }

                var w = new Order00300_EditProductWindow(vm.Model);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (!w.IsChanged) { return; }

                var index = this.ProductViewModels.IndexOf(vm);

                this.ProductViewModels.Remove(vm);

                this.ProductViewModels.Insert(index, new ProductViewModel(w.Product, this.productCateogryInfoDict.GetValueOrDefault(w.Product.ProductCategoryInfoId)?.Name));

                this.SetTotalAmount();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void SetTotalAmount()
        {
            this.TotalAmountTextBlock.Text = $"総額 {this.ProductViewModels.Sum(x => x.TotalAmount)}円";
        }
    }
}
