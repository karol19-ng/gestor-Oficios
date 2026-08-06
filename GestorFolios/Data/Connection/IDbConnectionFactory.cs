using System.Data;

namespace GestorOficios.Data.Connection
{
    /// <summary>
    /// Crea conexiones ADO.NET listas para usar con Dapper al invocar
    /// los Stored Procedures. Independiente del DbContext de EF Core
    /// (EF se sigue usando para las consultas simples / catálogos).
    /// </summary>
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
