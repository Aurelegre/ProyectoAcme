using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encuesta",
                columns: table => new
                {
                    idEncuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    descripcion = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Encuesta__C03F98577E5253C0", x => x.idEncuesta);
                });

            migrationBuilder.CreateTable(
                name: "campo",
                columns: table => new
                {
                    idcampo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    requerido = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idEncuesta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__campo__02D54CF5E41CCB59", x => x.idcampo);
                    table.ForeignKey(
                        name: "fk_encuesta",
                        column: x => x.idEncuesta,
                        principalTable: "Encuesta",
                        principalColumn: "idEncuesta");
                });

            migrationBuilder.CreateTable(
                name: "link",
                columns: table => new
                {
                    idlink = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    link = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    idencuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__link__EAF1A68570C0D3EC", x => x.idlink);
                    table.ForeignKey(
                        name: "fk_encuestaLink",
                        column: x => x.idencuesta,
                        principalTable: "Encuesta",
                        principalColumn: "idEncuesta");
                });

            migrationBuilder.CreateTable(
                name: "respuesta",
                columns: table => new
                {
                    idRespuesta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contenido = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    idcampo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__respuest__8AB5BFC8910A85B6", x => x.idRespuesta);
                    table.ForeignKey(
                        name: "fk_campo",
                        column: x => x.idcampo,
                        principalTable: "campo",
                        principalColumn: "idcampo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_campo_idEncuesta",
                table: "campo",
                column: "idEncuesta");

            migrationBuilder.CreateIndex(
                name: "IX_link_idencuesta",
                table: "link",
                column: "idencuesta");

            migrationBuilder.CreateIndex(
                name: "IX_respuesta_idcampo",
                table: "respuesta",
                column: "idcampo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "link");

            migrationBuilder.DropTable(
                name: "respuesta");

            migrationBuilder.DropTable(
                name: "campo");

            migrationBuilder.DropTable(
                name: "Encuesta");
        }
    }
}
