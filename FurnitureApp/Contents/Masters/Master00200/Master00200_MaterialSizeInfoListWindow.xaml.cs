using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.MaterialSizeInfos;
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

namespace FurnitureApp.Contents.Masters.Master00200
{
    /// <summary>
    /// Master00200_MaterialSizeInfoListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00200_MaterialSizeInfoListWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        public ObservableCollection<MaterialSizeInfoViewModel> ViewModels { get; } = new ObservableCollection<MaterialSizeInfoViewModel>();
        private Dictionary<int?, MaterialInfo> materialDict;
        public Master00200_MaterialSizeInfoListWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            this.materialDict = this.cd.MaterialInfos.ToDictionary(x => x.Id);

            this.DisplayInfos();
        }
        private void DisplayInfos()
        {
            this.ViewModels.Clear();

            this.cd.RefreshMasters();

            foreach (var m in this.cd.MaterialSizeInfos)
            {
                this.ViewModels.Add(new MaterialSizeInfoViewModel(m, this.materialDict));
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Master00200_EditMaterialSizeInfoWindow(new MaterialSizeInfo());
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
                var vm = this.DataGrid.SelectedItem as MaterialSizeInfoViewModel;
                
                if (vm == null) { return; }

                var w = new Master00200_EditMaterialSizeInfoWindow(vm.Model);
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

