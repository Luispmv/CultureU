using CultureU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CultureU.Pages.Profesores
{
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public EditModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Profesor Profesor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profesor = await _context.Profesores
                .Include(i => i.Directiva)
                .Include(i => i.Materias)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Profesor == null)
            {
                return NotFound();
            }
            PopulateAssignedCourseData(_context, Profesor);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructorToUpdate = await _context.Profesores
                .Include(i => i.Directiva)
                .Include(i => i.Materias)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (instructorToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Profesor>(
                instructorToUpdate,
                "Instructor",
                i => i.FirstMidName, i => i.LastName,
                i => i.HireDate, i => i.Directiva))
            {
                if (String.IsNullOrWhiteSpace(
                    instructorToUpdate.Directiva?.Location))
                {
                    instructorToUpdate.Directiva = null;
                }
                UpdateInstructorCourses(selectedCourses, instructorToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateInstructorCourses(selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(_context, instructorToUpdate);
            return Page();
        }

        public void UpdateInstructorCourses(string[] selectedCourses,
                                            Profesor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Materias = new List<Materia>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Materias.Select(c => c.MateriaID));
            foreach (var course in _context.Materias)
            {
                if (selectedCoursesHS.Contains(course.MateriaID.ToString()))
                {
                    if (!instructorCourses.Contains(course.MateriaID))
                    {
                        instructorToUpdate.Materias.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.MateriaID))
                    {
                        var courseToRemove = instructorToUpdate.Materias.Single(
                                                        c => c.MateriaID == course.MateriaID);
                        instructorToUpdate.Materias.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}