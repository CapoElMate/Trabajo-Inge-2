using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddAutoIncrementalPKArchivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos");

            migrationBuilder.AlterColumn<int>(
                name: "idArchivo",
                table: "Archivos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos",
                column: "idArchivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos");

            migrationBuilder.AlterColumn<int>(
                name: "idArchivo",
                table: "Archivos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Archivos",
                table: "Archivos",
                columns: new[] { "idArchivo", "EntidadID", "TipoEntidad" });
        }
    }
}
