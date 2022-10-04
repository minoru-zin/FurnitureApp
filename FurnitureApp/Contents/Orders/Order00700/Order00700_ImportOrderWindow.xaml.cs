using FurnitureApp.Models;
using FurnitureApp.Repository.MaterialInfos;
using FurnitureApp.Repository.Orders;
using FurnitureApp.Repository.PaintCostItemInfos;
using FurnitureApp.Repository.ProductCategoryInfos;
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
            if (this.cd.DialogService.TrySelectDirPath(this, out var dirPath, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
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

            var exMaterialInfos = Utility.XmlReader.ReadXml<List<MaterialInfo>>(Path.Combine(importDirPath, this.cd.ExportMaterialInfoFileName));
            var exProductCategoryInfos = Utility.XmlReader.ReadXml<List<ProductCategoryInfo>>(Path.Combine(importDirPath, this.cd.ExportProductCategoryInfoFileName));
            var exPaintCostItemInfos = Utility.XmlReader.ReadXml<List<PaintCostItemInfo>>(Path.Combine(importDirPath, this.cd.ExportPaintCostItemInfoFileName));

            var eb = new StringBuilder();

            foreach (var e in exMaterialInfos)
            {
                if(!this.cd.MaterialInfos.Any(x => x.Code == e.Code))
                {
                    eb.Append($"素材マスタ,{e.Code},{e.Name}");
                }
            }
            foreach (var e in exProductCategoryInfos)
            {
                if (!this.cd.ProductCategoryInfos.Any(x => x.Code == e.Code))
                {
                    eb.Append($"製品カテゴリマスタ,{e.Code},{e.Name}");
                }
            }
            foreach (var e in exPaintCostItemInfos)
            {
                if (!this.cd.PaintCostItemInfos.Any(x => x.Code == e.Code))
                {
                    eb.Append($"塗装コストマスタ,{e.Code},{e.Name}");
                }
            }

            if (!string.IsNullOrEmpty($"{eb}"))
            {
                this.cd.DialogService.ShowMessage($"下記のマスタが不足しています\r\n{eb}");
                return;
            }

            var orders = new List<Order>();

            foreach (var dirPath in Directory.GetDirectories(importDirPath))
            {
                orders.Add(Utility.XmlReader.ReadXml<Order>(Path.Combine(dirPath, this.cd.ExportOrderFileName)));
            }

            foreach (var order in orders)
            {
                foreach (var product in order.Products)
                {
                    foreach (var pf in product.ProductFiles)
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
