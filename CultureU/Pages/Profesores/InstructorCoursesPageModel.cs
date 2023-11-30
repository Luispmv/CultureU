using CultureU.Data;
using CultureU.Models;
using CultureU.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace CultureU.Pages.Profesores
{
    public class InstructorCoursesPageModel : PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList;

        public void PopulateAssignedCourseData(SchoolContext context,
                                               Profesor instructor)
        {
            var allCourses = context.Materias;
            var instructorCourses = new HashSet<int>(
                instructor.Materias.Select(c => c.MateriaID));
            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                AssignedCourseDataList.Add(new AssignedCourseData
                {
                    MateriaID = course.MateriaID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.MateriaID)
                });
            }
        }
    }
}