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
    public class IndexModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public IndexModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<Materia> Materia { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Materias != null)
            {
                Materia = await _context.Materias
                .Include(m => m.Escuela).ToListAsync();
            }
        }
    }
}
