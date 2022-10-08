using FurnitureApp.Models;
using FurnitureApp.Repository.CostItemInfos;
using NLog.Layouts;
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
using WinCopies.Util;

namespace FurnitureApp.Contents.Orders.Order00400
{
    /// <summary>
    /// Order00400_SelectCostItemInfoWIndow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00400_SelectCostItemInfoWIndow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        private List<DisplayInfo<CostItemInfo>> allCostItemInfos;
        private readonly string allCategoryText = "すべて";
        public List<string> Categories { get; }
        public ObservableCollection<DisplayInfo<CostItemInfo>> CostItemInfos { get; } = new ObservableCollection<DisplayInfo<CostItemInfo>>();
        public CostItemInfo Model { get; private set; }
        public bool IsSelected { get; private set; } = false;
        public Order00400_SelectCostItemInfoWIndow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.allCostItemInfos = this.cd.CostItemInfos.Select(x => new DisplayInfo<CostItemInfo>(x, x.Name)).ToList();
            this.Categories = new List<string> { this.allCategoryText };
            this.Categories.AddRange(this.cd.CostItemInfos.GroupBy(x => x.CategoryName).Select(x => x.Key));
            this.CategoryDataGrid.SelectedValue = this.allCategoryText;
        }

        private void CategoryDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var categoryName = this.CategoryDataGrid.SelectedValue as string;

            if (categoryName == null) { return; }

            this.CostItemInfos.Clear();

            if (categoryName == this.allCategoryText)
            {
                this.CostItemInfos.AddRange(this.allCostItemInfos);
            }
            else
            {
                this.CostItemInfos.AddRange(this.allCostItemInfos.Where(x => x.Code.CategoryName == categoryName));
            }
        }

        private void CostItemInfoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var vm = this.CostItemInfoDataGrid.SelectedItem as DisplayInfo<CostItemInfo>;

            if (vm == null) { return; }

            this.Model = vm.Code;
            this.IsSelected = true;
            this.Close();
        }
    }
}
