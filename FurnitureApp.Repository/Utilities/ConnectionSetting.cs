using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Repository.Utilities
{
    public class ConnectionSetting
    {
        /// <summary>
        /// 接続文字列
        /// </summary>
        public string ConnectionString { get; set; } = @"Resources\Furniture.sqlite3";
    }
}
