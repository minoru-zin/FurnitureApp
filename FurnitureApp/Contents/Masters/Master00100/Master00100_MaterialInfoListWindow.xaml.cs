using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
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

namespace FurnitureApp.Contents.Masters.Master00100
{
    /// <summary>
    /// Master00100_MaterialInfoListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00100_MaterialInfoListWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        public ObservableCollection<MaterialInfoViewModel> ViewModels { get; } = new ObservableCollection<MaterialInfoViewModel>();

        public Master00100_MaterialInfoListWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.DisplayInfos();
        }
        private void DisplayInfos()
        {
            this.ViewModels.Clear();

            this.cd.RefreshMasters();

            foreach(var m in this.cd.MaterialInfos)   
            {
                this.ViewModels.Add(new MaterialInfoViewModel(m));
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00100_EditMaterialInfoWindow(new MaterialInfo());
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
                var vm = this.DataGrid.SelectedItem as MaterialInfoViewModel;

                if (vm == null) { return; }

                var w = new Master00100_EditMaterialInfoWindow(vm.Model);
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
                var vm = this.DataGrid.SelectedItem as MaterialInfoViewModel;

                if (vm == null) { return; }

                var clone = vm.Model.Clone();
                clone.Id = null;
                var w = new Master00100_EditMaterialInfoWindow(clone);
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
