using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Materias
{
    public class CreateModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public CreateModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EscuelaID"] = new SelectList(_context.Escuelas, "EscuelaID", "EscuelaID");
            return Page();
        }

        [BindProperty]
        public Materia Materia { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Materias.Add(Materia);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
