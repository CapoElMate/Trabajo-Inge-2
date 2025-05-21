using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Changemarcaformodelinmaquinariatable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Marcas_MarcaName",
                table: "Maquinas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modelos",
                table: "Modelos");

            migrationBuilder.RenameColumn(
                name: "MarcaName",
                table: "Maquinas",
                newName: "ModeloName");

            migrationBuilder.RenameIndex(
                name: "IX_Maquinas_MarcaName",
                table: "Maquinas",
                newName: "IX_Maquinas_ModeloName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modelos",
                table: "Modelos",
                column: "ModeloName");

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Modelos_ModeloName",
                table: "Maquinas",
                column: "ModeloName",
                principalTable: "Modelos",
                principalColumn: "ModeloName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maquinas_Modelos_ModeloName",
                table: "Maquinas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modelos",
                table: "Modelos");

            migrationBuilder.RenameColumn(
                name: "ModeloName",
                table: "Maquinas",
                newName: "MarcaName");

            migrationBuilder.RenameIndex(
                name: "IX_Maquinas_ModeloName",
                table: "Maquinas",
                newName: "IX_Maquinas_MarcaName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modelos",
                table: "Modelos",
                columns: new[] { "ModeloName", "MarcaName" });

            migrationBuilder.AddForeignKey(
                name: "FK_Maquinas_Marcas_MarcaName",
                table: "Maquinas",
                column: "MarcaName",
                principalTable: "Marcas",
                principalColumn: "MarcaName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
