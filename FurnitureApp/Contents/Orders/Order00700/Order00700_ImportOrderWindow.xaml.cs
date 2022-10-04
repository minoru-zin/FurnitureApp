using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FurnitureApp.Contents.Orders.Order00700
{
    /// <summary>
    /// Order00800_ImportOrderWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00700_ImportOrderWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();
        public bool IsChanged { get; private set; } = false;
        public Order00700_ImportOrderWindow()
        {
            InitializeComponent();
        }

        private void SelectDirPathButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.cd.DialogService.TrySelectDirPath(this, out var dirPath, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
            {
                this.ImportDirPathTextBox.Text = dirPath;
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Import();
            }
            catch (Exception ex)
            {
                this.logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }
        private void Import()
        {
            var importDirPath = this.ImportDirPathTextBox.Text;

            if (!Directory.Exists(importDirPath))
            {
                this.cd.DialogService.ShowMessage("フォルダが存在しないかアクセスできません");
                return;
            }

            var orders = new List<Order>();

            foreach(var dirPath in Directory.GetDirectories(importDirPath))
            {
                orders.Add(Utility.XmlReader.ReadXml<Order>(Path.Combine(dirPath, this.cd.ExportOrderFileName)));
            }

            foreach(var order in orders)
            {
                foreach(var product in order.Products)
                {
                    foreach(var pf in product.ProductFiles)
                    {
                        pf.SourceFilePath = Path.Combine(importDirPath, $"{order.Id}", pf.FileName);
                    }
                }
            }

            this.cd.OrderRepository.Insert(orders.OrderBy(x => x.Id));

            this.cd.DialogService.ShowMessage("取り込み完了");

            this.IsChanged = true;
            this.Close();
        }
    }
}
