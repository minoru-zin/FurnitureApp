using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureApp.Models
{
    public class DisplayInfo<T>
    {
        public T Code { get; }
        public string DisplayName { get; }

        public DisplayInfo(T code, string displayName)
        {
            this.Code = code;
            this.DisplayName = displayName;
        }
    }
}
