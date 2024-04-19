using Acme.DTOs;
using Acme.Tablas;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Acme.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class ValidacionController : ControllerBase
    {
        private readonly BdAcmeContext context;
        private readonly IMapper mapper;
        private readonly string secretKey;

        public ValidacionController(BdAcmeContext context, IMapper mapper, IConfiguration config)
        {
            this.context = context;
            this.mapper = mapper;
            secretKey = config.GetSection("settings").GetSection("secretkey").ToString();
        }

        [HttpPost]
        [Route("Validar")]
        public async Task<ActionResult> Post(ValidarUsuarioDTO usuarioValidar)
        {
            var usuario = mapper.Map<Usuario>(usuarioValidar);
            var comprobar = new SqlParameter("@comprobar", SqlDbType.Int);
            comprobar.Direction = ParameterDirection.Output;
                
             await context.Database.ExecuteSqlInterpolatedAsync($@"EXEC SP_InicioSesion 
                                                                 @usuario={usuario.Nombre}, 
                                                                 @Contraseña = {usuario.Contraseña},
                                                                 @validado = {comprobar} OUTPUT");
            int comprobado = (int) comprobar.Value;
            
            if(comprobado == 1)
            {
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Nombre));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(3), //token expira cada 3 mins
                    SigningCredentials = new SigningCredentials (new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                return StatusCode(StatusCodes.Status200OK, new {token =  tokenCreado});
            }
            else
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
        }
    }
}
