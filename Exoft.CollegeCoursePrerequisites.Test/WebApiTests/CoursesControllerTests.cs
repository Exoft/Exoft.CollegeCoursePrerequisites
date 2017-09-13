using Exoft.CollegeCoursePrerequisites.WebApi.Comparers;
using Exoft.CollegeCoursePrerequisites.WebApi.Controllers;
using Exoft.CollegeCoursePrerequisites.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Exoft.CollegeCoursePrerequisites.Test.WebApiTests
{
    [TestClass]
    public class CoursesControllerTests
    {
        [TestMethod]
        public void Get_CheckReturnCollection_True()
        {
            var courseController = new CoursesController();
            // The array without cyclinc dependency
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
            courses.Add(course5);
            courses.Add(course2);
            courses.Add(course3);
            courses.Add(course6);
            courses.Add(course4);


            var resultObjectValue = ((OkObjectResult)courseController.Get(disorderedStringOfCourses)).Value;
            var result = ((HashSet<Course>)resultObjectValue).ToList();

            CollectionAssert.AllItemsAreNotNull(result);
            Assert.IsTrue(courses.SequenceEqual(result, new CourseEqualityComparer()));
        }

        [TestMethod]
        public void Get_HasReturnedCollectionWioutCyclicDependency_True()
        {
            var courseController = new CoursesController();
            // The array with cyclinc dependency
            var disorderedStringOfCourses = new[] { "Introduction to Paper Airplanes: ", "Advanced Throwing Techniques: Introduction to Paper Airplanes", "History of Cubicle Siege Engines: Rubber Band Catapults 101", "Advanced Office Warfare: History of Cubicle Siege Engines", "Rubber Band Catapults 101: ", "Paper Jet Engines: Introduction to Paper Airplanes", "Intro to Arguing on the Internet: Godwin’s Law", "Understanding Circular Logic: Intro to Arguing on the Internet", "Godwin’s Law: Understanding Circular Logic" };
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
            courses.Add(course5);
            courses.Add(course2);
            courses.Add(course3);
            courses.Add(course6);
            courses.Add(course4);


            var resultObjectValue = ((OkObjectResult)courseController.Get(disorderedStringOfCourses)).Value;
            var result = ((HashSet<Course>)resultObjectValue).ToList();

            CollectionAssert.AllItemsAreNotNull(result);
            Assert.IsTrue(courses.SequenceEqual(result, new CourseEqualityComparer()));
        }
    }
}
