using Acme.DTOs;
using Acme.Tablas;
using AutoMapper;

namespace Acme.Utilidades
{
    public class AutoMapperPerfiles : Profile
    {
        public AutoMapperPerfiles()
        {
            CreateMap<CrearEncuestaDTO,Encuestum>();
            CreateMap<CrearCamposDTO, Campo>();
            CreateMap<RespuestaAgregarDTO, Respuestum>();
            CreateMap<ValidarUsuarioDTO, Usuario>();
        }
    }
}
