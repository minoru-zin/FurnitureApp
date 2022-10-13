using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FurnitureApp.Utility.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> items)
        {
            if (items == null) { return; }

            foreach (var item in items)
            {
                source.Add(item);
            }
        }
    }
}
