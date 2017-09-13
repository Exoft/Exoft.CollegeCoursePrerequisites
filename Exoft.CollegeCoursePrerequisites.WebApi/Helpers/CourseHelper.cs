using Exoft.CollegeCoursePrerequisites.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Exoft.CollegeCoursePrerequisites.WebApi.Helpers
{
    public static class CourseHelper
    {
        /// <summary>
        /// Parse an array of course string to an collection of CollegeCoursePrerequisites.WebApi.Models.Course.  
        /// </summary>
        /// <param name="courseStr">An array of course string</param>
        /// <returns>return an IList<CollegeCoursePrerequisites.WebApi.Models.Course>>.</returns>
        public static IList<Course> ParseToCourses(string[] courseStr)
        {
            var result = new List<Course>();
            
            // Before all we add a vertex to the result list. After that we will set edges(dependency). We use two loops for keeping input order 
            var splitedCourses = courseStr.Select(x => x.Split(": ")).Where(x => x.Length == 2);
            result.AddRange(splitedCourses.Select(x => new Course(x[0])));
            foreach (var item in splitedCourses)
            {
                var course = result.SingleOrDefault(x => x.Name == item[0]);
                var dependetCourse = result.SingleOrDefault(x => x.Name == item[1]);
                if (dependetCourse != null)
                    course.Dependencies.Add(dependetCourse);

            }
            
            return result;
        }
    }
}
