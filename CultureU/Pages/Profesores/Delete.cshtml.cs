using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Profesores
{
    public class DeleteModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DeleteModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Profesor Profesor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Profesores == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesores.FirstOrDefaultAsync(m => m.ID == id);

            if (profesor == null)
            {
                return NotFound();
            }
            else 
            {
                Profesor = profesor;
            }
            return Page();
        }

       public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profesor instructor = await _context.Profesores
                .Include(i => i.Materias)
                .SingleAsync(i => i.ID == id);

            if (instructor == null)
            {
                return RedirectToPage("./Index");
            }

            var departments = await _context.Escuelas
                .Where(d => d.ProfesorID == id)
                .ToListAsync();
            departments.ForEach(d => d.ProfesorID = null);

            _context.Profesores.Remove(instructor);

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
