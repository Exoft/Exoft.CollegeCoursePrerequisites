using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exoft.CollegeCoursePrerequisites.WebApi.Models;
using Exoft.CollegeCoursePrerequisites.WebApi.Helpers;
using Exoft.CollegeCoursePrerequisites.WebApi.Extensions;

namespace Exoft.CollegeCoursePrerequisites.WebApi.Controllers
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
                var sortedCourses = new HashSet<Course>();
                CourseHelper.ParseToCourses(courses).TSortGroup(i => i.Dependencies).ToList().ForEach(x => sortedCourses.UnionWith(x));
                return Ok(sortedCourses);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
