using Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exoft.CollegeCoursePrerequisites.Test.AlternativeWebApiTests
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

            var orderedStringOfCourses =  new[] { "Introduction to Paper Airplanes, Rubber Band Catapults 101, Paper Jet Engines, Advanced Throwing Techniques, History of Cubicle Siege Engines, Advanced Office Warfare", "Introduction to Paper Airplanes, Advanced Throwing Techniques, Paper Jet Engines, Rubber Band Catapults 101, History of Cubicle Siege Engines, Advanced Office Warfare" };

            var resultObjectValue = ((OkObjectResult)courseController.Get(disorderedStringOfCourses)).Value.ToString();

            CollectionAssert.Contains(orderedStringOfCourses, resultObjectValue);
        }

        [TestMethod]
        public void Get_HasReturnedCollectionWioutCyclicDependency_True()
        {
            var courseController = new CoursesController();
            // The array with cyclinc dependency
            var disorderedStringOfCourses = new[] { "Introduction to Paper Airplanes: ", "Advanced Throwing Techniques: Introduction to Paper Airplanes", "History of Cubicle Siege Engines: Rubber Band Catapults 101", "Advanced Office Warfare: History of Cubicle Siege Engines", "Rubber Band Catapults 101: ", "Paper Jet Engines: Introduction to Paper Airplanes", "Intro to Arguing on the Internet: Godwin’s Law", "Understanding Circular Logic: Intro to Arguing on the Internet", "Godwin’s Law: Understanding Circular Logic" };

            var orderedStringOfCourses = new[] { "Introduction to Paper Airplanes, Rubber Band Catapults 101, Paper Jet Engines, Advanced Throwing Techniques, History of Cubicle Siege Engines, Advanced Office Warfare", "Introduction to Paper Airplanes, Advanced Throwing Techniques, Paper Jet Engines, Rubber Band Catapults 101, History of Cubicle Siege Engines, Advanced Office Warfare" };

            var resultObjectValue = ((OkObjectResult)courseController.Get(disorderedStringOfCourses)).Value.ToString();

            CollectionAssert.Contains(orderedStringOfCourses, resultObjectValue);
        }
    }
}
