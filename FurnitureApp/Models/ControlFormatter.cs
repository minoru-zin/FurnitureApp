using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace FurnitureApp.Models
{
    public class ControlFormatter
    {
        /// <summary>
        /// 日付を整形する
        /// </summary>
        /// <param name="textBox"></param>
        public void SetDate(TextBox textBox)
        {
            textBox.Text = $"{Utility.DateTimeFormatter.GetDateTime(textBox.Text):d}";
        }
        public void SetDates(TextBox dateFTextBox, TextBox dateTTextBox)
        {
            var fDate = Utility.DateTimeFormatter.GetDateTime(dateFTextBox.Text);
            var tDate = Utility.DateTimeFormatter.GetDateTime(dateTTextBox.Text);

            if (fDate == null && tDate == null)
            {
                dateFTextBox.Text = "";
                dateTTextBox.Text = "";
                return;
            }
            else if (fDate == null)
            {
                dateFTextBox.Text = $"{tDate:d}";
                dateTTextBox.Text = $"{tDate:d}";
                return;
            }
            else if (tDate == null)
            {
                dateFTextBox.Text = $"{fDate:d}";
                dateTTextBox.Text = $"{fDate:d}";
                return;
            }
            else if (fDate > tDate)
            {
                dateFTextBox.Text = $"{tDate:d}";
                dateTTextBox.Text = $"{tDate:d}";
                return;
            }

            dateFTextBox.Text = $"{fDate:d}";
            dateTTextBox.Text = $"{tDate:d}";
        }

        public void SetIntNumberTextBox(TextBox textBox)
        {
            if (int.TryParse(textBox.Text, out var number))
            {
                textBox.Text = $"{number}";
                return;
            }
            textBox.Text = "";
        }
        public void SetDoubleNumberTextBox(TextBox textBox)
        {
            if (double.TryParse(textBox.Text, out var number))
            {
                textBox.Text = $"{number:0.0}";
                return;
            }
            textBox.Text = "";
        }
    }
}
