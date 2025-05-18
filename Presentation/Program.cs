using System;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

//localhost:5000/swagger
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // ESCUCHA EN EL PUERTO 5000 SOLO HTTP.
});

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.Use(async (context, next) =>
{
    // Agrega encabezados CORS directamente a cada respuesta
    //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    //context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    //context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization, X-Requested-With");

    // Manejo especial para solicitudes OPTIONS (preflight)
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        await context.Response.CompleteAsync();
        return;
    }

    await next();
});

app.Use(async (context, next) =>
{
    await next();
    Console.WriteLine($"{context.Response.StatusCode} {context.Request.Method} {context.Request.Method}");
});

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

app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();