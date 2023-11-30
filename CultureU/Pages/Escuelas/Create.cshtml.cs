using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Escuelas
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
        ViewData["ProfesorID"] = new SelectList(_context.Profesores, "ID", "FirstMidName");
            return Page();
        }

        [BindProperty]
        public Escuela Escuela { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Escuelas.Add(Escuela);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
