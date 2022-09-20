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
            foreach (var item in items)
            {
                source.Add(item);
            }
        }
        public static void Update<T>(this ObservableCollection<T> source)
        {
            var temps = source.ToList();
            source.Clear();
            source.AddRange(temps);
        }
    }
}
