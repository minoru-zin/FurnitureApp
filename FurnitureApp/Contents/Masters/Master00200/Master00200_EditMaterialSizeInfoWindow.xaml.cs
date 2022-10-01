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
        public List<DisplayInfo<int?>> Materials { get; } = new List<DisplayInfo<int?>>();

        private MaterialSizeInfo model;
        public bool IsChanged = false;
        public Master00200_EditMaterialSizeInfoWindow(MaterialSizeInfo m)
        {
            InitializeComponent();
            this.DataContext = this;

            this.model = m.Clone();

            var materialInfos = this.cd.MaterialInfoRepository.SelectAll();

            this.Materials.AddRange(materialInfos.Select(x => new DisplayInfo<int?>(x.Id, x.Name)));

            this.SetInfoToControls();
        }

        private void SetInfoToControls()
        {
            this.MaterialComboBox.SelectedValue = this.model.MaterialInfoId;
            this.NameTextBox.Text = this.model.Name;
            this.LengthTextBox.Text = $"{this.model.Length}";
            this.WidthTextBox.Text = $"{this.model.Width}";
            this.UnitPriceTextBox.Text = $"{this.model.UnitPrice}";
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
            this.model.MaterialInfoId = (int?)this.MaterialComboBox.SelectedValue;
            this.model.Name = this.NameTextBox.Text;
            this.model.Length = Utility.NumberFormatter.GetNullDouble(this.LengthTextBox.Text);
            this.model.Width = Utility.NumberFormatter.GetNullDouble(this.WidthTextBox.Text);
            this.model.UnitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPriceTextBox.Text);

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

