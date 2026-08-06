using Gestor_Oficios.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Conexión SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


builder.Services.AddControllers();


var app = builder.Build();
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


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();