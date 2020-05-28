using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiUsers.Models.EntidadesLogin
{
    public class UsuarioInfo
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
    }
}