using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Exoft.CollegeCoursePrerequisites.WebApi.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ICollection<T>> TSortGroup<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies, IEqualityComparer<T> comparer = null) where T : class
        {
            var sorted = new List<ICollection<T>>();
            var visited = new Dictionary<T, int>(comparer);

            foreach (var item in source)
                Visit(item, getDependencies, sorted, visited);


            return sorted;
        }

        public static (int, bool) Visit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<ICollection<T>> sorted, Dictionary<T, int> visited)
        {
            const int inProcess = -1;
            var detectCycle = false;
            var alreadyVisited = visited.TryGetValue(item, out int level);

            if (alreadyVisited)
            {
                if (level == inProcess)
                {
                    detectCycle = true;
                }
            }
            else
            {
                visited[item] = (level = inProcess);

                var dependencies = getDependencies(item);
                if (dependencies != null)
                {
                    foreach (var dependency in dependencies)
                    {
                        var visitResult = Visit(dependency, getDependencies, sorted, visited);
                        level = Math.Max(level, visitResult.Item1);
                        detectCycle = visitResult.Item2;
                    }
                }

                visited[item] = ++level;
                if (!detectCycle)
                {
                    while (sorted.Count <= level)
                    {
                        sorted.Add(new Collection<T>());
                    }
                    sorted[level].Add(item);
                }
            }

            return (level, detectCycle);
        }

    }
}
