using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace FurnitureApp.Utility
{
    public class Reflector
    {
        /// <summary>
        /// オブジェクトのプロパティの値を取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(T instance, string propertyName)
        {
            try
            {
                // プロパティ情報の取得
                var property = typeof(T).GetProperty(propertyName);
                if (property != null)
                {
                    // インスタンスの値を取得
                    return property.GetValue(instance);
                }
                return null;
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// オブジェクトのプロパティに値をセット
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(T instance, string propertyName, object value)
        {
            try
            {
                // プロパティ情報の取得
                var property = typeof(T).GetProperty(propertyName);
                if (property != null)
                {
                    Type t = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    object safeValue = (value == null) ? null : Convert.ChangeType(value, t);

                    property.SetValue(instance, safeValue, null);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static bool IsSame<T>(T t1, T t2, HashSet<string> ignorePropertyNames = null)
        {
            ignorePropertyNames ??= new HashSet<string>();

            foreach (var pn in typeof(T).GetProperties().Select(x => x.Name).Where(x => !ignorePropertyNames.Contains(x)))
            {
                if (!IsSame(t1, t2, pn)) { return false; }
            }
            return true;
        }
        private static bool IsSame<T>(T t1, T t2, string propertyName)
        {
            return $"{GetPropertyValue(t1, propertyName)}" == $"{GetPropertyValue(t2, propertyName)}";
        }
    }
}
