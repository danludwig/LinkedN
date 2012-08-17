using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace LinkedN
{
    /// <summary>
    /// This type is responsible for bucketing all the other code that really doesn't belong anywhere else yet.
    /// </summary>
    public static class LinkedNExtensions
    {
        internal static TAttribute GetAttribute<TAttribute>(this Enum enumeration)
            where TAttribute : Attribute
        {
            var attribute = enumeration.GetType()
                .GetMember(enumeration.ToString())[0]
                .GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>().SingleOrDefault();

            return attribute;
        }

        public static string ActualName(this PersonField personField)
        {
            return personField.GetAttributeValue<DataMemberAttribute, string>(a => a.Name);
        }

        internal static TExpected GetAttributeValue<TAttribute, TExpected>(this Enum enumeration, Func<TAttribute, TExpected> expression)
            where TAttribute : Attribute
        {
            var attribute = enumeration.GetAttribute<TAttribute>();
            return attribute == null ? default(TExpected) : expression(attribute);
        }

        internal static string Implode(this IEnumerable<string> strings, string separator = ",")
        {
            var builder = new StringBuilder();
            foreach (var s in strings)
            {
                if (builder.Length > 0) builder.Append(separator);
                builder.Append(s);
            }
            return builder.ToString();
        }

        internal static IEnumerable<string> Explode(this string dynamite, string separator = ",")
        {
            return dynamite.Split(new[] { separator }, StringSplitOptions.None);
        }
    }
}