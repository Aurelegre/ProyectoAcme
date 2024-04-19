using Acme.DTOs;
using Acme.Tablas;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Acme.Controllers
{
    [ApiController]
    [Route("api/Responder")]
    public class RespuestaControllers :  ControllerBase
    {
        private readonly BdAcmeContext context;
        private readonly IMapper mapper;

        public RespuestaControllers(BdAcmeContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("{idEncuesta:int}")]
        public async Task<IActionResult> Post(RespuestaAgregarDTO agregarRespuesta, int idcampo) 
        { 
            var respuesta = mapper.Map<Respuestum>(agregarRespuesta);
            var requerido = context.Set<Campo>().Where(c => c.Idcampo == idcampo).FirstOrDefault();//obtengo si es campo obligatorio
            if (requerido.Requerido.Equals("s") & respuesta.Contenido.Equals("") )// realiza validación
            {
                return Ok(new { Mensaje = "Error" });
            }
            if (ValidarTipoDato(requerido.Tipo, respuesta.Contenido))
            {
                respuesta.Idcampo = idcampo;
                context.Add(respuesta);
                await context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Error No coincide el tipo de dato.");
            
        }
        private bool ValidarTipoDato (string tipoDato, string dato) 
        {
            string patronFecha = @"^(0[1-9]|1[0-2]|[1-9]|3[0-1])/(0[1-9]|1[0-2]|[1-9])/\d{4}$"; //dd/mm/yyyy
            if (tipoDato.Equals("Número") && int.TryParse(dato, out _))
            {
                // El tipo de dato es números
                return true;
            }
            else if(tipoDato.Equals("fecha") && Regex.IsMatch(dato,patronFecha))
            {
                // el tipo de dato es fecha
                return true;
            }else if (tipoDato.Equals("Texto"))
            {
                // el tipo de dato es texto
                return true;
            }
        return false;
        }
    }
}
