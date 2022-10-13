using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
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

namespace FurnitureApp.Contents.Masters.Master00100
{
    /// <summary>
    /// Master00100_EditMaterialInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00100_EditMaterialInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private ControlFormatter cf = new ControlFormatter();

        public List<DisplayInfo<CutType>> CutTypes { get; }
        private MaterialInfo model;
        public bool IsChanged = false;
        public Master00100_EditMaterialInfoWindow(MaterialInfo m)
        {
            InitializeComponent();
            this.DataContext = this;
            this.model = m.Clone();
            this.CutTypes = this.cd.CutTypes;
            
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
        private void CodeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }

        private void ThicknessTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDoubleNumberTextBox(sender as TextBox);
        }

        private void PasteUnitPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDoubleNumberTextBox(sender as TextBox);
        }
        private void UpdatedDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDate(sender as TextBox);
        }
        private void SetInfoToControls()
        {
            this.SequenceTextBox.Text = $"{this.model.Sequence}";
            this.CodeTextBox.Text = $"{this.model.Code}";
            this.NameTextBox.Text = this.model.Name;
            this.ThicknessTextBox.Text = $"{this.model.Thickness}";
            this.PasteUnitPriceTextBox.Text = $"{this.model.PasteUnitPrice}";
            this.CutTypeComboBox.SelectedValue = this.model.CutType;
            this.UpdatedDateTextBox.Text = $"{DateTime.Now.Date:d}";

            if(this.model.Id == null)
            {
                this.DeleteButton.IsEnabled = false;
                this.SequenceTextBox.Text = "";
                this.CodeTextBox.Text = $"{(this.cd.MaterialInfos.Max(x => x.Code) ?? 0) + 1}";
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
            this.model.Thickness = Utility.NumberFormatter.GetNullDouble(this.ThicknessTextBox.Text) ?? 0;
            this.model.PasteUnitPrice = Utility.NumberFormatter.GetNullInt(this.PasteUnitPriceTextBox.Text);
            this.model.CutType = (CutType)this.CutTypeComboBox.SelectedValue;
            this.model.UpdatedDate = Utility.DateTimeFormatter.GetDateTime(this.UpdatedDateTextBox.Text);

            if (this.model.Sequence <= 0) { throw new Exception("順番が不適"); }
            if (this.model.Code == null) { throw new Exception("コードが不適"); }
            if (string.IsNullOrEmpty(this.model.Name)) { throw new Exception("名称が不適"); }
            if (this.model.Thickness <= 0) { throw new Exception("厚さが不適"); }
            if (this.model.PasteUnitPrice < 0) { throw new Exception("貼り単価が不適"); }
            if (this.model.UpdatedDate == null) { throw new Exception("更新日が不適"); }

            if (this.model.Id == null)
            {
                this.cd.MaterialInfoRepository.Insert(this.model);
            }
            else
            {
                this.cd.MaterialInfoRepository.Update(this.model);
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

            this.cd.MaterialInfoRepository.Delete(this.model);

            this.IsChanged = true;
            this.Close();
        }

        
    }
}
