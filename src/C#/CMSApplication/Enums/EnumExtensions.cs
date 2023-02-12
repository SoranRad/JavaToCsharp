using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CMSApplication.Enums
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetEnumValues<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            return Enum.GetValues(input.GetType()).Cast<T>();
        }

        public static IEnumerable<T> GetEnumFlags<T>(this T input) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new NotSupportedException();

            foreach (var value in Enum.GetValues(input.GetType()))
                if ((input as Enum).HasFlag(value as Enum))
                    yield return (T)value;
        }

        public static string ToDisplay(this Enum value, DisplayProperty property = DisplayProperty.Name)
        {

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(property.ToString()).GetValue(attribute, null);
            return propValue.ToString();
        }

        public static string ToDisplayExp<T>(T value, DisplayProperty property)
        {
            var methodInfo =
                typeof(EnumExtensions)
                    .GetMethod(nameof(ToDisplay), BindingFlags.Public | BindingFlags.Static);

            var EnumConst = Expression.Constant(value, typeof(Enum));
            var DisplayItem = Expression.Constant(property, typeof(DisplayProperty));
            var CallInfo = Expression.Call(methodInfo, EnumConst, DisplayItem);

            var Exp = Expression.Lambda<Func<string>>(CallInfo).Compile()();
            return Exp;
        }
        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return
                Enum
                    .GetValues(value.GetType())
                    .Cast<Enum>()
                    .ToDictionary(p => Convert.ToInt32(p), q => ToDisplay(q));
        }

        public static IEnumerable<SelectListItem> ToSelectItems(this Enum value)
        {
            return
                value
                .ToDictionary()
                .Select(x => new SelectListItem()
                {
                    Value = x.Key.ToString(),
                    Text = x.Value
                });
        }



        public static T ToValue<T>(this Enum value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }
    }

    public enum DisplayProperty
    {
        Description,
        GroupName,
        Name,
        Prompt,
        ShortName,
        Order
    }
}
