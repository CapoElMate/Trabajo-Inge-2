using System;
using Data_Access_Layer;
using Microsoft.EntityFrameworkCore;

//localhost:5000/swagger
var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000); // ESCUCHA EN EL PUERTO 5000 SOLO HTTP.
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.MapControllers();
app.Run();