using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELODATOS.ENTIDADES.Alumnos
{
    [Table("Alumnos")]
    public class Alumnos
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Apellidos { get; set; }

        public int IdCurso { get; set; }

        public virtual ICollection<Profesores.Profesores> Profesores { get; set; }
    }
}
