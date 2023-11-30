using CultureU.Models;
using CultureU.Models.SchoolViewModels;  // Add VM
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CultureU.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly CultureU.Data.SchoolContext _context;

        public IndexModel(CultureU.Data.SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData InstructorData { get; set; }
        public int ProfesorID { get; set; }
        public int MateriaID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData = new InstructorIndexData();
            InstructorData.Profesores = await _context.Profesores
                .Include(i => i.Directiva)                 
                .Include(i => i.Materias)
                    .ThenInclude(c => c.Escuela)
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                ProfesorID = id.Value;
                Profesor instructor = InstructorData.Profesores
                    .Where(i => i.ID == id.Value).Single();
                InstructorData.Materias = instructor.Materias;
            }

            if (courseID != null)
            {
                MateriaID = courseID.Value;
                IEnumerable<Inscripcion> inscripciones = await _context.Inscripciones
                    .Where(x => x.MateriaID == MateriaID)                    
                    .Include(i=>i.Alumno)
                    .ToListAsync();                 
                InstructorData.Inscripciones = inscripciones;
            }
        }
    }
}