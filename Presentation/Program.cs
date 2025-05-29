using System;
using Data_Access_Layer;
using Domain_Layer.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI;
using Bussines_Logic_Layer.Interfaces;
using Bussines_Logic_Layer.Services;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositorios.SQL;
using Bussines_Logic_Layer.Mapping;
using MercadoPago.Config;
using Bussines_Logic_Layer.DTOs.Usuarios;
using Bussines_Logic_Layer.Managers;
using Microsoft.Extensions.Options;
using System.Configuration;
using Mailjet.Client;
using Microsoft.AspNetCore.Identity.UI.Services;


//añado el acces token de MeLi:
MercadoPagoConfig.AccessToken = "APP_USR-7358553432925364-052814-bea62fcaedc85041522284dcca5d1ad2-363087617";


//localhost:5000/swagger
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MailjetOptions>(builder.Configuration.GetSection("MailjetOptions"));
builder.Services.AddSingleton<IMailjetClient>(serviceProvider =>
{
    var mailjetOptions = serviceProvider.GetRequiredService<IOptions<MailjetOptions>>().Value;
    return new MailjetClient(mailjetOptions.ApiKey, mailjetOptions.SecretKey);
});

//builder.Services.AddSingleton<IEmailSender<IdentityUser>, MailjetEmailSender>();
builder.Services.AddSingleton<IEmailSender<IdentityUser>, DummyEmailSender>();
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // ESCUCHA EN EL PUERTO 5000 SOLO HTTP.
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


//aï¿½ado autoriazacion
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

//configuro la bdd
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Replace the problematic line with the following:
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//configuro identity
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;


});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
    
//});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173") // Permite el origen de tu frontend
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

//Repositorios
builder.Services.AddScoped<IMaquinaRepository, MaquinaRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IModeloRepository, ModeloRepository>();
builder.Services.AddScoped<IPermisoEspecialRepository, PermisoEspecialRepository>();
builder.Services.AddScoped<ITagMaquinaRepository, TagMaquinaRepository>();
builder.Services.AddScoped<ITipoMaquinaRepository, TipoMaquinaRepository>();
builder.Services.AddScoped<IUsuarioRegistradoRepository, UsuarioRegistradoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IPublicacionRepository, PublicacionRepository>();
builder.Services.AddScoped<ITagPublicacionRepository, TagPublicacionRepository>();
builder.Services.AddScoped<IAlquilerRepository, AlquilerRepository>();
builder.Services.AddScoped<IReembolsoRepository, ReembolsoRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();
builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRespository>();


//Servicios
builder.Services.AddScoped<IMaquinaService, MaquinaService>();
builder.Services.AddScoped<IMarcaService, MarcaService>();
builder.Services.AddScoped<IModeloService, ModeloService>();
builder.Services.AddScoped<IPermisoEspecialService, PermisoEspecialServie>();
builder.Services.AddScoped<ITagMaquinaService, TagMaquinaService>();
builder.Services.AddScoped<ITipoMaquinaService, TipoMaquinaService>();
builder.Services.AddScoped<IUsuarioRegistradoService, UsuarioRegistradoService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPublicacionService, PublicacionService>();
builder.Services.AddScoped<ITagPublicacionService, TagPublicacionService>();
builder.Services.AddScoped<IAlquilerService, AlquilerService>();
builder.Services.AddScoped<IReembolsoService, ReembolsoService>();
builder.Services.AddScoped<IUbicacionService, UbicacionService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();

builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())

    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        //// --- Creación de roles iniciales ---
        //var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        //// Obtener el repositorio/servicio de UsuarioRegistrado
        //var usuarioRegistradoRepo = services.GetRequiredService<IUsuarioRegistradoRepository>(); // Usaremos el servicio ya que lo tienes definido.

        //string[] roleNames = { "Dueño", "Empleado", "Cliente" };
        //IdentityResult roleResult;

        //foreach (var roleName in roleNames)
        //{
        //    // Verifica si el rol ya existe
        //    if (!await roleManager.RoleExistsAsync(roleName))
        //    {
        //        // Crea el rol.
        //        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));

        //        if (!roleResult.Succeeded)
        //        {
        //            Console.WriteLine($"Error al crear el rol {roleName}:");
        //            foreach (var error in roleResult.Errors)
        //            {
        //                Console.WriteLine($"- {error.Description}");
        //            }
        //        }
        //    }
        //}
        //// --- Fin de Creación de roles iniciales ---

        //// --- Creación de usuario "Dueño" (administrador) ---
        //var adminUserName = "admin@bobelalquilador.com";
        //var adminPassword = "Password123!";
        //var adminRoleName = "Dueño";

        //var adminUser = await userManager.FindByNameAsync(adminUserName);

        //if (adminUser == null)
        //{
        //    adminUser = new IdentityUser
        //    {
        //        UserName = adminUserName,
        //        Email = adminUserName,
        //        EmailConfirmed = true
        //    };

        //    var createUserResult = await userManager.CreateAsync(adminUser, adminPassword);

        //    if (createUserResult.Succeeded)
        //    {
        //        Console.WriteLine($"Usuario Identity '{adminUserName}' creado exitosamente.");

        //        // Asignar el rol "Dueño" al nuevo usuario Identity
        //        if (await roleManager.RoleExistsAsync(adminRoleName))
        //        {
        //            var addToRoleResult = await userManager.AddToRoleAsync(adminUser, adminRoleName);
        //            if (addToRoleResult.Succeeded)
        //            {
        //                Console.WriteLine($"Usuario Identity '{adminUserName}' asignado al rol '{adminRoleName}' exitosamente.");

        //                // --- CREAR EL USUARIO REGISTRADO ASOCIADO ---
        //                var nuevoUsuarioRegistrado = new UsuarioRegistrado
        //                {
        //                    Email = adminUserName,
        //                    DNI = "999",
        //                    isDeleted = false,
        //                    Nombre = "Admin",
        //                    Apellido = "",
        //                    Edad = 99,
        //                    Telefono = "999999",
        //                    Calle = "Calle Falsa",
        //                    Altura = "123",
        //                    Dpto = null,
        //                    EntreCalles = "Av. Siempre Viva y Calle de Atrás",
        //                    dniVerificado = true,
        //                    roleName = adminRoleName,
        //                };

        //                await usuarioRegistradoRepo.AddAsync(nuevoUsuarioRegistrado); 
        //                // --- FIN DE CREACIÓN DE USUARIO REGISTRADO ---

        //            }
        //            else
        //            {
        //                Console.WriteLine($"Error al asignar el rol '{adminRoleName}' al usuario Identity '{adminUserName}':");
        //                foreach (var error in addToRoleResult.Errors)
        //                {
        //                    Console.WriteLine($"- {error.Description}");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine($"El rol '{adminRoleName}' no existe, no se pudo asignar al usuario Identity '{adminUserName}'.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Error al crear el usuario Identity '{adminUserName}':");
        //        foreach (var error in createUserResult.Errors)
        //        {
        //            Console.WriteLine($"- {error.Description}");
        //        }
        //    }
        //}
        //ACA SE INTERACTUA CON EL CONTEXT DE LA DB.
    }
}
//comentarios

// ---
// INICIO DEL CÓDIGO DE PRUEBA DE EMAIL
// ---

// IMPORTANTE: Este código de prueba se ejecutará CADA VEZ que inicies la aplicación.
// DEBERÍAS QUITARLO una vez que hayas verificado que el envío de emails funciona.

//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    try
//    {
        
//        var emailSender = serviceProvider.GetRequiredService<IEmailSender<IdentityUser>>();
//        string testEmail = "@gmail.com"; 
//        string testUserName = "UsuarioDePrueba"; 
//        string testConfirmationLink = "https://tualquilador.com/confirm?token=TEST_CONFIRMATION_TOKEN"; 
//        string testResetLink = "https://tualquilador.com/resetpassword?token=TEST_RESET_TOKEN"; 
//        string testResetCode = "123456"; 

        
//        var dummyUser = new IdentityUser { UserName = testUserName, Email = testEmail };

//        Console.WriteLine("Intentando enviar emails de prueba...");

//        // Prueba de email de confirmación
//        await emailSender.SendConfirmationLinkAsync(dummyUser, testEmail, testConfirmationLink);

//        // Prueba de email de restablecimiento de contraseña (por enlace)
//        await emailSender.SendPasswordResetLinkAsync(dummyUser, testEmail, testResetLink);

//        // Prueba de email de restablecimiento de contraseña (por código)
//        // Puedes comentar esta línea si solo usas el enlace para restablecer contraseña
//        await emailSender.SendPasswordResetCodeAsync(dummyUser, testEmail, testResetCode);

//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"ERROR al enviar emails de prueba: {ex.Message}");
//    }
//}

// ---
// FIN DEL CÓDIGO DE PRUEBA DE EMAIL
// ---

app.UseCors(MyAllowSpecificOrigins); // importante que vaya antes

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<IdentityUser>();

app.MapControllers();
app.Run();