﻿using FurnitureApp.Models;
using FurnitureApp.Repository.ProductCategoryInfos;
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

namespace FurnitureApp.Contents.Masters.Master00300
{
    /// <summary>
    /// Master00300_EditProductCategoryInfoWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Master00300_EditProductCategoryInfoWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        private ProductCategoryInfo model;
        public bool IsChanged = false;
        public Master00300_EditProductCategoryInfoWindow(ProductCategoryInfo m)
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
            if (!this.cd.DialogService.ShowComfirmationMessageDialog("本当に削除しますか？")) { return; }

            this.cd.ProductCategoryInfoRepository.Delete(this.model);

            this.IsChanged = true;
            this.Close();
        }
    }
}