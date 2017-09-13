using Exoft.CollegeCoursePrerequisites.WebApi.Extensions;
using Exoft.CollegeCoursePrerequisites.WebApi.Helpers;
using Exoft.CollegeCoursePrerequisites.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Exoft.CollegeCoursePrerequisites.Test.WebApiTests
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestMethod]
        public void TSortGroup_IsValidOrderOfCourses_True()
        {
            var disorderedStringOfCourses = new[] { "Introduction to Paper Airplanes: ", "Advanced Throwing Techniques: Introduction to Paper Airplanes", "History of Cubicle Siege Engines: Rubber Band Catapults 101", "Advanced Office Warfare: History of Cubicle Siege Engines", "Rubber Band Catapults 101: ", "Paper Jet Engines: Introduction to Paper Airplanes" };
            var expectStringOfCourses = new[] { "Introduction to Paper Airplanes, Rubber Band Catapults 101, Paper Jet Engines, Advanced Throwing Techniques, History of Cubicle Siege Engines, Advanced Office Warfare", "Introduction to Paper Airplanes, Rubber Band Catapults 101, Advanced Throwing Techniques, History of Cubicle Siege Engines, Paper Jet Engines, Advanced Office Warfare", "Introduction to Paper Airplanes, Rubber Band Catapults 101, History of Cubicle Siege Engines, Advanced Throwing Techniques, Paper Jet Engines, Advanced Office Warfare" };

            var courses = new HashSet<Course>();
            CourseHelper.ParseToCourses(disorderedStringOfCourses).TSortGroup(i => i.Dependencies).ToList().ForEach(x => courses.UnionWith(x));
            var result = string.Join(", ", courses.Select(x => x.Name));

            Assert.AreEqual(expectStringOfCourses[0].Length, result.Length);
            CollectionAssert.Contains(expectStringOfCourses, result);

        }

        [TestMethod]
        public void TSortGroup_IsResultWithoutCyclicDependency_True()
        {
            var disorderedStringOfCourses = new[] { "Introduction to Paper Airplanes: ", "Advanced Throwing Techniques: Introduction to Paper Airplanes", "History of Cubicle Siege Engines: Rubber Band Catapults 101", "Advanced Office Warfare: History of Cubicle Siege Engines", "Rubber Band Catapults 101: ", "Paper Jet Engines: Introduction to Paper Airplanes", "Intro to Arguing on the Internet: Godwin’s Law", "Understanding Circular Logic: Intro to Arguing on the Internet", "Godwin’s Law: Understanding Circular Logic" };
            var expectStringOfCourses = new[] { "Introduction to Paper Airplanes, Rubber Band Catapults 101, Paper Jet Engines, Advanced Throwing Techniques, History of Cubicle Siege Engines, Advanced Office Warfare", "Introduction to Paper Airplanes, Rubber Band Catapults 101, Advanced Throwing Techniques, History of Cubicle Siege Engines, Paper Jet Engines, Advanced Office Warfare", "Introduction to Paper Airplanes, Rubber Band Catapults 101, History of Cubicle Siege Engines, Advanced Throwing Techniques, Paper Jet Engines, Advanced Office Warfare" };

            var courses = new HashSet<Course>();
            CourseHelper.ParseToCourses(disorderedStringOfCourses).TSortGroup(i => i.Dependencies).ToList().ForEach(x => courses.UnionWith(x));
            var result = string.Join(", ", courses.Select(x => x.Name));

            Assert.AreEqual(expectStringOfCourses[0].Length, result.Length);
            CollectionAssert.Contains(expectStringOfCourses, result);
        }
    }
}
