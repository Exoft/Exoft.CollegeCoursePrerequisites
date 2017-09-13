using System.Collections.Generic;

namespace Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Models
{
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
}
