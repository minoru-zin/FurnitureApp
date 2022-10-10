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
using System.Windows.Forms.VisualStyles;
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
        private ControlFormatter cf = new ControlFormatter();

        public ObservableCollection<ProductViewModel> ProductViewModels { get; } = new ObservableCollection<ProductViewModel>();
        
        private Order oldOrder;
        public bool IsChanged = false;
        public bool IsDeleted = false;
        private bool canClose = false;

        public Order00200_EditOrderWindow(Order order)
        {
            InitializeComponent();
            this.DataContext = this;
            this.oldOrder = order.Clone();
            this.SetOrderToControls();
            this.SetTotalAmount();
            if (order.Id == null) { this.CreatedDateTextBox.Text = $"{DateTime.Now:d}"; }
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
            this.CreatedDateTextBox.Focus();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as System.Windows.Controls.TextBox;

            if (textBox == null) { return; }

            textBox.SelectAll();
        }
        
        private void CreatedDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDate(sender as TextBox);
        }
        private void SetOrderToControls()
        {
            this.CreatedDateTextBox.Text = $"{this.oldOrder.CreatedDate:d}";
            this.NameTextBox.Text = this.oldOrder.Name;
            this.ClientNameTextBox.Text = this.oldOrder.ClientName;
            this.DeliveryDateTextBox.Text = $"{this.oldOrder.DeliveryDate:d}";
            this.RemarksTextBox.Text = this.oldOrder.Remarks;

            this.ProductViewModels.AddRange(this.oldOrder.Products.Select(x => new ProductViewModel(x)));
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
            var newOrder = new Order { Id = this.oldOrder.Id };

            newOrder.CreatedDate = Utility.DateTimeFormatter.GetDateTime(this.CreatedDateTextBox.Text);
            newOrder.Name = this.NameTextBox.Text;
            newOrder.ClientName = this.ClientNameTextBox.Text;
            newOrder.DeliveryDate = Utility.DateTimeFormatter.GetDateTime(this.DeliveryDateTextBox.Text);
            newOrder.Remarks = this.RemarksTextBox.Text;
            newOrder.Products = this.ProductViewModels.Select(x => x.Model).ToList();

            if (newOrder.CreatedDate == null) { throw new Exception("作成日が不適"); }
            if (string.IsNullOrEmpty(newOrder.Name)) { throw new Exception("物件名が不適"); }

            if (newOrder.Id == null)
            {
                // add
                this.cd.OrderRepository.Insert(newOrder);
            }
            else
            {
                // update
                this.cd.OrderRepository.Update(newOrder);
            }

            this.IsChanged = true;
            this.canClose = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Delete();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void Delete()
        {
            if (!this.cd.DialogService.ShowComfirmationMessageDialog("本当に削除しますか？")) { return; }

            this.cd.OrderRepository.Delete(this.oldOrder);
            this.IsChanged = true;
            this.IsDeleted = true;
            this.canClose = true;
            this.Close();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00300_EditProductWindow(new Product());
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (!w.IsChanged) { return; }

                this.ProductViewModels.Add(new ProductViewModel(w.Product));

                this.SetTotalAmount();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void CopyProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = this.ProductDataGrid.SelectedItem as ProductViewModel;

                if (vm == null) { return; }

                var clone = vm.Model.Clone();
                clone.ProductFiles.Clear();
                clone.Name += " - コピー"; 
                var w = new Order00300_EditProductWindow(clone);
                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (!w.IsChanged) { return; }

                this.ProductViewModels.Add(new ProductViewModel(w.Product));

                this.SetTotalAmount();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void UpProductButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.ProductDataGrid.SelectedItem as ProductViewModel;

            if (vm == null) { return; }

            var index = this.ProductViewModels.IndexOf(vm);

            if (index == 0) { return; }

            var clone = this.ProductViewModels.ToList()[index];

            this.ProductViewModels.Remove(vm);

            this.ProductViewModels.Insert(index - 1, clone);

            this.ProductDataGrid.SelectedItem = clone;
        }

        private void DownProductButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.ProductDataGrid.SelectedItem as ProductViewModel;

            if (vm == null) { return; }

            var index = this.ProductViewModels.IndexOf(vm);

            if (index == this.ProductViewModels.Count - 1) { return; }

            var clone = this.ProductViewModels.ToList()[index];

            this.ProductViewModels.Remove(vm);

            this.ProductViewModels.Insert(index + 1, clone);

            this.ProductDataGrid.SelectedItem = clone;
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

                if (!w.IsDeleted)
                {
                    this.ProductViewModels.Insert(index, new ProductViewModel(w.Product));
                }

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
            this.TotalAmountTextBlock.Text = $"総額 {this.ProductViewModels.Sum(x => x.TotalAmount):#,0}円";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.canClose) { return; }

            if (!this.cd.DialogService.ShowComfirmationMessageDialog("編集中ですが、閉じますか？"))
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
