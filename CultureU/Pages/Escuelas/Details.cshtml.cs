using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Escuelas
{
    public class DetailsModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DetailsModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

      public Escuela Escuela { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Escuelas == null)
            {
                return NotFound();
            }

            var escuela = await _context.Escuelas.FirstOrDefaultAsync(m => m.EscuelaID == id);
            if (escuela == null)
            {
                return NotFound();
            }
            else 
            {
                Escuela = escuela;
            }
            return Page();
        }
    }
}
