using FurnitureApp.Models;
using FurnitureApp.Repository.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FurnitureApp.Contents.Orders.Order00500
{
    /// <summary>
    /// Order00500_EditProductFileWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Order00500_EditProductFileWindow : Window
    {
        private NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonData cd = CommonData.GetInstance();

        public ProductFileEx ProductFile { get; private set; }
        public bool IsChanged { get; private set; } = false;
        
        public Order00500_EditProductFileWindow(ProductFileEx productFile)
        {
            InitializeComponent();

            this.ProductFile = productFile.Clone();
            this.SetModelToControls();
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
            this.DisplayNameTextBox.Focus();
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as System.Windows.Controls.TextBox;

            if (textBox == null) { return; }

            textBox.SelectAll();
        }
        private void SetModelToControls()
        {
            this.DisplayNameTextBox.Text = this.ProductFile.DisplayName;
            this.SourceFilePathTextBox.Text = this.ProductFile.SourceFilePath;

            if(this.ProductFile.Id == null)
            {
                if (string.IsNullOrEmpty(this.ProductFile.FileName))
                {
                    this.DeleteButton.IsEnabled = false;
                }
            }
            else
            {
                this.SourceFilePathStackPanel.IsEnabled = false;
            }
        }
        private void SelctFileButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.cd.DialogService.TrySelectFilePath(this, out string filePath, Environment.GetFolderPath(Environment.SpecialFolder.Desktop)))
            {
                this.SourceFilePathTextBox.Text = filePath;
                if (string.IsNullOrEmpty(this.DisplayNameTextBox.Text))
                {
                    this.DisplayNameTextBox.Text = Path.GetFileNameWithoutExtension(filePath);
                }
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
                logger.Error(ex);
                this.cd.DialogService.ShowMessage(ex.Message);
            }
        }

        private void Update()
        {
            if (string.IsNullOrEmpty(this.DisplayNameTextBox.Text))
            {
                this.cd.DialogService.ShowMessage("名称が空");
                return;
            }

            this.ProductFile.DisplayName = this.DisplayNameTextBox.Text;
            this.ProductFile.SourceFilePath = this.SourceFilePathTextBox.Text;

            if(this.ProductFile.Id == null)
            {
                if (string.IsNullOrEmpty(this.ProductFile.SourceFilePath))
                {
                    this.cd.DialogService.ShowMessage("コピー元が空");
                    return;
                }

                this.ProductFile.FileName = $"{Guid.NewGuid()}{Path.GetExtension(this.ProductFile.SourceFilePath)}";
            }
            
            this.ProductFile.IsDeleted = false;
            this.IsChanged = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            this.ProductFile.IsDeleted = true;
            this.IsChanged = true;
            this.Close();
        }
    }
}
