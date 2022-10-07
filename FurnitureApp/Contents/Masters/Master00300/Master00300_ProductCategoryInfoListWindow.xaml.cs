using FurnitureApp.Models;
using FurnitureApp.Repository.ProductCategoryInfos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FurnitureApp.Contents.Masters.Master00300
{
    /// <summary>
    /// Master00300_ProductCategoryInfoListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00300_ProductCategoryInfoListWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        public ObservableCollection<ProductCategoryInfoViewModel> ViewModels { get; } = new ObservableCollection<ProductCategoryInfoViewModel>();

        public Master00300_ProductCategoryInfoListWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.DisplayInfos();
        }
        private void DisplayInfos()
        {
            this.ViewModels.Clear();

            this.cd.RefreshMasters();

            foreach (var m in this.cd.ProductCategoryInfos)
            {
                this.ViewModels.Add(new ProductCategoryInfoViewModel(m));
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00300_EditProductCategoryInfoWindow(new ProductCategoryInfo());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (w.IsChanged)
                {
                    this.DisplayInfos();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var vm = this.DataGrid.SelectedItem as ProductCategoryInfoViewModel;

                if (vm == null) { return; }
                
                var w = new Master00300_EditProductCategoryInfoWindow(vm.Model);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (w.IsChanged)
                {
                    this.DisplayInfos();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = this.DataGrid.SelectedItem as ProductCategoryInfoViewModel;

                if (vm == null) { return; }

                var clone = vm.Model.Clone();
                clone.Id = null;
                var w = new Master00300_EditProductCategoryInfoWindow(clone);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();
                if (w.IsChanged)
                {
                    this.DisplayInfos();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }

        }
    }
}
