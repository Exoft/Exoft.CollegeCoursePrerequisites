using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Helpers;

namespace Exoft.CollegeCoursePrerequisites.AlternativeWebApi.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {

        [HttpGet]
        [ActionName("GetOrderedCourses")]
        public IActionResult Get(string[] courses)
        {
            if (courses == null)
                return BadRequest();
            try
            {
                var splitedCourses = CourseHelper.SplitCourses(courses);
                var validatedCollection = CourseHelper.CheckForCyclicDependencies(splitedCourses).ToArray();
                var treeItems = CourseHelper.GenerateTree(validatedCollection, string.Empty).ToArray();
                var result = CourseHelper.TreeToString(treeItems);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
