using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class changePKReembolso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reembolsos",
                table: "Reembolsos");

            migrationBuilder.AlterColumn<int>(
                name: "idReembolso",
                table: "Reembolsos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reembolsos",
                table: "Reembolsos",
                column: "idReembolso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reembolsos",
                table: "Reembolsos");

            migrationBuilder.AlterColumn<int>(
                name: "idReembolso",
                table: "Reembolsos",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reembolsos",
                table: "Reembolsos",
                columns: new[] { "idReembolso", "DNICliente" });
        }
    }
}
