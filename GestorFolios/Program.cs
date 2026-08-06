using Gestor_Oficios.Data;
using GestorOficios.Data.Connection;
using GestorOficios.Repositories;
using GestorOficios.Repositories.Interfaces;
using GestorOficios.Services.Implementations;
using GestorOficios.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//==================================== <>==========================//


// Conexión SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//==================================== <>==========================//


// Fabrica de conexiones ADO.NET para Dapper (llamadas a Stored Procedures)
builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();

// Carpeta Repositories  contenido interfaz y servicios 
builder.Services.AddScoped<IFolioRepository, FolioRepository>();

builder.Services.AddScoped<IReservaRepository, ReservaRepository>();

builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Carpeta Services: logica de negocio / orquestacion sobre los repositorios
builder.Services.AddScoped<IFolioService, FolioService>();




//==================================== <>==========================//
builder.Services.AddControllers();




//==================================== <>==========================//
var app = builder.Build();



//==================================== <>==========================//

//Prueba de conexion a la base de datos
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        if (db.Database.CanConnect())
        {
            Console.WriteLine(" Conexión exitosa a la base de datos");
        }
        else
        {
            Console.WriteLine(" No se pudo conectar a la base de datos");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(" Error de conexión:");
        Console.WriteLine(ex.Message);
    }
}
//==================================== <>==========================//


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();