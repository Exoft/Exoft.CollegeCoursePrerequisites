using Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<List<T>> FindCyclicDependencies<T>(this IEnumerable<T> nodes, Func<T, IEnumerable<T>> edges)
        {
            var cycles = new List<List<T>>();
            var visited = new Dictionary<T, VisitState>();
            foreach (var node in nodes)
                DoSearch(node, edges, new List<T>(), visited, cycles);
            return cycles;
        }

        public static List<List<T>> FindCyclicDependencies<T, TValueList>(this IDictionary<T, TValueList> listDictionary)
            where TValueList : class, IEnumerable<T>
        {
            return listDictionary.Keys.FindCyclicDependencies(key =>
                listDictionary.ValueOrDefault(key, null) ?? Enumerable.Empty<T>());
        }

        static void DoSearch<T>(T node, Func<T, IEnumerable<T>> getChildrenFunc, List<T> parents,
            Dictionary<T, VisitState> visited, List<List<T>> cycles)
        {
            var state = visited.ValueOrDefault(node, VisitState.NotVisited);
            if (state == VisitState.Visited)
                return;
            else if (state == VisitState.Visiting)
            {
                // Do not report nodes not included in the cycle.
                cycles.Add(parents.Concat(new[] { node })
                    .SkipWhile(parent => !EqualityComparer<T>.Default.Equals(parent, node)).ToList());
            }
            else
            {
                visited[node] = VisitState.Visiting;
                parents.Add(node);
                foreach (var child in getChildrenFunc(node))
                    DoSearch(child, getChildrenFunc, parents, visited, cycles);
                parents.RemoveAt(parents.Count - 1);
                visited[node] = VisitState.Visited;
            }
        }
    }
}
