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

        private Dictionary<int?, List<Order00600_ProductViewModel>> productViewModelDict = new Dictionary<int?, List<Order00600_ProductViewModel>>();
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
            this.SetAllViewModels();    
            this.SetProductViewModels();
        }

        private void SetAllViewModels()
        {
            var orders = this.cd.OrderRepository.SelectAllUpToProductTopOnly();

            var vms = new List<Order00600_ProductViewModel>();

            foreach(var o in orders)
            {
                foreach(var p in o.Products)
                {
                    vms.Add(new Order00600_ProductViewModel(o, p));
                }
            }
            
            this.productViewModelDict.Clear();
            foreach(var g in vms.GroupBy(x => new { x.ProductCategoryCode, x.ProductCategoryName, x.ProductCategorySequence}).OrderBy(x => x.Key.ProductCategorySequence))
            {
                this.productViewModelDict.Add(g.Key.ProductCategoryCode, g.ToList());
            }
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

            this.ProductViewModels.AddRange(this.productViewModelDict.GetValueOrDefault(pCode));
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
