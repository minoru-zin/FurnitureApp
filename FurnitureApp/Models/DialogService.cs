using FurnitureApp.Contents.Common.Common00000;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace FurnitureApp.Models
{
    public class DialogService
    {
        public void ShowMessage(string message)
        {
            var window = new Common00000_ShowMessageWindow(message);
            window.ShowDialog();
        }
        public bool ShowComfirmationMessageDialog(string message)
        {
            var window = new Common00000_ShowConfirmationMessageWindow(message);
            window.ShowDialog();
            return window.IsOk;
        }
        /// <summary>
        /// ディレクトリを選択するダイアログ
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="defaultDirPath"></param>
        /// <returns></returns>
        public bool TrySelectDirPath(Window window, out string dirPath, string defaultDirPath)
        {
            dirPath = "";

            var dlg = new CommonOpenFileDialog();

            dlg.IsFolderPicker = true;

            dlg.DefaultDirectory = defaultDirPath;

            var result = dlg.ShowDialog(window);

            if (result == CommonFileDialogResult.Ok)
            {
                dirPath = dlg.FileName;
                return true;
            }

            return false;
        }
        /// <summary>
        /// ファイルを選択するダイアログ
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="defaultDirPath"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool TrySelectFilePath(Window window, out string filePath, string defaultDirPath, CommonFileDialogFilter filter = null)
        {
            filePath = "";

            var dlg = new CommonOpenFileDialog();

            dlg.InitialDirectory = defaultDirPath;

            if (filter != null)
            {
                dlg.Filters.Add(filter);
            }

            var result = dlg.ShowDialog(window);

            if (result == CommonFileDialogResult.Ok)
            {
                filePath = dlg.FileName;
                return true;
            }

            return false;
        }
    }
}