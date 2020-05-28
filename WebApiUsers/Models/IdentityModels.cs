using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using MODELODATOS.ENTIDADES.Alumnos;
using MODELODATOS.ENTIDADES.Cursos;
using MODELODATOS.ENTIDADES.Profesores;

namespace WebApiUsers.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("entidadColegio", throwIfV1Schema: false)
        {
            base.Configuration.LazyLoadingEnabled = false;
        }

     
            public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Alumnos> Alumnos { get; set; }

        public virtual DbSet<Cursos> Cursos { get; set; }

        public virtual DbSet<Profesores> Profesores { get; set; }

    }
}