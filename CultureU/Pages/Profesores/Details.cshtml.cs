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
    public class DetailsModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DetailsModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

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
    }
}
