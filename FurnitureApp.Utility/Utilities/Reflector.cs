using System;
using System.Collections.Generic;
using System.Text;

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
        /// <summary>
        /// ReactivePropertyのValueの値をセットする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetReactivePropertyValue<T>(T instance, string propertyName, object value)
        {
            try
            {
                var v1 = typeof(T).GetProperty(propertyName).GetValue(instance, null);
                v1.GetType().GetProperty("Value").SetValue(v1, value);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ReactivePropertyのValueの値を取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static object GetReactivePropertyValue<T>(T instance, string propertyName)
        {
            try
            {
                var v1 = typeof(T).GetProperty(propertyName).GetValue(instance, null);
                return v1.GetType().GetProperty("Value").GetValue(v1);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
