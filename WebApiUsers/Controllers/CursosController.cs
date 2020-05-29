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
using MODELODATOS.ENTIDADES.Cursos;
using WebApiUsers.Models;

namespace WebApiUsers.Controllers
{
    /// <summary>
    /// Controlador para gestionar los Cursos
    /// </summary>
    [Authorize]
    [RoutePrefix("api/Cursos")]
    public class CursosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Devuelve todos los Cursos
        /// </summary>     
        /// <returns>Lista de Cursos</returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Route("GetCursos")]
        [ResponseType(typeof(IQueryable<Cursos>))]
        [HttpGet]
        public IQueryable<Cursos> GetCursos()
        {
            return db.Cursos;
        }

        /// <summary>
        /// Devuelve un Curso segun el Id pasado como parametro 
        /// </summary>
        /// <param name="id"> Id del Curso a buscar</param> <seealso cref="int"></seealso>
        /// <returns>Curso</returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [Route("GetCurso")]
        [ResponseType(typeof(Cursos))]
        public async Task<IHttpActionResult> GetCurso([FromUri] int id)
        {
            Cursos cursos = await db.Cursos.FindAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }

            return Ok(cursos);
        }

        /// <summary>
        /// Edita un Curso segun el Id pasado como parametro y el Curso 
        /// </summary>       
        /// <param name="id"> Id del Curso a editar</param> <seealso cref="int"></seealso>
        /// <param name="curso"> Datos actualizados del Curso a editar</param>  <seealso cref="Cursos"></seealso>
        /// <returns></returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [Route("EditCurso")]
        [ResponseType(typeof(IHttpActionResult))]
        public async Task<IHttpActionResult> EditCurso([FromUri] int id, [FromBody] Cursos curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != curso.Id)
            {
                return BadRequest();
            }

            db.Entry(curso).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursosExists(id))
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
        /// Crea un Curso
        /// </summary>       
        /// <param name="curso"> Datos del Curso a crear</param>  <seealso cref="Cursos"></seealso>
        /// <returns></returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpPost]
        [Route("AddCurso")]
        [ResponseType(typeof(Cursos))]
        public async Task<IHttpActionResult> AddCurso(Cursos curso)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cursos.Add(curso);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = curso.Id }, curso);
        }

        /// <summary>
        /// Elimina un Curso
        /// </summary>       
        /// <param name="id"> Id del Curso a eliminar</param>  
        /// <returns></returns> 
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>
        [HttpDelete]
        [Route("DeleteCurso")] 
        [ResponseType(typeof(Cursos))]
        public async Task<IHttpActionResult> DeleteCurso(int id)
        {
            Cursos cursos = await db.Cursos.FindAsync(id);
            if (cursos == null)
            {
                return NotFound();
            }

            db.Cursos.Remove(cursos);
            await db.SaveChangesAsync();

            return Ok(cursos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CursosExists(int id)
        {
            return db.Cursos.Count(e => e.Id == id) > 0;
        }
    }
}