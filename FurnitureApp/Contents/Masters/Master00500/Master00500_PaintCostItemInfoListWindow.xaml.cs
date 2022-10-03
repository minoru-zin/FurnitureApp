using FurnitureApp.Models;
using FurnitureApp.Repository.PaintCostItemInfos;
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

namespace FurnitureApp.Contents.Masters.Master00500
{
    /// <summary>
    /// Master00400_CostItemInfoListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00500_PaintCostItemInfoListWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        public ObservableCollection<PaintCostItemInfoViewModel> ViewModels { get; } = new ObservableCollection<PaintCostItemInfoViewModel>();

        public Master00500_PaintCostItemInfoListWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.DisplayInfos();
        }
        private void DisplayInfos()
        {
            this.ViewModels.Clear();

            this.cd.RefreshMasters();

            foreach (var m in this.cd.PaintCostItemInfos)
            {
                this.ViewModels.Add(new PaintCostItemInfoViewModel(m));
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00500_EditPaintCostItemInfoWindow(new PaintCostItemInfo());
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
                var vm = this.DataGrid.SelectedItem as PaintCostItemInfoViewModel;

                if (vm == null) { return; }

                var w = new Master00500_EditPaintCostItemInfoWindow(vm.Model);
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