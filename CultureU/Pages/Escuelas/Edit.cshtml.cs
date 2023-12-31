using CultureU.Data;
using CultureU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;                // For GUID in SQLite version
using System.Linq;
using System.Threading.Tasks;

namespace CultureU.Pages.Escuelas
{
    public class EditModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public EditModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Escuela Escuela { get; set; }
        // Replace ViewData["InstructorID"] 
        public SelectList InstructorNameSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Escuela = await _context.Escuelas
                .Include(d => d.Profesor)  // eager loading
                .AsNoTracking()                 // tracking not required
                .FirstOrDefaultAsync(m => m.EscuelaID == id);

            if (Escuela == null)
            {
                return NotFound();
            }

            // Use strongly typed data rather than ViewData.
            InstructorNameSL = new SelectList(_context.Escuelas,
                "ID", "FirstMidName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Fetch current department from DB.
            // ConcurrencyToken may have changed.
            var departmentToUpdate = await _context.Escuelas
                .Include(i => i.Profesor)
                .FirstOrDefaultAsync(m => m.EscuelaID == id);

            if (departmentToUpdate == null)
            {
                return HandleDeletedDepartment();
            }

            departmentToUpdate.ConcurrencyToken = Guid.NewGuid();

            // Set ConcurrencyToken to value read in OnGetAsync
            _context.Entry(departmentToUpdate).Property(d => d.ConcurrencyToken)
                                   .OriginalValue = Escuela.ConcurrencyToken;

            if (await TryUpdateModelAsync<Escuela>(
                departmentToUpdate,
                "Department",
                s => s.Name, s => s.StartDate, s => s.Budget, s => s.ProfesorID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Escuela)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save. " +
                            "The department was deleted by another user.");
                        return Page();
                    }

                    var dbValues = (Escuela)databaseEntry.ToObject();
                    await SetDbErrorMessage(dbValues, clientValues, _context);

                    // Save the current ConcurrencyToken so next postback
                    // matches unless an new concurrency issue happens.
                    Escuela.ConcurrencyToken = dbValues.ConcurrencyToken;
                    // Clear the model error for the next postback.
                    ModelState.Remove($"{nameof(Escuela)}.{nameof(Escuela.ConcurrencyToken)}");
                }
            }

            InstructorNameSL = new SelectList(_context.Profesores,
                "ID", "FullName", departmentToUpdate.ProfesorID);

            return Page();
        }

        private IActionResult HandleDeletedDepartment()
        {
            // ModelState contains the posted data because of the deletion error
            // and overides the Department instance values when displaying Page().
            ModelState.AddModelError(string.Empty,
                "Unable to save. The department was deleted by another user.");
            InstructorNameSL = new SelectList(_context.Profesores, "ID", "FullName", Escuela.ProfesorID);
            return Page();
        }

        private async Task SetDbErrorMessage(Escuela dbValues,
                Escuela clientValues, SchoolContext context)
        {

            if (dbValues.Name != clientValues.Name)
            {
                ModelState.AddModelError("Department.Name",
                    $"Current value: {dbValues.Name}");
            }
            if (dbValues.Budget != clientValues.Budget)
            {
                ModelState.AddModelError("Department.Budget",
                    $"Current value: {dbValues.Budget:c}");
            }
            if (dbValues.StartDate != clientValues.StartDate)
            {
                ModelState.AddModelError("Department.StartDate",
                    $"Current value: {dbValues.StartDate:d}");
            }
            if (dbValues.ProfesorID != clientValues.ProfesorID)
            {
                Profesor dbInstructor = await _context.Profesores
                   .FindAsync(dbValues.ProfesorID);
                ModelState.AddModelError("Department.InstructorID",
                    $"Current value: {dbInstructor?.FullName}");
            }

            ModelState.AddModelError(string.Empty,
                "The record you attempted to edit "
              + "was modified by another user after you. The "
              + "edit operation was canceled and the current values in the database "
              + "have been displayed. If you still want to edit this record, click "
              + "the Save button again.");
        }
    }
}