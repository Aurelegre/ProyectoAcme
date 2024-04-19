using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Migrations
{
    /// <inheritdoc />
    public partial class AggSPValidacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            create procedure SP_InicioSesion
	             @usuario varchar(40),
	             @contraseña varchar(20)
            as 
            begin
	            declare @respuesta varchar(40) = (select Nombre from Usuario where Nombre like(@usuario) and Contraseña like(@contraseña))

	            if @respuesta is not null
	            begin	
	            return 'true';
	            end
	            else
	            begin
	            return	'false';
	            end
            end");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.SP_InicioSesion");
        }
    }
}
