using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureU.Models
{
    public class Directiva
    {
        [Key]
        public int ProfesorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Profesor Profesor { get; set; }
    }
}