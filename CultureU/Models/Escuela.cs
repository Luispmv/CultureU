using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureU.Models
{
    public class Escuela
    {
        public int EscuelaID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                       ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? ProfesorID { get; set; }

         public Guid ConcurrencyToken { get; set; } = Guid.NewGuid();

        public Profesor Profesor { get; set; }
        public ICollection<Materia> Materias { get; set; }
    }
}