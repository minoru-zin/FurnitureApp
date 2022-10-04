using FurnitureApp.Models;
using FurnitureApp.Repository.ProductCategoryInfos;
using System;
using System.Collections.Generic;
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

namespace FurnitureApp.Contents.Masters.Master00300
{
    /// <summary>
    /// Master00300_EditProductCategoryInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00300_EditProductCategoryInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private ControlFormatter cf = new ControlFormatter();

        private ProductCategoryInfo model;
        public bool IsChanged = false;
        public Master00300_EditProductCategoryInfoWindow(ProductCategoryInfo m)
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
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as System.Windows.Controls.TextBox;

            if (textBox == null) { return; }

            textBox.SelectAll();
        }
        private void SequenceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void CodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void SetInfoToControls()
        {
            this.SequenceTextBox.Text = $"{this.model.Sequence}";
            this.CodeTextBox.Text = $"{this.model.Code}";
            this.NameTextBox.Text = this.model.Name;

            if (this.model.Id == null)
            {
                this.CodeTextBox.Text = $"{(this.cd.ProductCategoryInfos.Max(x => x.Code) ?? 0) + 1}";
            }
            else
            {
                this.CodeTextBox.IsEnabled = false;
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
            this.model.Code = Utility.NumberFormatter.GetNullInt(this.CodeTextBox.Text);
            this.model.Name = this.NameTextBox.Text;

            if (this.model.Sequence <= 0) { throw new Exception("順番が不適"); }
            if (this.model.Code == null) { throw new Exception("コードが不適"); }
            if (string.IsNullOrEmpty(this.model.Name)) { throw new Exception("名称が不適"); }

            if (this.model.Id == null)
            {
                this.cd.ProductCategoryInfoRepository.Insert(this.model);
            }
            else
            {
                this.cd.ProductCategoryInfoRepository.Update(this.model);
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

            this.cd.ProductCategoryInfoRepository.Delete(this.model);

            this.IsChanged = true;
            this.Close();
        }
        
    }
}
