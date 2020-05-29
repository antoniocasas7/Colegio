using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELODATOS.ENTIDADES.Profesores
{

    [Table("Profesores")]
    public class Profesores
    {
        [Key]
        [Required(ErrorMessage = " El Id es requerido")]
        public int Id { get; set; }
        [Required(ErrorMessage = " El Nombre es requerido")]
        public string Name { get; set; }

        public string Apellidos { get; set; }

        public virtual ICollection<Alumnos.Alumnos> Alumnos { get; set; }
    }
}
