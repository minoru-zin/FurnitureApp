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

namespace FurnitureApp.Contents.Orders.Order00600
{
    /// <summary>
    /// Order00600_SelectProductWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00600_SelectProductWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        public ObservableCollection<Order00600_ProductViewModel> ProductViewModels { get; } = new ObservableCollection<Order00600_ProductViewModel>();
        public List<DisplayInfo<int?>> ProductCategoryInfos { get; } = new List<DisplayInfo<int?>>();
        public Product Product { get; private set; }
        public bool IsSelected { get; private set; } = false;
        public Order00600_SelectProductWindow(int? productCategoryInfoId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.ProductCategoryInfoComboBox.SelectedValue = productCategoryInfoId;
            this.ProductCategoryInfos.AddRange(this.cd.ProductCategoryInfos.Select(x => new DisplayInfo<int?>(x.Id, x.Name)));
            this.SetProductViewModels();
        }
        private void ProductCategoryInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SetProductViewModels();
        }
        private void SetProductViewModels()
        {
            this.ProductViewModels.Clear();

            var pCode = this.ProductCategoryInfoComboBox.SelectedValue as int?;

            if (pCode == null) { return; }

            this.ProductViewModels.AddRange(this.cd.OrderRepository.SelectTopOnlyByProductCategoryInfoCode((int)pCode).Select(x => new Order00600_ProductViewModel(x)));
        }
        private void ProductDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = this.ProductDataGrid.SelectedItem as Order00600_ProductViewModel;
            
            if(vm == null) { return; }

            var order = this.cd.OrderRepository.SelectById((int)vm.Model.OrderId);
            this.Product = order.Products.First(x => x.Id == vm.Model.Id);
            this.IsSelected = true;
            this.Close();
        }
    }
}
