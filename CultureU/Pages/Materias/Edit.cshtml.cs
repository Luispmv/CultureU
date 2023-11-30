using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Materias
{
    public class EditModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public EditModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Materia Materia { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Materias == null)
            {
                return NotFound();
            }

            var materia =  await _context.Materias.FirstOrDefaultAsync(m => m.MateriaID == id);
            if (materia == null)
            {
                return NotFound();
            }
            Materia = materia;
           ViewData["EscuelaID"] = new SelectList(_context.Escuelas, "EscuelaID", "EscuelaID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Materia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaExists(Materia.MateriaID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MateriaExists(int id)
        {
          return _context.Materias.Any(e => e.MateriaID == id);
        }
    }
}
