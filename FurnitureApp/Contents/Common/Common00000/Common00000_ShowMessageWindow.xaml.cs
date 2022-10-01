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
    /// Common00000_ShowMessageWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class Common00000_ShowMessageWindow : Window
    {
        public Common00000_ShowMessageWindow(string message)
        {
            InitializeComponent();
            this.MessageTextBlock.Text = message;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.OkButton.Focus();
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
