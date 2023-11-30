using CultureU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CultureU.Pages.Escuelas
{
    public class DeleteModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public DeleteModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Escuela Escuela { get; set; }
        public string ConcurrencyErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, bool? concurrencyError)
        {
            Escuela = await _context.Escuelas
                .Include(d => d.Profesor)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EscuelaID == id);

            if (Escuela == null)
            {
                 return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ConcurrencyErrorMessage = "The record you attempted to delete "
                  + "was modified by another user after you selected delete. "
                  + "The delete operation was canceled and the current values in the "
                  + "database have been displayed. If you still want to delete this "
                  + "record, click the Delete button again.";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                if (await _context.Escuelas.AnyAsync(
                    m => m.EscuelaID == id))
                {
                    // Department.ConcurrencyToken value is from when the entity
                    // was fetched. If it doesn't match the DB, a
                    // DbUpdateConcurrencyException exception is thrown.
                    _context.Escuelas.Remove(Escuela);
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToPage("./Delete",
                    new { concurrencyError = true, id = id });
            }
        }
    }
}