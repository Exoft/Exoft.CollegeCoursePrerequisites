using Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Extensions;
using Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Helpers
{
    public static class CourseHelper
    {
        public static Tuple<string, string>[] SplitCourses(string[] courses)
        {
            return courses.Select(x =>
            {
                if (string.IsNullOrEmpty(x))
                    return null;
                var coursesPair = x.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                return coursesPair.Length > 1
                    ? new Tuple<string, string>(coursesPair[0], coursesPair[1])
                    : new Tuple<string, string>(coursesPair[0], String.Empty);
            }).ToArray();
        }

        public static IEnumerable<Tuple<string, string>> CheckForCyclicDependencies(Tuple<string, string>[] parsedStrings)
        {
            var dict = new Dictionary<string, List<string>>();

            foreach (var tuple in parsedStrings)
            {
                if (dict.ContainsKey(tuple.Item1))
                {
                    if (dict[tuple.Item1] == null)
                    {
                        dict[tuple.Item1] = new List<string>() { tuple.Item2 };
                    }
                    else
                    {
                        dict[tuple.Item1].Add(tuple.Item2);
                    }
                }
                else
                {
                    dict.Add(tuple.Item1, new List<string> { tuple.Item2 });
                }
            }

            var cycles = IDictionaryExtensions.FindCyclicDependencies(dict);
            var result = new List<Tuple<string, string>>();
            if (cycles.Count > 0)
            {
                var flatCycles = cycles.SelectMany(s => s).ToList();
                foreach (var str in parsedStrings)
                {
                    if (flatCycles.Contains(str.Item1) && flatCycles.Contains(str.Item2))
                    {
                        continue;
                    }
                    result.Add(str);
                }
                return result;
            }
            else { return parsedStrings; }

        }

        public static IEnumerable<TreeItem<string>> GenerateTree(Tuple<string, string>[] sourceCollection, string parent)
        {
            foreach (var c in sourceCollection.Where(c => c.Item2.Equals(parent)))
            {
                yield return new TreeItem<string>
                {
                    Item = c.Item1,
                    Children = GenerateTree(sourceCollection, c.Item1).ToArray()
                };
            }
        }

        public static string TreeToString(IEnumerable<TreeItem<string>> tree, string resultString = "")
        {
            foreach (var c in tree)
            {
                if (string.IsNullOrEmpty(resultString))
                {
                    resultString = c.Item;
                }
                else
                {
                    resultString += ", " + c.Item;
                }
                resultString = TreeToString(c.Children, resultString);
            }

            return resultString;
        }
    }
}
