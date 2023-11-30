using CultureU.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CultureU.Pages.Materias
{
    public class CreateModel : DepartmentNamePageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public CreateModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDepartmentsDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Materia Materia { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {   
            var emptyMateria = new Materia();
 
            if (await TryUpdateModelAsync<Materia>(
                 emptyMateria,
                 "course",   // Prefix for form value.
                 s => s.MateriaID, s => s.EscuelaID, s => s.Title, s => s.Credits))
            {
                _context.Materias.Add(emptyMateria);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
 
            // Select DepartmentID if TryUpdateModelAsync fails.
            PopulateDepartmentsDropDownList(_context, emptyMateria.EscuelaID);
            return Page();
            // var emptyCourse = new Materia();

            // if (await TryUpdateModelAsync<Materia>(
            //      emptyCourse,
            //      "course",   // Prefix for form value.
            //      s => s.MateriaID, s => s.EscuelaID, s => s.Title, s => s.Credits))
            // {
            //     _context.Materias.Add(emptyCourse);
            //     await _context.SaveChangesAsync();
            //     return RedirectToPage("./Index");
            // }

            // // Select DepartmentID if TryUpdateModelAsync fails.
            // PopulateDepartmentsDropDownList(_context, emptyCourse.EscuelaID);
            // return Page();
        }
      }
} 