using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SQLiteManager.DataAccess
{
    public static class SQLiteHelpers
    {
        public static string GenerateInsertQuery<T>(string tableName, T t) where T : class
        {
            var sb = new StringBuilder($"INSERT INTO {tableName} (");

            var props = GenerateListOfProperties(typeof(T).GetProperties());

            var lastProp = props.Last();
            foreach (var prop in props)
            {

                sb.Append($"{prop}");
                if (!prop.Equals(lastProp)) { sb.Append(", "); }
            }

            sb.Append($"\n) VALUES (\n");
            foreach (var prop in props)
            {

                sb.Append($"@{prop}");
                if (!prop.Equals(lastProp)) { sb.Append(", "); }
            }

            sb.Append("\n)");

            return sb.ToString();
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }
    }
}
