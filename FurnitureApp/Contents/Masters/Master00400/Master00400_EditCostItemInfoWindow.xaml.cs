using FurnitureApp.Models;
using FurnitureApp.Repository.CostItemInfos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FurnitureApp.Contents.Masters.Master00400
{
    /// <summary>
    /// Master00400_EditCostItemInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00400_EditCostItemInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private ControlFormatter cf = new ControlFormatter();

        private CostItemInfo model;
        public bool IsChanged = false;
        public Master00400_EditCostItemInfoWindow(CostItemInfo m)
        {
            InitializeComponent();
            this.model = m.Clone();
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
            this.SequenceTextBox.Focus();
        }
        
        private void SequenceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void UpdatedDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDate(sender as TextBox);
        }
        private void SetInfoToControls()
        {
            this.SequenceTextBox.Text = $"{this.model.Sequence}";
            this.CategoryNameTextBox.Text = this.model.CategoryName;
            this.NameTextBox.Text = this.model.Name;
            this.UnitPriceTextBox.Text = $"{this.model.UnitPrice}";
            this.UpdatedDateTextBox.Text = $"{DateTime.Now.Date:d}";

            if(this.model.Id == null)
            {
                this.SequenceTextBox.Text = "";
                this.DeleteButton.IsEnabled = false;
            }
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
            this.model.Sequence = Utility.NumberFormatter.GetNullInt(this.SequenceTextBox.Text) ?? 0;
            this.model.CategoryName = this.CategoryNameTextBox.Text;
            this.model.Name = this.NameTextBox.Text;
            this.model.UnitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPriceTextBox.Text) ?? 0;
            this.model.UpdatedDate = Utility.DateTimeFormatter.GetDateTime(this.UpdatedDateTextBox.Text);

            if (this.model.Sequence <= 0) { throw new Exception("順番が不適"); }
            if (string.IsNullOrEmpty(this.model.CategoryName)) { throw new Exception("カテゴリ名が不適"); }
            if (string.IsNullOrEmpty(this.model.Name)) { throw new Exception("名称が不適"); }
            if (this.model.UnitPrice < 0) { throw new Exception("単価が不適"); }
            if (this.model.UpdatedDate == null) { throw new Exception("更新日が不適"); }


            if (this.model.Id == null)
            {
                this.cd.CostItemInfoRepository.Insert(this.model);
            }
            else
            {
                this.cd.CostItemInfoRepository.Update(this.model);
            }
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

            this.cd.CostItemInfoRepository.Delete(this.model);

            this.IsChanged = true;
            this.Close();
        }

        
    }
}

