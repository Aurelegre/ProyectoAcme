using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Migrations
{
    /// <inheritdoc />
    public partial class AggCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_encuesta",
                table: "campo");

            migrationBuilder.DropForeignKey(
                name: "fk_campo",
                table: "respuesta");

            migrationBuilder.AlterColumn<int>(
                name: "idcampo",
                table: "respuesta",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idEncuesta",
                table: "campo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_encuesta",
                table: "campo",
                column: "idEncuesta",
                principalTable: "Encuesta",
                principalColumn: "idEncuesta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_campo",
                table: "respuesta",
                column: "idcampo",
                principalTable: "campo",
                principalColumn: "idcampo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_encuesta",
                table: "campo");

            migrationBuilder.DropForeignKey(
                name: "fk_campo",
                table: "respuesta");

            migrationBuilder.AlterColumn<int>(
                name: "idcampo",
                table: "respuesta",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idEncuesta",
                table: "campo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_encuesta",
                table: "campo",
                column: "idEncuesta",
                principalTable: "Encuesta",
                principalColumn: "idEncuesta");

            migrationBuilder.AddForeignKey(
                name: "fk_campo",
                table: "respuesta",
                column: "idcampo",
                principalTable: "campo",
                principalColumn: "idcampo");
        }
    }
}
