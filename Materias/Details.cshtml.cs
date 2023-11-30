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
    public class DetailsModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DetailsModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

      public Materia Materia { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    Materia = await _context.Materias
        .AsNoTracking()
        .Include(c => c.Escuela)
        .FirstOrDefaultAsync(m => m.MateriaID == id);

    if (Materia == null)
    {
        return NotFound();
    }
    return Page();
}
    }
}
