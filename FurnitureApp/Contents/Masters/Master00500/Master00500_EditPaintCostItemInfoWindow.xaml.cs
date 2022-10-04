using FurnitureApp.Models;
using FurnitureApp.Repository.PaintCostItemInfos;
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

namespace FurnitureApp.Contents.Masters.Master00500
{
    /// <summary>
    /// Master00400_EditCostItemInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00500_EditPaintCostItemInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private ControlFormatter cf = new ControlFormatter();

        private PaintCostItemInfo model;
        public bool IsChanged = false;
        public Master00500_EditPaintCostItemInfoWindow(PaintCostItemInfo m)
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
            this.UnitPriceTextBox.Text = $"{this.model.UnitPrice}";

            if (this.model.Id == null)
            {
                this.CodeTextBox.Text = $"{(this.cd.PaintCostItemInfos.Max(x => x.Code) ?? 0) + 1}";
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
            this.model.UnitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPriceTextBox.Text) ?? 0;

            if (this.model.Sequence <= 0) { throw new Exception("順番が不適"); }
            if (this.model.Code == null) { throw new Exception("コードが不適"); }
            if (string.IsNullOrEmpty(this.model.Name)) { throw new Exception("名称が不適"); }
            if (this.model.UnitPrice < 0) { throw new Exception("単価が不適"); }

            if (this.model.Id == null)
            {
                this.cd.PaintCostItemInfoRepository.Insert(this.model);
            }
            else
            {
                this.cd.PaintCostItemInfoRepository.Update(this.model);
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

            this.cd.PaintCostItemInfoRepository.Delete(this.model);

            this.IsChanged = true;
            this.Close();
        }


    }
}

