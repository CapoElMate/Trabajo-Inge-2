using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                name: "PermisosEspeciales",
                columns: table => new
                {
                    Permiso = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisosEspeciales", x => x.Permiso);
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
                name: "UsuariosRegistrados",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
                    isDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Edad = table.Column<int>(type: "INTEGER", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", maxLength: 16, nullable: false),
                    Calle = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Altura = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    Dpto = table.Column<string>(type: "TEXT", maxLength: 70, nullable: true),
                    EntreCalles = table.Column<string>(type: "TEXT", maxLength: 70, nullable: false),
                    mailVerificado = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosRegistrados", x => x.DNI);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.PrimaryKey("PK_Modelos", x => x.ModeloName);
                    table.ForeignKey(
                        name: "FK_Modelos_Marcas_MarcaName",
                        column: x => x.MarcaName,
                        principalTable: "Marcas",
                        principalColumn: "MarcaName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.DNI);
                    table.ForeignKey(
                        name: "FK_Clientes_UsuariosRegistrados_DNI",
                        column: x => x.DNI,
                        principalTable: "UsuariosRegistrados",
                        principalColumn: "DNI",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_UsuarioRegistrado_PermisoEspecial_PermisosEspeciales_Permiso",
                        column: x => x.Permiso,
                        principalTable: "PermisosEspeciales",
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
                name: "Maquinas",
                columns: table => new
                {
                    idMaquina = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    anioFabricacion = table.Column<int>(type: "INTEGER", nullable: false),
                    ModeloName = table.Column<string>(type: "TEXT", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: false),
                    TipoMaquinaTipo = table.Column<string>(type: "TEXT", nullable: false),
                    ModeloName1 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => x.idMaquina);
                    table.ForeignKey(
                        name: "FK_Maquinas_Modelos_ModeloName",
                        column: x => x.ModeloName,
                        principalTable: "Modelos",
                        principalColumn: "ModeloName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maquinas_Modelos_ModeloName1",
                        column: x => x.ModeloName1,
                        principalTable: "Modelos",
                        principalColumn: "ModeloName");
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
                name: "Empleados",
                columns: table => new
                {
                    DNI = table.Column<string>(type: "TEXT", nullable: false),
                    nroEmpleado = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.DNI);
                    table.ForeignKey(
                        name: "FK_Empleados_Clientes_DNI",
                        column: x => x.DNI,
                        principalTable: "Clientes",
                        principalColumn: "DNI",
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

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_DNICliente",
                table: "Alquileres",
                column: "DNICliente");

            migrationBuilder.CreateIndex(
                name: "IX_Alquileres_DNIEmpleado",
                table: "Alquileres",
                column: "DNIEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_DNI",
                table: "Clientes",
                column: "DNI",
                unique: true);

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
                name: "IX_Empleados_DNI",
                table: "Empleados",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_nroEmpleado",
                table: "Empleados",
                column: "nroEmpleado",
                unique: true);

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
                name: "IX_Maquinas_ModeloName",
                table: "Maquinas",
                column: "ModeloName");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_ModeloName1",
                table: "Maquinas",
                column: "ModeloName1");

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

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRegistrado_PermisoEspecial_Permiso",
                table: "UsuarioRegistrado_PermisoEspecial",
                column: "Permiso");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Archivos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

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
                name: "PublicacionTagPublicacion");

            migrationBuilder.DropTable(
                name: "Recargos");

            migrationBuilder.DropTable(
                name: "Reembolsos");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "UsuarioRegistrado_PermisoEspecial");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
                name: "PermisosEspeciales");

            migrationBuilder.DropTable(
                name: "Alquileres");

            migrationBuilder.DropTable(
                name: "Maquinas");

            migrationBuilder.DropTable(
                name: "PoliticasDeCancelacion");

            migrationBuilder.DropTable(
                name: "Ubicaciones");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "TiposMaquina");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "UsuariosRegistrados");
        }
    }
}
