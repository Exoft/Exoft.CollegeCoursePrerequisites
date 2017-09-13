using System;
using System.Collections.Generic;
using System.Text;

namespace Exoft.CollegeCoursePrerequisites.WebApi.Models
{
    public class Course
    {
        public Course(string name)
        {
            Name = name;
            Dependencies = new List<Course>();
        }
        public string Name { get; set; }

        public IList<Course> Dependencies { get; set; }
    }
}
