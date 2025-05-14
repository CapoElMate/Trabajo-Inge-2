using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addClienteEmpleadoRolPermisoPermisoEspecialandUsuarioRegistrado_PermisoEspecial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.DNI);
                    table.ForeignKey(
                        name: "FK_Cliente_UsuariosRegistrados_DNI",
                        column: x => x.DNI,
                        principalTable: "UsuariosRegistrados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permiso",
                columns: table => new
                {
                    idPermiso = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permiso", x => x.idPermiso);
                });

            migrationBuilder.CreateTable(
                name: "PermisoEspeciales",
                columns: table => new
                {
                    Permiso = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisoEspeciales", x => x.Permiso);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", nullable: false),
                    nroEmpleado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.DNI);
                    table.ForeignKey(
                        name: "FK_Empleado_Cliente_DNI",
                        column: x => x.DNI,
                        principalTable: "Cliente",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRegistrado_PermisoEspecial",
                columns: table => new
                {
                    UsuarioRegistradoDNI = table.Column<string>(type: "TEXT", nullable: false),
                    Permiso = table.Column<string>(type: "TEXT", nullable: false),
                    fecEmision = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fecVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRegistrado_PermisoEspecial", x => new { x.UsuarioRegistradoDNI, x.Permiso });
                    table.ForeignKey(
                        name: "FK_UsuarioRegistrado_PermisoEspecial_PermisoEspeciales_Permiso",
                        column: x => x.Permiso,
                        principalTable: "PermisoEspeciales",
                        principalColumn: "Permiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRegistrado_PermisoEspecial_UsuariosRegistrados_UsuarioRegistradoDNI",
                        column: x => x.UsuarioRegistradoDNI,
                        principalTable: "UsuariosRegistrados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermisoRol",
                columns: table => new
                {
                    PermisosidPermiso = table.Column<int>(type: "INTEGER", nullable: false),
                    RolesidRol = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisoRol", x => new { x.PermisosidPermiso, x.RolesidRol });
                    table.ForeignKey(
                        name: "FK_PermisoRol_Permiso_PermisosidPermiso",
                        column: x => x.PermisosidPermiso,
                        principalTable: "Permiso",
                        principalColumn: "idPermiso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermisoRol_Rol_RolesidRol",
                        column: x => x.RolesidRol,
                        principalTable: "Rol",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRegistrados_DNI",
                table: "UsuariosRegistrados",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRegistrados_Email",
                table: "UsuariosRegistrados",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosRegistrados_passwordHash",
                table: "UsuariosRegistrados",
                column: "passwordHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_DNI",
                table: "Cliente",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_DNI",
                table: "Empleado",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_nroEmpleado",
                table: "Empleado",
                column: "nroEmpleado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermisoRol_RolesidRol",
                table: "PermisoRol",
                column: "RolesidRol");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRegistrado_PermisoEspecial_Permiso",
                table: "UsuarioRegistrado_PermisoEspecial",
                column: "Permiso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "PermisoRol");

            migrationBuilder.DropTable(
                name: "UsuarioRegistrado_PermisoEspecial");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Permiso");

            migrationBuilder.DropTable(
                name: "Rol");

            migrationBuilder.DropTable(
                name: "PermisoEspeciales");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRegistrados_DNI",
                table: "UsuariosRegistrados");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRegistrados_Email",
                table: "UsuariosRegistrados");

            migrationBuilder.DropIndex(
                name: "IX_UsuariosRegistrados_passwordHash",
                table: "UsuariosRegistrados");
        }
    }
}
