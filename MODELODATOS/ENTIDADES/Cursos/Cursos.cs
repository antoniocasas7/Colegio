using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELODATOS.ENTIDADES.Cursos
{
    [Table("Cursos")]
    public class Cursos
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Alumnos.Alumnos> Alumnos { get; set; }

    }
}
