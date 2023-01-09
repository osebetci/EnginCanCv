using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EnginCan.Core.Helpers
{
    public static class ListExtensions
    {
        /// <summary>
        /// Creates the CSV from a generic list.
        /// </summary>;
        /// <typeparam name="T"></typeparam>;
        /// <param name="list">The list.</param>;
        /// <param name="csvNameWithExt">Name of CSV (w/ path) w/ file ext.</param>;
        public static void CreateCSVFromGenericList<T>(this List<T> list, string csvCompletePath, string delimeter = ",")
        {
            if (list == null || list.Count == 0) return;

            //get type from 0th member
            Type t = list[0].GetType();
            string newLine = Environment.NewLine;

            if (!Directory.Exists(Path.GetDirectoryName(csvCompletePath))) Directory.CreateDirectory(Path.GetDirectoryName(csvCompletePath));

            if (!File.Exists(csvCompletePath))
            {
                FileStream file = File.Create(csvCompletePath);
                file.Close();
            }

            using (var sw = new StreamWriter(csvCompletePath))
            {
                //make a new instance of the class name we figured out to get its props
                object o = Activator.CreateInstance(t);
                //gets all properties
                PropertyInfo[] props = o.GetType().GetProperties();

                //foreach of the properties in class above, write out properties
                //this is the header row
                sw.Write(string.Join(delimeter, props.Select(d => d.Name).ToArray()) + newLine);

                //this acts as datarow
                foreach (T item in list)
                {
                    //this acts as datacolumn
                    var row = string.Join(delimeter, props.Select(d => item.GetType()
                                                                    .GetProperty(d.Name)
                                                                    .GetValue(item, null)
                                                                    .ToString())
                                                            .ToArray());
                    sw.Write(row + newLine);
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> sourceList, int ListSize)
        {
            while (sourceList.Any())
            {
                yield return sourceList.Take(ListSize);
                sourceList = sourceList.Skip(ListSize);
            }
        }

        public static IQueryable<T> SelectManyRecursive<T>(this IQueryable<T> source, Func<T, ICollection<T>> selector)
        {
            var result = source.SelectMany(selector).AsQueryable();
            if (!result.Any())
            {
                return result;
            }
            return result.Concat(result.SelectManyRecursive(selector));
        }
    }
}