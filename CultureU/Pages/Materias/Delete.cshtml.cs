using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Materias
{
    public class DeleteModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DeleteModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Materia Materia { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Materias == null)
            {
                return NotFound();
            }

            var materia = await _context.Materias.FirstOrDefaultAsync(m => m.MateriaID == id);

            if (materia == null)
            {
                return NotFound();
            }
            else 
            {
                Materia = materia;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Materias == null)
            {
                return NotFound();
            }
            var materia = await _context.Materias.FindAsync(id);

            if (materia != null)
            {
                Materia = materia;
                _context.Materias.Remove(Materia);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
