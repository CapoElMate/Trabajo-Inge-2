using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class removeEdadADDFecNacimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "UsuariosRegistrados");

            migrationBuilder.AddColumn<DateTime>(
                name: "fecNacimiento",
                table: "UsuariosRegistrados",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fecNacimiento",
                table: "UsuariosRegistrados");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "UsuariosRegistrados",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
