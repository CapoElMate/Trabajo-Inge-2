using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class updateEntreCallesToPiso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntreCalles",
                table: "UsuariosRegistrados",
                newName: "Piso");

            migrationBuilder.RenameColumn(
                name: "EntreCalles",
                table: "Reservas",
                newName: "Piso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Piso",
                table: "UsuariosRegistrados",
                newName: "EntreCalles");

            migrationBuilder.RenameColumn(
                name: "Piso",
                table: "Reservas",
                newName: "EntreCalles");
        }
    }
}
