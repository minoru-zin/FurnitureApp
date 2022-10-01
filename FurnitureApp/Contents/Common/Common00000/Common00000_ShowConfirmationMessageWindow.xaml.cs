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

namespace FurnitureApp.Contents.Common.Common00000
{
    /// <summary>
    /// Common00000_ShowConfirmationMessageWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Common00000_ShowConfirmationMessageWindow : Window
    {
        public bool IsOk { get; private set; } = false;
        public Common00000_ShowConfirmationMessageWindow(string message)
        {
            InitializeComponent();
            this.MessageTextBlock.Text = message;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.NoButton.Focus();
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsOk = true;
            this.Close();
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsOk = false;
            this.Close();
        }
    }
}
