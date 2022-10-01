using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
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

namespace FurnitureApp.Contents.Masters.Master00100
{
    /// <summary>
    /// Master00100_EditMaterialInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00100_EditMaterialInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

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

        private void SetInfoToControls()
        {
            this.SequenceTextBox.Text = $"{this.model.Sequence}";
            this.NameTextBox.Text = this.model.Name;
            this.ThicknessTextBox.Text = $"{this.model.Thickness}";
            this.PasteUnitPriceTextBox.Text = $"{this.model.PasteUnitPrice}";
            this.CutTypeComboBox.SelectedValue = this.model.CutType;
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
            this.model.Sequence = Utility.NumberFormatter.GetNullInt(this.SequenceTextBox.Text);
            this.model.Name = this.NameTextBox.Text;
            this.model.Thickness = Utility.NumberFormatter.GetNullDouble(this.ThicknessTextBox.Text);
            this.model.PasteUnitPrice = Utility.NumberFormatter.GetNullInt(this.PasteUnitPriceTextBox.Text);
            this.model.CutType = (CutType)this.CutTypeComboBox.SelectedValue;

            if(this.model.Id == null)
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
