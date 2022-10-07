using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialSizeInfos;
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

namespace FurnitureApp.Contents.Masters.Master00200
{
    /// <summary>
    /// Master00200_EditMaterialSizeInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00200_EditMaterialSizeInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        private ControlFormatter cf = new ControlFormatter();
        public List<DisplayInfo<int?>> Materials { get; } = new List<DisplayInfo<int?>>();

        private MaterialSizeInfo model;
        public bool IsChanged = false;
        public Master00200_EditMaterialSizeInfoWindow(MaterialSizeInfo m)
        {
            InitializeComponent();
            this.DataContext = this;

            this.model = m.Clone();

            var materialInfos = this.cd.MaterialInfoRepository.SelectAll();

            this.Materials.AddRange(materialInfos.Select(x => new DisplayInfo<int?>(x.Code, x.Name)));

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
            this.MaterialComboBox.Focus();
            this.MaterialComboBox.SelectionChanged += MaterialComboBox_SelectionChanged;
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as System.Windows.Controls.TextBox;

            if (textBox == null) { return; }

            textBox.SelectAll();
        }
        private void MaterialComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.NameTextBox.Text = this.Materials.First(x => x.Code == (int?)this.MaterialComboBox.SelectedValue).DisplayName;
        }

        private void LengthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDoubleNumberTextBox(sender as TextBox);
        }

        private void UnitPriceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetIntNumberTextBox(sender as TextBox);
        }
        private void UpdatedDateTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.cf.SetDate(this.UpdatedDateTextBox);
        }
        private void SetInfoToControls()
        {
            this.MaterialComboBox.SelectedValue = this.model.MaterialInfoCode;
            this.NameTextBox.Text = this.model.Name;
            this.LengthTextBox.Text = $"{this.model.Length}";
            this.WidthTextBox.Text = $"{this.model.Width}";
            this.UnitPriceTextBox.Text = $"{this.model.UnitPrice}";
            this.UpdatedDateTextBox.Text = $"{this.model.UpdatedDate:d}";
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
            this.model.MaterialInfoCode = (int?)this.MaterialComboBox.SelectedValue;
            this.model.Name = this.NameTextBox.Text;
            this.model.Length = Utility.NumberFormatter.GetNullDouble(this.LengthTextBox.Text) ?? 0;
            this.model.Width = Utility.NumberFormatter.GetNullDouble(this.WidthTextBox.Text) ?? 0;
            this.model.UnitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPriceTextBox.Text) ?? 0;
            this.model.UpdatedDate = Utility.DateTimeFormatter.GetDateTime(this.UpdatedDateTextBox.Text);

            if (this.model.MaterialInfoCode == null) { throw new Exception("素材が不適"); }
            if (string.IsNullOrEmpty(this.model.Name)) { throw new Exception("名称が不適"); }
            if (this.model.Length <= 0) { throw new Exception("縦が不適"); }
            if (this.model.Width <= 0) { throw new Exception("横が不適"); }
            if (this.model.UnitPrice < 0) { throw new Exception("単価が不適"); }
            if (this.model.UpdatedDate == null) { throw new Exception("更新日が不適"); }

            if (this.model.Id == null)
            {
                this.cd.MaterialSizeInfoRepository.Insert(this.model);
            }
            else
            {
                this.cd.MaterialSizeInfoRepository.Update(this.model);
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

            this.cd.MaterialSizeInfoRepository.Delete(this.model);

            this.IsChanged = true;
            this.Close();
        }

        
    }
}

