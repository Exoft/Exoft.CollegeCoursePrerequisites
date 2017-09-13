using Exoft.CollegeCoursePrerequisites.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exoft.CollegeCoursePrerequisites.WebApi.Comparers
{
    public class CourseEqualityComparer : IEqualityComparer<Course>
    {
        public bool Equals(Course x, Course y)
        {
            if (object.ReferenceEquals(x, y)) return true;

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null)) return false;

            return x.Name == y.Name && x.Dependencies?.Count == y.Dependencies?.Count;
        }

        public int GetHashCode(Course obj)
        {
            if (object.ReferenceEquals(obj, null)) return 0;

            int hashCodeName = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int hasCodeAge = obj.Dependencies.Count.GetHashCode();

            return hashCodeName ^ hasCodeAge;
        }
    }
}
