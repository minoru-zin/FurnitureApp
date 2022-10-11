using FurnitureApp.Contents.Orders.Order00300;
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
        private ControlFormatter cf = new ControlFormatter();

        public Cost Model { get; private set; } 

        public bool IsChanged { get; private set; } = false;
        public bool IsDeleted { get; private set; } = false;
        public Order00400_EditCostWindow(Cost c)
        {
            InitializeComponent();
            this.Model = c.Clone();
            this.SetInfoToControls();
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
            this.NameTextBox.Focus();
        }
        
        private void SequenceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void QuantityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void SetInfoToControls()
        {
            this.NameTextBox.Text = this.Model.Name;
            this.UnitPriceTextBox.Text = $"{this.Model.UnitPrice}";
            this.QuantityTextBox.Text = $"{this.Model.Quantity}";
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
            this.Model.Name = this.NameTextBox.Text;
            this.Model.UnitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPriceTextBox.Text) ?? 0;
            this.Model.Quantity = Utility.NumberFormatter.GetNullInt(this.QuantityTextBox.Text) ?? 0;
            this.Model.TotalAmount = this.Model.UnitPrice * this.Model.Quantity;

            if (string.IsNullOrEmpty(this.Model.Name)) { throw new Exception("名称が不適"); }
            if (this.Model.UnitPrice < 0) { throw new Exception("単価が不適"); }
            if (this.Model.Quantity < 0) { throw new Exception("数量が不適"); }

            this.IsChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.cd.DialogService.ShowComfirmationMessageDialog("本当に削除しますか？")) { return; }

            this.IsChanged = true;
            this.IsDeleted = true;
            this.Close();
        }

        private void SelectCostItemInfoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = new Order00400_SelectCostItemInfoWIndow();

                w.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                w.ShowDialog();

                if (w.IsSelected)
                {
                    this.NameTextBox.Text = w.Model.Name;
                    this.UnitPriceTextBox.Text = $"{w.Model.UnitPrice}";
                    this.QuantityTextBox.Focus();
                    this.QuantityTextBox.SelectAll();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
    }
}

