using CultureU.Data;
using CultureU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CultureU.Pages.Profesores
{
    public class CreateModel : InstructorCoursesPageModel
    {
        private readonly CultureU.Data.SchoolContext _context;
        private readonly ILogger<InstructorCoursesPageModel> _logger;

        public CreateModel(SchoolContext context,
                          ILogger<InstructorCoursesPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            var instructor = new Profesor
            {
                Materias = new List<Materia>()
            };

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateAssignedCourseData(_context, instructor);
            return Page();
        }

        [BindProperty]
        public Profesor Profesor { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCourses)
        {
            var newProfesor = new Profesor();

            if (selectedCourses.Length > 0)
            {
                newProfesor.Materias = new List<Materia>();
                // Load collection with one DB call.
                _context.Materias.Load();
            }

            // Add selected Courses courses to the new instructor.
            foreach (var course in selectedCourses)
            {
                var foundCourse = await _context.Materias.FindAsync(int.Parse(course));
                if (foundCourse != null)
                {
                    newProfesor.Materias.Add(foundCourse);
                }
                else
                {
                    _logger.LogWarning("Course {course} not found", course);
                }
            }

            try
            {
                if (await TryUpdateModelAsync<Profesor>(
                                newProfesor,
                                "Instructor",
                                i => i.FirstMidName, i => i.LastName,
                                i => i.HireDate, i => i.Directiva))
                {
                    _context.Profesores.Add(newProfesor);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            PopulateAssignedCourseData(_context, newProfesor);
            return Page();
        }
    }
}