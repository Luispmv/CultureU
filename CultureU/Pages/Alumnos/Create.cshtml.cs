using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CultureU.Data;
using CultureU.Models;

namespace CultureU.Pages.Alumnos
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
            return Page();
        }

        [BindProperty]
        public Alumno Alumno { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
{
if (!ModelState.IsValid)
            {
                return Page();
            }
 
            _context.Alumnos.Add(Alumno);
            await _context.SaveChangesAsync();
 
            return RedirectToPage("./Index");
    // var emptyStudent = new Alumno();

    // if (await TryUpdateModelAsync<Alumno>(
    //     emptyStudent,
    //     "student",   // Prefix for form value.
    //     s => s.FirstMidName, s => s.LastName, s => s.InscripcionDate))
    // {
    //     _context.Alumnos.Add(emptyStudent);
    //     await _context.SaveChangesAsync();
    //     return RedirectToPage("./Index");
    // }

    // return Page();
}
    }
}
