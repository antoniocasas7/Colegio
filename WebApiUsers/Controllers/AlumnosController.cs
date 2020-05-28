using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MODELODATOS.ENTIDADES.Alumnos;
using WebApiUsers.Models;

namespace WebApiUsers.Controllers
{
    /// <summary>
    /// Controlador para gestionar loa Alumnos
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Alumnos")]
    public class AlumnosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Devuelve todos los Alumnos
        /// </summary>     
        /// <returns>Lista de Alumnos</returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Route("GetAlumnos")]
        [ResponseType(typeof(IQueryable<Alumnos>))]
        [HttpGet]
        // GET: api/Alumnos
        public IQueryable<Alumnos> GetAlumnos()
        {
            return db.Alumnos;
        }


        /// <summary>
        /// Devuelve un Alumno segun el Id pasado como parametro 
        /// </summary>
        /// <param name="id"> Id del Alumno a buscar</param> <seealso cref="int"></seealso>
        /// <returns>Empleo bus</returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [Route("GetAlumno")]
        [ResponseType(typeof(Alumnos))]
        // GET: api/Alumnos/5
        [ResponseType(typeof(Alumnos))]
        public async Task<IHttpActionResult> GetAlumno([FromUri] int id)
        {
            Alumnos alumnos = await db.Alumnos.FindAsync(id);
            if (alumnos == null)
            {
                return NotFound();
            }

            return Ok(alumnos);
        }


        /// <summary>
        /// Edita un Alumno segun el Id pasado como parametro y el Alumno
        /// </summary>       
        /// <param name="id"> Id del eAlumno a editar</param> <seealso cref="int"></seealso>
        /// <param name="alumnos"> Datos actualizados del Alumno a editar</param>  <seealso cref="Alumnos"></seealso>
        /// <returns></returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
  
        [Route("EditAlumno")]
        [ResponseType(typeof(IHttpActionResult))]
        // PUT: api/Alumnos/5
        public async Task<IHttpActionResult> EditAlumno([FromUri] int id, [FromBody]Alumnos alumnos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alumnos.Id)
            {
                return BadRequest();
            }

            db.Entry(alumnos).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        /// <summary>
        /// Crea un Alumno
        /// </summary>       
        /// <param name="alumno"> Datos del Alumno a crear</param>  <seealso cref="Alumnos"></seealso>
        /// <returns></returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>

        [HttpPost]
        [Route("AddAlumno")]
        [ResponseType(typeof(Alumnos))]
        // POST: api/Alumnos
        public async Task<IHttpActionResult> PostAlumnos([FromBody] Alumnos alumnos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Alumnos.Add(alumnos);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = alumnos.Id }, alumnos);
        }


        /// <summary>
        /// Elimina un Alumno
        /// </summary>       
        /// <param name="id"> Id del Alumno a eliminar</param>  
        /// <returns></returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        // DELETE: api/Empleos/5
        [HttpDelete]
        [Route("DeleteAlumno")]
        // DELETE: api/Alumnos/5
        [ResponseType(typeof(Alumnos))]
        public async Task<IHttpActionResult> DeleteAlumnos([FromUri] int id)
        {
            Alumnos alumnos = await db.Alumnos.FindAsync(id);
            if (alumnos == null)
            {
                return NotFound();
            }

            db.Alumnos.Remove(alumnos);
            await db.SaveChangesAsync();

            return Ok(alumnos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlumnosExists(int id)
        {
            return db.Alumnos.Count(e => e.Id == id) > 0;
        }
    }
}