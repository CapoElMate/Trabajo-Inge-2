using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class updateusuarioregistrado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "mailVerificado",
                table: "UsuariosRegistrados",
                newName: "dniVerificado");

            migrationBuilder.AddColumn<string>(
                name: "roleName",
                table: "UsuariosRegistrados",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "roleName",
                table: "UsuariosRegistrados");

            migrationBuilder.RenameColumn(
                name: "dniVerificado",
                table: "UsuariosRegistrados",
                newName: "mailVerificado");
        }
    }
}
