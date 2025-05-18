using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class addrestoftheentitiesandconfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_UsuariosRegistrados_DNI",
                table: "Cliente");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Cliente_DNI",
                table: "Empleado");

            migrationBuilder.DropForeignKey(
                name: "FK_PermisoRol_Permiso_PermisosidPermiso",
                table: "PermisoRol");

            migrationBuilder.DropForeignKey(
                name: "FK_PermisoRol_Rol_RolesidRol",
                table: "PermisoRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRegistrado_PermisoEspecial_PermisoEspeciales_Permiso",
                table: "UsuarioRegistrado_PermisoEspecial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermisoEspeciales",
                table: "PermisoEspeciales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permiso",
                table: "Permiso");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "PermisoEspeciales",
                newName: "PermisosEspeciales");

            migrationBuilder.RenameTable(
                name: "Permiso",
                newName: "Permisos");

            migrationBuilder.RenameTable(
                name: "Empleado",
                newName: "Empleados");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameIndex(
                name: "IX_Empleado_nroEmpleado",
                table: "Empleados",
                newName: "IX_Empleados_nroEmpleado");

            migrationBuilder.RenameIndex(
                name: "IX_Empleado_DNI",
                table: "Empleados",
                newName: "IX_Empleados_DNI");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_DNI",
                table: "Clientes",
                newName: "IX_Clientes_DNI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "idRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermisosEspeciales",
                table: "PermisosEspeciales",
                column: "Permiso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permisos",
                table: "Permisos",
                column: "idPermiso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados",
                column: "DNI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "DNI");

            migrationBuilder.CreateTable(
                name: "Alquileres",
                columns: table => new
                {
                    idAlquiler = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    fecEfectivizacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DNICliente = table.Column<string>(type: "TEXT", nullable: false),
                    DNIEmpleado = table.Column<string>(type: "TEXT", nullable: false),
                    idReserva = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alquileres", x => x.idAlquiler);
                    table.ForeignKey(
                        name: "FK_Alquileres_Clientes_DNICliente",
                        column: x => x.DNICliente,
                        principalTable: "Clientes",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Alquileres_Empleados_DNIEmpleado",
                        column: x => x.DNIEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Archivos",
                columns: table => new
                {
                    idArchivo = table.Column<int>(type: "INTEGER", nullable: false),
                    EntidadID = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoEntidad = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TipoContenido = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Ruta = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivos", x => new { x.idArchivo, x.EntidadID, x.TipoEntidad });
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    MarcaName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcaName);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    nroPago = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecPago = table.Column<DateTime>(type: "TEXT", nullable: false),
                    idReserva = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.nroPago);
                });

            migrationBuilder.CreateTable(
                name: "PoliticasDeCancelacion",
                columns: table => new
                {
                    Politica = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticasDeCancelacion", x => x.Politica);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    idRespuesta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fec = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Contenido = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    isDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DNIEmpleado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.idRespuesta);
                    table.ForeignKey(
                        name: "FK_Respuestas_Empleados_DNIEmpleado",
                        column: x => x.DNIEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagsMaquina",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsMaquina", x => x.Tag);
                });

            migrationBuilder.CreateTable(
                name: "TagsPublicacion",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsPublicacion", x => x.Tag);
                });

            migrationBuilder.CreateTable(
                name: "TiposEntrega",
                columns: table => new
                {
                    Entrega = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposEntrega", x => x.Entrega);
                });

            migrationBuilder.CreateTable(
                name: "TiposMaquina",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMaquina", x => x.Tipo);
                });

            migrationBuilder.CreateTable(
                name: "Ubicaciones",
                columns: table => new
                {
                    UbicacionName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ubicaciones", x => x.UbicacionName);
                });

            migrationBuilder.CreateTable(
                name: "InfoAsentada",
                columns: table => new
                {
                    idInfo = table.Column<int>(type: "INTEGER", nullable: false),
                    idAlquiler = table.Column<int>(type: "INTEGER", nullable: false),
                    fec = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Contenido = table.Column<string>(type: "TEXT", nullable: false),
                    DNIEmpleado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoAsentada", x => new { x.idInfo, x.idAlquiler });
                    table.ForeignKey(
                        name: "FK_InfoAsentada_Alquileres_idAlquiler",
                        column: x => x.idAlquiler,
                        principalTable: "Alquileres",
                        principalColumn: "idAlquiler",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoAsentada_Empleados_DNIEmpleado",
                        column: x => x.DNIEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reembolsos",
                columns: table => new
                {
                    idReembolso = table.Column<int>(type: "INTEGER", nullable: false),
                    DNICliente = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Monto = table.Column<double>(type: "REAL", precision: 18, scale: 2, nullable: false),
                    Motivo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    idAlquiler = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reembolsos", x => new { x.idReembolso, x.DNICliente });
                    table.ForeignKey(
                        name: "FK_Reembolsos_Alquileres_idAlquiler",
                        column: x => x.idAlquiler,
                        principalTable: "Alquileres",
                        principalColumn: "idAlquiler",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reembolsos_Clientes_DNICliente",
                        column: x => x.DNICliente,
                        principalTable: "Clientes",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    ModeloName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    MarcaName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => new { x.ModeloName, x.MarcaName });
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaName",
                        column: x => x.MarcaName,
                        principalTable: "Marcas",
                        principalColumn: "MarcaName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Maquinas",
                columns: table => new
                {
                    idMaquina = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    anioFabricacion = table.Column<int>(type: "INTEGER", nullable: false),
                    MarcaName = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    TipoMaquinaTipo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => x.idMaquina);
                    table.ForeignKey(
                        name: "FK_Maquinas_Marcas_MarcaName",
                        column: x => x.MarcaName,
                        principalTable: "Marcas",
                        principalColumn: "MarcaName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maquinas_TiposMaquina_Tipo",
                        column: x => x.Tipo,
                        principalTable: "TiposMaquina",
                        principalColumn: "Tipo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maquinas_TiposMaquina_TipoMaquinaTipo",
                        column: x => x.TipoMaquinaTipo,
                        principalTable: "TiposMaquina",
                        principalColumn: "Tipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devoluciones",
                columns: table => new
                {
                    idDevolucion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecDevolucion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    DNIEmpleado = table.Column<string>(type: "TEXT", nullable: false),
                    UbicacionName = table.Column<string>(type: "TEXT", nullable: false),
                    idAlquiler = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devoluciones", x => x.idDevolucion);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Alquileres_idAlquiler",
                        column: x => x.idAlquiler,
                        principalTable: "Alquileres",
                        principalColumn: "idAlquiler",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Empleados_DNIEmpleado",
                        column: x => x.DNIEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devoluciones_Ubicaciones_UbicacionName",
                        column: x => x.UbicacionName,
                        principalTable: "Ubicaciones",
                        principalColumn: "UbicacionName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Empleado_Maquina",
                columns: table => new
                {
                    DNIEmpleado = table.Column<string>(type: "TEXT", nullable: false),
                    IdMaquina = table.Column<int>(type: "INTEGER", nullable: false),
                    FecInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FecFin = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado_Maquina", x => new { x.DNIEmpleado, x.IdMaquina });
                    table.ForeignKey(
                        name: "FK_Empleado_Maquina_Empleados_DNIEmpleado",
                        column: x => x.DNIEmpleado,
                        principalTable: "Empleados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_Maquina_Maquinas_IdMaquina",
                        column: x => x.IdMaquina,
                        principalTable: "Maquinas",
                        principalColumn: "idMaquina",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaquinaPermisoEspecial",
                columns: table => new
                {
                    MaquinariaidMaquina = table.Column<int>(type: "INTEGER", nullable: false),
                    PermisosEspecialesPermiso = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinaPermisoEspecial", x => new { x.MaquinariaidMaquina, x.PermisosEspecialesPermiso });
                    table.ForeignKey(
                        name: "FK_MaquinaPermisoEspecial_Maquinas_MaquinariaidMaquina",
                        column: x => x.MaquinariaidMaquina,
                        principalTable: "Maquinas",
                        principalColumn: "idMaquina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaquinaPermisoEspecial_PermisosEspeciales_PermisosEspecialesPermiso",
                        column: x => x.PermisosEspecialesPermiso,
                        principalTable: "PermisosEspeciales",
                        principalColumn: "Permiso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaquinaTagMaquina",
                columns: table => new
                {
                    MaquinasidMaquina = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsMaquinaTag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinaTagMaquina", x => new { x.MaquinasidMaquina, x.TagsMaquinaTag });
                    table.ForeignKey(
                        name: "FK_MaquinaTagMaquina_Maquinas_MaquinasidMaquina",
                        column: x => x.MaquinasidMaquina,
                        principalTable: "Maquinas",
                        principalColumn: "idMaquina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaquinaTagMaquina_TagsMaquina_TagsMaquinaTag",
                        column: x => x.TagsMaquinaTag,
                        principalTable: "TagsMaquina",
                        principalColumn: "Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Publicaciones",
                columns: table => new
                {
                    idPublicacion = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    PrecioPorDia = table.Column<double>(type: "REAL", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    idMaquina = table.Column<int>(type: "INTEGER", nullable: false),
                    Politica = table.Column<string>(type: "TEXT", nullable: false),
                    UbicacionName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicaciones", x => x.idPublicacion);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Maquinas_idMaquina",
                        column: x => x.idMaquina,
                        principalTable: "Maquinas",
                        principalColumn: "idMaquina",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publicaciones_PoliticasDeCancelacion_Politica",
                        column: x => x.Politica,
                        principalTable: "PoliticasDeCancelacion",
                        principalColumn: "Politica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Ubicaciones_UbicacionName",
                        column: x => x.UbicacionName,
                        principalTable: "Ubicaciones",
                        principalColumn: "UbicacionName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recargos",
                columns: table => new
                {
                    idRecargo = table.Column<int>(type: "INTEGER", nullable: false),
                    idDevolucion = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Total = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recargos", x => new { x.idRecargo, x.idDevolucion });
                    table.ForeignKey(
                        name: "FK_Recargos_Devoluciones_idDevolucion",
                        column: x => x.idDevolucion,
                        principalTable: "Devoluciones",
                        principalColumn: "idDevolucion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    idComentario = table.Column<int>(type: "INTEGER", nullable: false),
                    idPublicacion = table.Column<int>(type: "INTEGER", nullable: false),
                    fec = table.Column<DateTime>(type: "TEXT", nullable: false),
                    isDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    Contenido = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    idRespuesta = table.Column<int>(type: "INTEGER", nullable: false),
                    DNICliente = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => new { x.idComentario, x.idPublicacion });
                    table.ForeignKey(
                        name: "FK_Comentarios_Clientes_DNICliente",
                        column: x => x.DNICliente,
                        principalTable: "Clientes",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Publicaciones_idPublicacion",
                        column: x => x.idPublicacion,
                        principalTable: "Publicaciones",
                        principalColumn: "idPublicacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Respuestas_idRespuesta",
                        column: x => x.idRespuesta,
                        principalTable: "Respuestas",
                        principalColumn: "idRespuesta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PublicacionTagPublicacion",
                columns: table => new
                {
                    PublicacionesidPublicacion = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsPublicacionTag = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicacionTagPublicacion", x => new { x.PublicacionesidPublicacion, x.TagsPublicacionTag });
                    table.ForeignKey(
                        name: "FK_PublicacionTagPublicacion_Publicaciones_PublicacionesidPublicacion",
                        column: x => x.PublicacionesidPublicacion,
                        principalTable: "Publicaciones",
                        principalColumn: "idPublicacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublicacionTagPublicacion_TagsPublicacion_TagsPublicacionTag",
                        column: x => x.TagsPublicacionTag,
                        principalTable: "TagsPublicacion",
                        principalColumn: "Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    idReserva = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fecInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fecFin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    montoTotal = table.Column<double>(type: "decimal(18,2)", nullable: false),
                    Calle = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Altura = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Dpto = table.Column<string>(type: "TEXT", maxLength: 70, nullable: true),
                    EntreCalles = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Entrega = table.Column<string>(type: "TEXT", nullable: false),
                    nroPago = table.Column<int>(type: "INTEGER", nullable: false),
                    idAlquiler = table.Column<int>(type: "INTEGER", nullable: false),
                    DNI = table.Column<string>(type: "TEXT", nullable: false),
                    idPublicacion = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.idReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Alquileres_idAlquiler",
                        column: x => x.idAlquiler,
                        principalTable: "Alquileres",
                        principalColumn: "idAlquiler",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_DNI",
                        column: x => x.DNI,
                        principalTable: "Clientes",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Pagos_nroPago",
                        column: x => x.nroPago,
                        principalTable: "Pagos",
                        principalColumn: "nroPago",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_Publicaciones_idPublicacion",
                        column: x => x.idPublicacion,
                        principalTable: "Publicaciones",
                        principalColumn: "idPublicacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservas_TiposEntrega_Entrega",
                        column: x => x.Entrega,
                        principalTable: "TiposEntrega",
                        principalColumn: "Entrega",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_DNICliente",
                table: "Alquileres",
                column: "DNICliente");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_DNIEmpleado",
                table: "Alquileres",
                column: "DNIEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_DNICliente",
                table: "Comentarios",
                column: "DNICliente");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_idPublicacion",
                table: "Comentarios",
                column: "idPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_idRespuesta",
                table: "Comentarios",
                column: "idRespuesta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_DNIEmpleado",
                table: "Devoluciones",
                column: "DNIEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_idAlquiler",
                table: "Devoluciones",
                column: "idAlquiler",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devoluciones_UbicacionName",
                table: "Devoluciones",
                column: "UbicacionName");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Maquina_IdMaquina",
                table: "Empleado_Maquina",
                column: "IdMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_InfoAsentada_DNIEmpleado",
                table: "InfoAsentada",
                column: "DNIEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_InfoAsentada_idAlquiler",
                table: "InfoAsentada",
                column: "idAlquiler");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinaPermisoEspecial_PermisosEspecialesPermiso",
                table: "MaquinaPermisoEspecial",
                column: "PermisosEspecialesPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_MarcaName",
                table: "Maquinas",
                column: "MarcaName");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_Tipo",
                table: "Maquinas",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_TipoMaquinaTipo",
                table: "Maquinas",
                column: "TipoMaquinaTipo");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinaTagMaquina_TagsMaquinaTag",
                table: "MaquinaTagMaquina",
                column: "TagsMaquinaTag");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaName",
                table: "Modelos",
                column: "MarcaName");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_idMaquina",
                table: "Publicaciones",
                column: "idMaquina");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_Politica",
                table: "Publicaciones",
                column: "Politica");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_UbicacionName",
                table: "Publicaciones",
                column: "UbicacionName");

            migrationBuilder.CreateIndex(
                name: "IX_PublicacionTagPublicacion_TagsPublicacionTag",
                table: "PublicacionTagPublicacion",
                column: "TagsPublicacionTag");

            migrationBuilder.CreateIndex(
                name: "IX_Recargos_idDevolucion",
                table: "Recargos",
                column: "idDevolucion");

            migrationBuilder.CreateIndex(
                name: "IX_Reembolsos_DNICliente",
                table: "Reembolsos",
                column: "DNICliente");

            migrationBuilder.CreateIndex(
                name: "IX_Reembolsos_idAlquiler",
                table: "Reembolsos",
                column: "idAlquiler",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_DNI",
                table: "Reservas",
                column: "DNI");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_Entrega",
                table: "Reservas",
                column: "Entrega");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_idAlquiler",
                table: "Reservas",
                column: "idAlquiler",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_idPublicacion",
                table: "Reservas",
                column: "idPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_idReserva",
                table: "Reservas",
                column: "idReserva",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_nroPago",
                table: "Reservas",
                column: "nroPago",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_DNIEmpleado",
                table: "Respuestas",
                column: "DNIEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_UsuariosRegistrados_DNI",
                table: "Clientes",
                column: "DNI",
                principalTable: "UsuariosRegistrados",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Clientes_DNI",
                table: "Empleados",
                column: "DNI",
                principalTable: "Clientes",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisoRol_Permisos_PermisosidPermiso",
                table: "PermisoRol",
                column: "PermisosidPermiso",
                principalTable: "Permisos",
                principalColumn: "idPermiso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisoRol_Roles_RolesidRol",
                table: "PermisoRol",
                column: "RolesidRol",
                principalTable: "Roles",
                principalColumn: "idRol",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRegistrado_PermisoEspecial_PermisosEspeciales_Permiso",
                table: "UsuarioRegistrado_PermisoEspecial",
                column: "Permiso",
                principalTable: "PermisosEspeciales",
                principalColumn: "Permiso",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_UsuariosRegistrados_DNI",
                table: "Clientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Clientes_DNI",
                table: "Empleados");

            migrationBuilder.DropForeignKey(
                name: "FK_PermisoRol_Permisos_PermisosidPermiso",
                table: "PermisoRol");

            migrationBuilder.DropForeignKey(
                name: "FK_PermisoRol_Roles_RolesidRol",
                table: "PermisoRol");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioRegistrado_PermisoEspecial_PermisosEspeciales_Permiso",
                table: "UsuarioRegistrado_PermisoEspecial");

            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "Empleado_Maquina");

            migrationBuilder.DropTable(
                name: "InfoAsentada");

            migrationBuilder.DropTable(
                name: "MaquinaPermisoEspecial");

            migrationBuilder.DropTable(
                name: "MaquinaTagMaquina");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "PublicacionTagPublicacion");

            migrationBuilder.DropTable(
                name: "Recargos");

            migrationBuilder.DropTable(
                name: "Reembolsos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "TagsMaquina");

            migrationBuilder.DropTable(
                name: "TagsPublicacion");

            migrationBuilder.DropTable(
                name: "Devoluciones");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Publicaciones");

            migrationBuilder.DropTable(
                name: "TiposEntrega");

            migrationBuilder.DropTable(
                name: "Alquileres");

            migrationBuilder.DropTable(
                name: "Maquinas");

            migrationBuilder.DropTable(
                name: "PoliticasDeCancelacion");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "TiposMaquina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermisosEspeciales",
                table: "PermisosEspeciales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Permisos",
                table: "Permisos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Empleados",
                table: "Empleados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.RenameTable(
                name: "PermisosEspeciales",
                newName: "PermisoEspeciales");

            migrationBuilder.RenameTable(
                name: "Permisos",
                newName: "Permiso");

            migrationBuilder.RenameTable(
                name: "Empleados",
                newName: "Empleado");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_nroEmpleado",
                table: "Empleado",
                newName: "IX_Empleado_nroEmpleado");

            migrationBuilder.RenameIndex(
                name: "IX_Empleados_DNI",
                table: "Empleado",
                newName: "IX_Empleado_DNI");

            migrationBuilder.RenameIndex(
                name: "IX_Clientes_DNI",
                table: "Cliente",
                newName: "IX_Cliente_DNI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "idRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermisoEspeciales",
                table: "PermisoEspeciales",
                column: "Permiso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Permiso",
                table: "Permiso",
                column: "idPermiso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Empleado",
                table: "Empleado",
                column: "DNI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "DNI");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_UsuariosRegistrados_DNI",
                table: "Cliente",
                column: "DNI",
                principalTable: "UsuariosRegistrados",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Cliente_DNI",
                table: "Empleado",
                column: "DNI",
                principalTable: "Cliente",
                principalColumn: "DNI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisoRol_Permiso_PermisosidPermiso",
                table: "PermisoRol",
                column: "PermisosidPermiso",
                principalTable: "Permiso",
                principalColumn: "idPermiso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermisoRol_Rol_RolesidRol",
                table: "PermisoRol",
                column: "RolesidRol",
                principalTable: "Rol",
                principalColumn: "idRol",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioRegistrado_PermisoEspecial_PermisoEspeciales_Permiso",
                table: "UsuarioRegistrado_PermisoEspecial",
                column: "Permiso",
                principalTable: "PermisoEspeciales",
                principalColumn: "Permiso",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
