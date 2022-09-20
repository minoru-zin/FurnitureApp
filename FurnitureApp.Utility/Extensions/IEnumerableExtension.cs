using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FurnitureApp.Utility
{
    public static class IEnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunkSize)
        {
            if (chunkSize <= 0)
            {
                throw new ArgumentException("チャンク数は 0 より大きい値");
            }

            while (source.Any())
            {
                yield return source.Take(chunkSize);
                source = source.Skip(chunkSize);
            }
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items, params string[] ignoreProperties)
        {
            var properties = typeof(T).GetProperties().Where(x => ignoreProperties.Contains(x.Name) == false);
            var dataTable = new DataTable();

            foreach (var prop in properties)
            {
                var column = new DataColumn(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                if(Nullable.GetUnderlyingType(prop.PropertyType) != null)
                {
                    column.AllowDBNull = true;
                }
                dataTable.Columns.Add(column);
            }

            foreach (var item in items)
            {
                var row = dataTable.NewRow();

                foreach (var prop in properties)
                {
                    var itemValue = prop.GetValue(item, new object[] { });

                    if(itemValue != DBNull.Value && itemValue != null)
                    {
                        row[prop.Name] = itemValue;
                    }
                    else
                    {
                        row[prop.Name] = DBNull.Value;
                    }
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        public static IEnumerable<(T, int)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((t, i) => (t, i));
        }
    }
}
