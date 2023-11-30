using System.ComponentModel.DataAnnotations;

namespace CultureU.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Inscripcion
    {
        public int InscripcionID { get; set; }
        public int MateriaID { get; set; }
        public int AlumnoID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Materia Materia { get; set; }
        public Alumno Alumno { get; set; }
    }
}