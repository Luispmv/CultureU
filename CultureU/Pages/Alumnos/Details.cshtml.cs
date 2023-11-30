using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Alumnos
{
    public class DetailsModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DetailsModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

      public Alumno Alumno { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Alumno == null)
            {
                return NotFound();
            }

            Alumno = await _context.Alumnos
        .Include(s => s.Inscripciones)
        .ThenInclude(e => e.Materia)
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.ID == id);

            if (Alumno == null)
            {
                return NotFound();
            }
            else 
            {
                Alumno = Alumno;
            }
            return Page();
        }
    }
}
