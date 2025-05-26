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

//localhost:5000/swagger
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmailSender<IdentityUser>, DummyEmailSender>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // ESCUCHA EN EL PUERTO 5000 SOLO HTTP.
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


//añado autoriazacion
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
    options.Lockout.MaxFailedAccessAttempts = 999;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

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
        //ACA SE INTERACTUA CON EL CONTEXT DE LA DB.
    }
}
//comentarios

app.UseCors(MyAllowSpecificOrigins); // importante que vaya antes

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<IdentityUser>();

app.MapControllers();
app.Run();