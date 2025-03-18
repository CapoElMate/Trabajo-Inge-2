using Microsoft.EntityFrameworkCore;
using PruebaApiRest.Context;
using Microsoft.EntityFrameworkCore.Sqlite;



var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { EnvironmentName = "Development", ApplicationName = "ApiREST- .NET" }); ;

//configuracion para poder hacer request desde localHost:
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin() // Permite el origen de tu frontend
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});




// Add services to the container.

//crear variable para la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("ConexionDBSqlite");
//registrar servicio para la conexion
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.Use(async (context, next) =>
{
    // Agrega encabezados CORS directamente a cada respuesta
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With");

    // Manejo especial para solicitudes OPTIONS (preflight)
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }

    await next();
});


app.UseCors(MyAllowSpecificOrigins); //configuracion para poder hacer request desde localHost 2

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())

    {

        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<AppDBContext>();

        if (context.Database.GetPendingMigrations().Any())

        {

            context.Database.Migrate();

        }

    }

}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
