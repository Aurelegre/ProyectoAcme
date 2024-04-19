using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Migrations
{
    /// <inheritdoc />
    public partial class ModifySPValidacion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
             ALTER procedure [dbo].[SP_InicioSesion]
	             @usuario varchar(40),
	             @contraseña varchar(20),
				 @validado int OUTPUT
            as 
            begin
	            declare @respuesta varchar(40) = (select Nombre from Usuario where Nombre like(@usuario) and Contraseña like(@contraseña))

	            if @respuesta is not null
				begin	
				select @validado = 1;
				end
				else
				begin
				select @validado = 0;
	            end
            end");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PRECEDURE dbo.SP_InicioSesion");

        }
    }
}
