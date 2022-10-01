using FurnitureApp.Models;
using FurnitureApp.Repository.CostItemInfos;
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

namespace FurnitureApp.Contents.Orders.Order00400
{
    /// <summary>
    /// Order00400_EditCostWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00400_EditCostWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        public List<DisplayInfo<CostItemInfo>> CostItemInfos { get; }
        public List<Cost> Costs { get; } = new List<Cost>();
        public bool IsChanged { get; private set; } = false;
        public ObservableCollection<CostViewModel> ViewModels { get; } = new ObservableCollection<CostViewModel>();

        public Order00400_EditCostWindow(IEnumerable<Cost> costs)
        {
            InitializeComponent();
            this.DataContext = this;
            this.CostItemInfos = this.cd.CostItemInfos.Select(x => new DisplayInfo<CostItemInfo>(x, x.Name)).ToList();
            this.ViewModels.AddRange(costs.Select(x => new CostViewModel(x)));
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.ViewModels.Add(new CostViewModel(new Cost()));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.CostDataGrid.SelectedItem as CostViewModel;

            if (vm == null) { return; }

            var index = this.ViewModels.IndexOf(vm);

            this.ViewModels.Remove(vm);

            if (!this.ViewModels.Any()) { return; }

            if (this.ViewModels.Count > index)
            {
                this.CostDataGrid.SelectedItem = this.ViewModels[index];
            }
            else
            {
                this.CostDataGrid.SelectedItem = this.ViewModels[this.ViewModels.Count - 1];
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.CostDataGrid.SelectedItem as CostViewModel;

            if (vm == null) { return; }

            var index = this.ViewModels.IndexOf(vm);

            if (index == 0) { return; }

            var clone = this.ViewModels.ToList()[index];

            this.ViewModels.Remove(vm);

            this.ViewModels.Insert(index - 1, clone);

            this.CostDataGrid.SelectedItem = clone;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.CostDataGrid.SelectedItem as CostViewModel;

            if (vm == null) { return; }

            var index = this.ViewModels.IndexOf(vm);

            if (index == this.ViewModels.Count - 1) { return; }

            var clone = this.ViewModels.ToList()[index];

            this.ViewModels.Remove(vm);

            this.ViewModels.Insert(index + 1, clone);

            this.CostDataGrid.SelectedItem = clone;
        }

        private void NameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var vm = this.CostDataGrid.SelectedItem as CostViewModel;

            if (vm == null) { return; }

            var costItemInfo =  comboBox.SelectedValue as CostItemInfo;

            if (costItemInfo == null) { return; }
            vm.Name = costItemInfo.Name;
            vm.UnitPrice = $"{costItemInfo.UnitPrice}";

            this.ViewModels.Update();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Update();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void Update()
        {
            var vms = this.ViewModels.Where(x => !string.IsNullOrEmpty($"{x.Name}{x.UnitPrice}{x.Quantity}"));

            this.Costs.Clear();
            this.Costs.AddRange(vms.Select(x => x.Model));

            this.IsChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
