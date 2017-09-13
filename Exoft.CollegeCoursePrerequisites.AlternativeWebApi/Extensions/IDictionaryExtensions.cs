using System.Collections.Generic;
using System.Linq;

namespace Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Extensions
{
    public static class IDictionaryExtensions
    {
        public static List<List<T>> FindCyclicDependencies<T, TValueList>(this IDictionary<T, TValueList> listDictionary)
            where TValueList : class, IEnumerable<T>
        {
            return listDictionary.Keys.FindCyclicDependencies(key =>
                listDictionary.ValueOrDefault(key, null) ?? Enumerable.Empty<T>());
        }

        public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
         TValue defaultValue)
        {
            TValue value;
            if (dictionary.TryGetValue(key, out value))
                return value;
            return defaultValue;
        }
    }
}
