using Acme.DTOs;
using Acme.Tablas;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Acme.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Encuesta")]
    public class EncuestaController : ControllerBase
    {
        private readonly BdAcmeContext context;
        private readonly IMapper mapper;

        public EncuestaController(BdAcmeContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CrearEncuestaDTO encuestaCrear) // crea la encuesta y el link
        {
            var encuesta = new Encuestum
            {
                Nombre = encuestaCrear.Nombre,
                Descripcion = encuestaCrear.Descripcion
            };
            context.Add(encuesta);    
            await context.SaveChangesAsync();
            var ultimoID = context.Set<Encuestum>().OrderByDescending(t => t.IdEncuesta).FirstOrDefault(); //obtiene el ultimo registro
            var link = new Link  //mapeo manual de los registros
            {
                Link1 = "https://localhost:7200/api/Responder/" + ultimoID.IdEncuesta + "",
                Idencuesta = ultimoID.IdEncuesta  
            };
            context.Add(link);
            await context.SaveChangesAsync();


            return Ok();
        }
        [HttpPost]
        [Route("AggCampos")]
        public async Task<IActionResult> Post(int idEncuesta, CrearCamposDTO camposCrear) // crea los campos de la encuesta
        {
            var campo = mapper.Map<Campo>(camposCrear);
            campo.IdEncuesta = idEncuesta;
            context.Add(campo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("Select")]
        public async Task<ActionResult> GetSelect(int id)
        {
            var encuesta = await context.Encuesta
                .Select(en => new
                {
                    en.IdEncuesta,
                    en.Nombre,
                    en.Descripcion,
                    Enlace = en.Links.Select(link => link.Link1).ToList(),
                    Campos = en.Campos.OrderBy(cam => cam.Idcampo).Select(cam => new
                    {
                        Id = cam.Idcampo,
                        cam.Nombre,
                        cam.Titulo,
                        cam.Tipo,
                        cantidadRepuestas = cam.Respuesta.Count(),
                        respuestas = cam.Respuesta.OrderBy(re => re.IdRespuesta).Select(re => new
                        {
                            re.Contenido
                        })

                    })
                })
                .FirstOrDefaultAsync(p=> p.IdEncuesta == id);

            if(encuesta is null)
            {
                return NotFound();
            }

            return Ok(encuesta);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> put(int idEncuesta, CrearEncuestaDTO encuestamodify)
        {
            var encuesta = await context.Encuesta.FirstOrDefaultAsync(e => e.IdEncuesta == idEncuesta);
            if(encuesta is null) {  return NotFound(); }

            encuesta.Nombre = !string.IsNullOrEmpty(encuestamodify.Nombre) & !encuestamodify.Nombre.Equals("string") ? encuestamodify.Nombre : encuesta.Nombre;
            encuesta.Descripcion = !string.IsNullOrEmpty(encuestamodify.Descripcion) & !encuestamodify.Descripcion.Equals("string") ? encuestamodify.Descripcion : encuesta.Descripcion;

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Update/Campo")]
        public async Task<ActionResult> Put(int idcampo, int idEncuesta, CrearCamposDTO campomodify)
        {
            var campo = await context.Campos.FirstOrDefaultAsync(c => c.Idcampo == idcampo & c.IdEncuesta == idEncuesta);
            if(campo is null) { return NotFound(); }

            campo.Nombre = !string.IsNullOrEmpty(campomodify.Nombre) & !campomodify.Nombre.Equals("string") ? campomodify.Nombre : campo.Nombre;
            campo.Titulo = !string.IsNullOrEmpty(campomodify.Titulo) & !campomodify.Titulo.Equals("string") ? campomodify.Titulo : campo.Titulo;
            campo.Requerido = !string.IsNullOrEmpty(campomodify.Requerido) & !campomodify.Requerido.Equals("string") ? campomodify.Requerido : campo.Requerido;
            campo.Tipo = !string.IsNullOrEmpty(campomodify.Tipo) & !campomodify.Tipo.Equals("string") ? campomodify.Tipo : campo.Tipo;
            
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int idEncuesta)
        {
            var filasAfectadas = await context.Encuesta.Where(e => e.IdEncuesta == idEncuesta).ExecuteDeleteAsync();
            

            if(filasAfectadas == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}

