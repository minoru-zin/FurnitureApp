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

        private CostItemInfo model;
        public bool IsChanged = false;
        public Master00400_EditCostItemInfoWindow(CostItemInfo m)
        {
            InitializeComponent();
            this.model = m.Clone();
            this.SetInfoToControls();
        }

        private void SetInfoToControls()
        {
            this.SequenceTextBox.Text = $"{this.model.Sequence}";
            this.NameTextBox.Text = this.model.Name;
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
            this.model.UnitPrice = Utility.NumberFormatter.GetNullInt(this.UnitPriceTextBox.Text);

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

