using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CultureU.Models.SchoolViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Profesor> Profesores { get; set; }
        public IEnumerable<Materia> Materias { get; set; }
        public IEnumerable<Inscripcion> Inscripciones { get; set; }
    }
}