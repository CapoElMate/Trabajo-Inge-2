using Microsoft.EntityFrameworkCore;
using PruebaApiRest.Context;




var builder = WebApplication.CreateBuilder(args);

//configuracion para poder hacer request desde localHost:
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:63342") // Permite el origen de tu frontend
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});




// Add services to the container.

//crear variable para la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("ConexionDB");
//registrar servicio para la conexion
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins); //configuracion para poder hacer request desde localHost 2

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
