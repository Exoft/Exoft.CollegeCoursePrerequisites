using Exoft.CollegeCoursePrerequisites.WebApi.Comparers;
using Exoft.CollegeCoursePrerequisites.WebApi.Helpers;
using Exoft.CollegeCoursePrerequisites.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exoft.CollegeCoursePrerequisites.Test.WebApiTests
{
    [TestClass]
    public class CourseHelperTests
    {
        [TestMethod]
        public void ParseToCourses_HasReturnCorrectListOfCourses_True()
        {
            var disorderedStringOfCourses = new[] { "Introduction to Paper Airplanes: ", "Advanced Throwing Techniques: Introduction to Paper Airplanes", "History of Cubicle Siege Engines: Rubber Band Catapults 101", "Advanced Office Warfare: History of Cubicle Siege Engines", "Rubber Band Catapults 101: ", "Paper Jet Engines: Introduction to Paper Airplanes" };
            var courses = new List<Course>();
            var course1 = new Course("Introduction to Paper Airplanes");
            var course2 = new Course("Advanced Throwing Techniques");
            var course3 = new Course("History of Cubicle Siege Engines");
            var course4 = new Course("Advanced Office Warfare");
            var course5 = new Course("Rubber Band Catapults 101");
            var course6 = new Course("Paper Jet Engines");
            course2.Dependencies.Add(course1);
            course3.Dependencies.Add(course5);
            course4.Dependencies.Add(course3);
            course6.Dependencies.Add(course1);
            courses.Add(course1);
            courses.Add(course2);
            courses.Add(course3);
            courses.Add(course4);
            courses.Add(course5);
            courses.Add(course6);

            var outputCourses = CourseHelper.ParseToCourses(disorderedStringOfCourses).ToList();

            CollectionAssert.AllItemsAreNotNull(outputCourses);
            Assert.IsTrue(courses.SequenceEqual(outputCourses, new CourseEqualityComparer()));
            
        }
    }
}
