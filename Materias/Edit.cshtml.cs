using CultureU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CultureU.Pages.Materias
{
    public class EditModel : DepartmentNamePageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public EditModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materia Materia { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Materia = await _context.Materias
                .Include(c => c.Escuela).FirstOrDefaultAsync(m => m.MateriaID == id);

            if (Materia == null)
            {
                return NotFound();
            }

            // Select current DepartmentID.
            PopulateDepartmentsDropDownList(_context, Materia.EscuelaID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseToUpdate = await _context.Materias.FindAsync(id);

            if (courseToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Materia>(
                 courseToUpdate,
                 "course",   // Prefix for form value.
                   c => c.Credits, c => c.EscuelaID, c => c.Title))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, courseToUpdate.EscuelaID);
            return Page();
        }       
    }
}