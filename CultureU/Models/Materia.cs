using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureU.Models
{
    public class Materia
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int MateriaID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int EscuelaID { get; set; }

        public Escuela Escuela { get; set; }
        public ICollection<Inscripcion> Inscripciones { get; set; }
        public ICollection<Profesor> Profesores { get; set; }
    }
}